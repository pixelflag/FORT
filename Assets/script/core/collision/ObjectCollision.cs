public class ObjectCollision:DI
{
	public void Initialize()
    {
		// Empty
    }

	public void Execute()
	{
		CheckUnit();
		CheckFire();
	}

	private void CheckUnit()
    {

	}

	private void CheckFire()
	{
		// 後で考えよ

		/*
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
		*/
	}

	// ------


}
