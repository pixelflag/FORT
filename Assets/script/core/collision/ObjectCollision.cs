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
		for (int i = 0; i < field.teams.Length; i++)
		{
			for (int j = 0; j < field.teams.Length; j++)
			{
				if(i != j)
					HitCkeck(field.teams[i], field.teams[j]);
			}
		}

		void HitCkeck(Team t1, Team t2)
        {
			for (int p1 = 0; p1 < t1.platoons.Count; p1++)
			{
				for (int u1 = 0; u1 < t1.platoons[p1].units.Count; u1++)
				{
					Unit unit1 = t1.platoons[p1].units[u1];
					if (unit1.weapon != null)
					{
						CollisionObject co1 = unit1.weapon.collision;
						for (int p2 = 0; p2 < t2.platoons.Count; p2++)
						{
							for (int u2 = 0; u2 < t2.platoons[p2].units.Count; u2++)
							{
								Unit unit2 = t2.platoons[p2].units[u2];
								if (CheckCollision(co1, unit2.collision))
									unit2.HitAttack(unit1.GetAttackData());
							}
						}
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
