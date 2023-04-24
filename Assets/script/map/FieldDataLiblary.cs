using UnityEngine;

public class FieldDataLiblary : MonoBehaviour
{
    [SerializeField]
    private FieldMapData[] fileds = default;

    public FieldMapData GetFieldMapData(FieldMapName mapName)
    {
        return fileds[(int)mapName];
    }
}