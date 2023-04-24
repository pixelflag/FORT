using UnityEngine;

public class FormationManager : DIMonoBehaviour
{
    [SerializeField]
    private int[] eyeRadiusTable = default;
    [SerializeField]
    private RetainerTable[] formations = default;

    public int formationIndex { get; private set; }
    private int currentPositionIndex = 0;

    public void Reset()
    {
        formationIndex = 0;
    }

    public void ResetRetainerFormation()
    {
        currentPositionIndex = 0;
        foreach (Buddies re in objects.buddies)
        {
            Vector2 pos = formations[formationIndex].GetPosition(currentPositionIndex);
            re.moveLogic.SetPositionTarget(pos);
            re.moveLogic.SetSeachRadius(GetCurrentFormationEyeRadius());
            currentPositionIndex++;
        }
    }

    public void ChangeFormation()
    {
        formationIndex = formations.Length <= formationIndex + 1 ? 0 : formationIndex + 1;
        ResetRetainerFormation();
    }

    public Vector2 GetCurrentFormationPosition()
    {
        Vector2 pos = formations[formationIndex].GetPosition(currentPositionIndex);
        currentPositionIndex++;
        return pos;
    }

    public int GetCurrentFormationEyeRadius()
    {
        return eyeRadiusTable[formationIndex];
    }
}
