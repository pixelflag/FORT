using System.Collections.Generic;
using UnityEngine;

public interface ICharacterLogic
{
    void Execute(FieldMapObject activeArea, Character player, List<Enemy> targets);
    void SetSeachRadius(float radius);
    void SetPositionTarget(Vector2 position);
    bool isIdle { get; }
}