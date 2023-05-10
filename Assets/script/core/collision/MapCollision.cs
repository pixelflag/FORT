using System.Collections.Generic;
using UnityEngine;

public class MapCollision : DI
{
	FieldMap map;
	public Team[] teams;

	public MapCollision(Team[] teams, FieldMap map)
    {
		this.teams = teams;
		this.map = map;
	}

	public void Execute()
	{
		for (int t = 0; t < teams.Length; t++)
		{
			for (int p = 0; p < teams[t].platoons.Count; p++)
			{
				List<Unit> units = teams[t].platoons[p].units;
				for (int u = 0; u < units.Count; u++)
				{
					CheckMapCorrection(units[u]);
					if (units[u].weapon != null)
						CheckBrokenSprite(units[u].weapon.collision, units[u].GetAttackData());
				}
			}
		}

		// Fire
		foreach (FireObject fire in objects.fires)
		{
			CheckBrokenSprite(fire.collision, fire.GetAttackData());
		}

		void CheckMapCorrection(MassObject massObj)
		{
			CollisionObject col = massObj.collision;
			if (col.mapCollisionDisabled == false)
			{
				Vector3 resultPosition = col.position;
				int radius = col.radius;
				Box box = col.box;

				int t = (int)(box.topRight.y / Global.gridSize.y);
				int r = (int)(box.topRight.x / Global.gridSize.x);
				int b = (int)(box.bottomLeft.y / Global.gridSize.y);
				int l = (int)(box.bottomLeft.x / Global.gridSize.x);

				for (int y = b - 1; y < t + 1; y++)
				{
					for (int x = l - 1; x < r + 1; x++)
					{
						Vector2Int location = new Vector2Int(x, -y);
						if (map.ExistsCellData(location))
						{
							Cell cell = map.GetCell(location) as Cell;
							if (cell.isCollision)
								resultPosition = BoxCollision.CirclePositionCorrection(resultPosition, radius, cell.box);
						}
					}
				}
				massObj.position = resultPosition;
			}
		}

		bool CheckCollision(Box boxA, Box boxB)
		{
			if (BoxCollision.BoxHitCheck(boxA, boxB))
			{
				return true;
			}
			return false;
		}

		void CheckBrokenSprite(CollisionObject wc, AttackData ad)
		{
			if (!wc.objectCollisionDisabled)
			{
				Box box = wc.box;
				for (int y = (int)box.bottomLeft.y - 1; y < (int)box.topRight.y + 1; y++)
				{
					for (int x = (int)box.bottomLeft.x - 1; x < (int)box.topRight.x + 1; x++)
					{
						Vector2Int location = new Vector2Int(x, y);
						if (map.ExistsCellData(location))
						{
							Cell cell = map.GetCell(location) as Cell;
							if (cell.canBreak && !cell.isBroken)
							{
								Box cellBox = cell.box;
								cellBox.position += (Vector2)map.position;

								if (CheckCollision(box, cellBox))
									if (AskBroken(cell.type, ad))
										cell.Broken();
							}
						}
					}
				}
			}
		}
	}

	private bool AskBroken(CellType type, AttackData attackData)
	{
		WeaponType wt = attackData.weapon.type;
		ElementType el = attackData.weapon.element;

		switch (type)
		{
			case CellType.None:
			case CellType.Collision:
				break;
			case CellType.Grass:
				if (wt == WeaponType.Sickle) return true;
				if (wt == WeaponType.Sword) return true;
				if (wt == WeaponType.Spear) return true;
				break;
			case CellType.Wood:
				if (wt == WeaponType.Axe) return true;
				break;
			case CellType.Rock:
				if (wt == WeaponType.Hammer) return true;
				break;
			case CellType.Fire:
				if (el == ElementType.Ice) return true;
				break;
			case CellType.Ice:
				if (el == ElementType.Fire) return true;
				if (wt == WeaponType.Hammer) return true;
				break;
		}
		return false;
	}
}
