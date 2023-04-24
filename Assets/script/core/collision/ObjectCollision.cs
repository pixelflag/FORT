public class ObjectCollision:DI
{
	public void Initialize()
    {

    }

	public void Execute(ObjectExecutor objects)
	{
		CheckPlayer(objects);
		CheckBuddies(objects);
		CheckEnemy(objects);
		CheckFire(objects);
	}

	private void CheckPlayer(ObjectExecutor objects)
    {
		Player player = objects.player;
		CollisionObject PCo = player.collision;
		FieldMapObject area = field.map;

		// player attack
		if (player.weapon != null)
		{
			CollisionObject wc = player.weapon.collision;
			AttackData ad = player.GetAttackData();

			foreach (Enemy enemy in area.enemys)
			{
				if (CheckCollision(wc, enemy.collision))
					enemy.HitAttack(ad);
			}
		}

		// item
		foreach (Item item in area.items)
		{
			if (CheckCollision(PCo, item.collision))
			{
				player.HitItem(item);
				// item.Open();
			}
		}
	}

    private void CheckBuddies(ObjectExecutor objects)
    {
		FieldMapObject area = field.map;

		foreach (Character buddie in objects.buddies)
		{
			if (buddie.weapon != null)
			{
				CollisionObject RwCo = buddie.weapon.collision;
				AttackData ad = buddie.GetAttackData();

				// enemy
				foreach (Enemy enemy in area.enemys)
				{
					if (CheckCollision(RwCo, enemy.collision))
						enemy.HitAttack(ad);
				}
			}
		}
	}

	private void CheckEnemy(ObjectExecutor objects)
    {
		Player player = objects.player;
		CollisionObject PCo = player.collision;
		FieldMapObject map = field.map;

		foreach (Enemy enemy in map.enemys)
		{
			// weaponを持たずに体当たりしかない可能性がある。
			// 敵をデザインするときに再設計する。

			AttackData EAo = enemy.GetAttackData();
			CollisionObject Eco = enemy.collision;

			if (CheckCollision(Eco, PCo))
				player.HitAttack(EAo);

			foreach (Character buddie in objects.buddies)
			{
				if (CheckCollision(Eco, buddie.collision))
					buddie.HitAttack(EAo);
			}
		}
	}

	private void CheckFire(ObjectExecutor objects)
	{
		FieldMapObject area = field.map;

		foreach (FireObject fire in objects.fires)
		{
			CollisionObject FrCo = fire.collision;
			AttackData ad = fire.GetAttackData();

			// Fireは点で評価したほうがいいかもなあ。
			foreach (Enemy enemy in area.enemys)
			{
				if (CheckCollision(FrCo, enemy.collision))
				{
					fire.Hit();
					enemy.HitAttack(ad);
					break;
				}
			}
			if (fire.isDestroy) continue;
		}
	}

	// ------

	private bool CheckCollision(CollisionObject objA, CollisionObject objB)
	{
		if (!objA.objectCollisionDisabled && !objB.objectCollisionDisabled)
		{
			if (BoxCollision.BoxHitCheck(objA.GetBox(), objB.GetBox()))
			{
				return true;
			}
		}
		return false;
	}
}
