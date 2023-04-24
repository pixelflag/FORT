using UnityEngine;

public class CellScore
{
    public float Ascore;    // 距離スコア
    public float Bscore;    // 距離スコア
    public float Cscore;    // 地形スコア
    public float TotalScore => Ascore + Bscore + Cscore;

    public bool isRootCell;
    public bool isClose;
    public bool isChecked;
    public CellScore parent;

    public Vector2Int localLocation;
    public Vector2 localPosition;

    public CellScore(Vector2 localPosition, Vector2Int localLocation)
    {
        this.localLocation = localLocation;
        this.localPosition = localPosition;

        Ascore = 0;
        Bscore = 0;

        isClose = false;
        isChecked = false;
        isRootCell = false;

        parent = null;
    }

    public void RouteReset()
    {
        Ascore = 0;
        Bscore = 0;
        isClose = false;
        isChecked = false;
        isRootCell = false;
        parent = null;
    }
}