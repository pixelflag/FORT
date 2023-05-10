using UnityEngine;

public class WorldMapCastle : PixelObject
{
    public WorldMapCastleData castelData { get; private set; }

    public void Initialize(WorldMapCastleData castelData)
    {
        this.castelData = castelData;
        x = castelData.location.x * Global.gridSize.x;
        y = -castelData.location.y * Global.gridSize.y;
    }
}
