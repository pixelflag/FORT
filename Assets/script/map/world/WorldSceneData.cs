using UnityEngine;
using System;

[CreateAssetMenu(fileName = "WorldSceneData", menuName = "Data/Create WorldSceneData")]
public class WorldSceneData : ScriptableObject
{



}

[Serializable]
public struct WorldUnitState
{
    public int dataIndex;
    public Vector3 position;
}