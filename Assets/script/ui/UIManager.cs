using UnityEngine;
using pixelflag.UI;

public class UIManager : MonoBehaviour
{
    public GameObject title;
    public GameObject youLose;
    public GameObject youWin;
    public GameObject youWin2;

    public TextObject centerText;
    public TextObject centerText2;

    public Gauge hpGauge;
    public Gauge expGauge;

    public TextObject hpName;
    public TextObject expName;

    public TextObject levelName;
    public TextObject levelNum;

    public Sprite flagSprite;
    public Sprite dotSprite;
    public SpriteRenderer[] flagPosition;

    private int count;
    private bool showLaptime;
    private GameObject centerObject;

    public void Initialize()
    {
        hpGauge.Initialize();
        expGauge.Initialize();

        hpName.Initialize();
        expName.Initialize();

        levelName.Initialize();
        levelNum.Initialize();

        centerText.Initialize();
        centerText2.Initialize();

        HideCenter();
    }

    public void ChangeFlag(int index)
    {
        foreach(SpriteRenderer dot in flagPosition)
        {
            dot.sprite = dotSprite;
        }
        flagPosition[index].sprite = flagSprite;
    }

    public void PlayerLifeUpdate(int life, int max)
    {
        hpGauge.SetNormal((float)life / max);
    }

    public void PlayerExpUpdate(int exp, int max)
    {
        expGauge.SetNormal((float)exp / max);
    }

    public void PlayerLevelUpDate(int level, bool isMaxLevel)
    {
        levelNum.SetText(level.ToString());
    }

    public void Excute()
    {
        count--;
        if(count == 0 && showLaptime)
        {
            showLaptime = false;
            HideCenter();
        }
    }

    public void GameStart()
    {
        ClearCenterObj();
        centerObject = Instantiate(title, transform);
    }

    public void GameOver()
    {
        ClearCenterObj();
        centerObject = Instantiate(youLose,transform);
    }

    public void GameClear()
    {
        ClearCenterObj();
        centerObject = Instantiate(youWin, transform);
    }

    public void GameClearForTime(string time)
    {
        ClearCenterObj();
        centerObject = Instantiate(youWin2, transform);
    }

    public void RapTime(string time)
    {
        centerText2.gameObject.SetActive(true);
        centerText2.SetText(time);
        showLaptime = true;
        count = 180;
    }

    public void Pause()
    {
        centerText.gameObject.SetActive(true);
        centerText.SetText("PAUSE");
    }

    public void HideCenter()
    {
        ClearCenterObj();
        centerText.gameObject.SetActive(false);
        centerText2.gameObject.SetActive(false);
    }

    private void ClearCenterObj()
    {
        if (centerObject != null)
        {
            Destroy(centerObject);
            centerObject = null;
        }
    }
}
