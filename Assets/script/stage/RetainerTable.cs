using UnityEngine;
using UnityEditor;

public class RetainerTable : MonoBehaviour
{
    [SerializeField]
    private Vector2[] positionTable = default;

    [ContextMenu("Put Child Position")]
    void PutChildPosition()
    {
        Vector2[] table = new Vector2[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            table[i] = transform.GetChild(i).position;
        }
#if UNITY_EDITOR
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
