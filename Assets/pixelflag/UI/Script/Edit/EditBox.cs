using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(SpriteRenderer))]
public class EditBox : MonoBehaviour
{
    public Vector2Int size;

#if UNITY_EDITOR
    private void Update()
    {
        GetComponent<SpriteRenderer>().size = size;
    }
#endif
}
