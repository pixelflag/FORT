using System.Collections.Generic;
using UnityEngine;

public interface ICharacterLogic
{
    void Execute(FieldMapObject activeArea, Unit player, List<Unit> targets);
    void SetSeachRadius(float radius);
    void SetPositionTarget(Vector2 position);
    bool isIdle { get; }
}