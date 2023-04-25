using UnityEngine;

public class UnitSkinLibrary : MonoBehaviour
{
    [SerializeField]
    private UnitSkin[] skins = default;
    public int Length => skins.Length;
    public UnitSkin Get(int index) => skins[index];
}
