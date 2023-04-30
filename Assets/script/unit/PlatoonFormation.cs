using UnityEngine;
using UnityEditor;

public class PlatoonFormation : MonoBehaviour
{
    [SerializeField]
    private Vector2[] positionTable;

#if UNITY_EDITOR
    [ContextMenu("Put Child Position")]
    void PutChildPosition()
    {
        positionTable = new Vector2[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            positionTable[i] = transform.GetChild(i).position;
        }

        EditorUtility.SetDirty(this);

#endif
    }

    public Vector2 GetPosition(int index)
    {
        index = positionTable.Length <= index ? positionTable.Length - 1 : index;
        index = index < 0 ? 0 : index;

        return positionTable[index];
    }
}