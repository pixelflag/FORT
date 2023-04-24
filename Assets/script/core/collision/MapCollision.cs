using System.Collections.Generic;
using UnityEngine;

public class MapCollision
{
	public void Execute(FieldMapObject map, ObjectExecutor objects)
	{
		// Player
		Player player = objects.player;
		if (objects.player != null)
		{
			CheckMapCorrection(objects.player);
			if (player.weapon != null)
				CheckBrokenSprite(player.weapon.collision, player.GetAttackData());
		}

		// Buddies
		foreach (Character buddie in objects.buddies)
		{
			CheckMapCorrection(buddie);
			if (buddie.weapon != null)
				CheckBrokenSprite(player.weapon.collision, player.GetAttackData());
		}

		// Enemy
		foreach (MassObject obj in map.enemys)
        {
			CheckMapCorrection(obj);
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
				int radius = col.GetRadius();
				Box box = col.GetBox();

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
							Cell cell = map.GetCell(location);
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
				Box box = wc.GetBox();
				for (int y = (int)box.bottomLeft.y - 1; y < (int)box.topRight.y + 1; y++)
				{
					for (int x = (int)box.bottomLeft.x - 1; x < (int)box.topRight.x + 1; x++)
					{
						Vector2Int location = new Vector2Int(x, y);
						if (map.ExistsCellData(location))
						{
							Cell cell = map.GetCell(location);
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
