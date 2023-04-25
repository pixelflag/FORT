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
		for (int i = 0; i < field.teams.Length-1; i++)
		{
			for (int j = i+1; j < field.teams.Length; j++)
			{
                HitCkeck(field.teams[i], field.teams[j]);
			}
		}

		void HitCkeck(Team t1, Team t2)
        {
			for (int i = 0; i < t1.units.Count - 1; i++)
			{
				if (t1.units[i].weapon != null)
				{
					CollisionObject co1 = t1.units[i].weapon.collision;

					for (int j = i + 1; j < t2.units.Count; j++)
					{
						if (CheckCollision(co1, t2.units[i].collision))
							t2.units[i].HitAttack(t1.units[i].GetAttackData());
					}
				}
			}
		}
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

	private bool CheckCollision(CollisionObject objA, CollisionObject objB)
	{
		if (!objA.objectCollisionDisabled && !objB.objectCollisionDisabled)
		{
			if (BoxCollision.BoxHitCheck(objA.box, objB.box))
			{
				return true;
			}
		}
		return false;
	}
}
