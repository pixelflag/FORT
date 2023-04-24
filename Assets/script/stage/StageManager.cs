using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : DIMonoBehaviour
{
    [SerializeField]
    private GameObject[] stageObjectContainers = default;

    private List<StageObjectData> stageData;
    public bool bossDead { get; private set; }

    private SpawnLogic spawnLogic;
    private FormationManager formation;
    private EventManager eventManager = default;

    public int currentStage = 0;

    public Player player { get; private set; }

    private CameraObject mainCamera;
    private UIManager ui;

    public void Initialize(CameraObject mainCamera, UIManager ui)
    {
        this.mainCamera = mainCamera;
        this.ui = ui;

        spawnLogic = new SpawnLogic();
        stageData = new List<StageObjectData>();
        formation = GetComponent<FormationManager>();

        eventManager = GetComponent<EventManager>();
        eventManager.Initialize();
        eventManager.OnEventHit = OnEventHit;

        CreateStageData();
    }

    public void Excute()
    {
        eventManager.Excute();

        if(player.isDead == false)
        {
            objects.player.HealProgress();
            /*
            foreach (Buddies re in objects.buddies)
                re.HealProgress();
            */
        }
    }    

    private void CreateStageData()
    {
        foreach (GameObject container in stageObjectContainers)
        {
            StageObjectData data = new StageObjectData();
            data.Inisialize();

            for (int i = 0; i < container.transform.childCount; i++)
            {
                GameObject obj = container.transform.GetChild(i).gameObject;
                MapObject me = obj.GetComponent<MapObject>();
                if (me != null)
                    data.AddMapObject(me.GetData());
            }
            stageData.Add(data);
        }

        foreach (GameObject container in stageObjectContainers)
        {
            Destroy(container);
        }
    }

    public void Reset()
    {
        bossDead = false;

        foreach (StageObjectData data in stageData)
        {
            data.FlushObjects();
        }

        formation.Reset();
        ui.ChangeFlag(formation.formationIndex);

        currentStage = 0;
        CreateStage(currentStage);

        eventManager.ResetAll();

        SetUpPlayer();
    }

    private void SetUpPlayer()
    {
        if (player != null) Destroy(player);
        player = creater.CreatePlayer(new Vector3(0, 48, 0));
        player.OnCommand = ChangeFormation;

        eventManager.SetPlayer(player);
        mainCamera.SetTarget(player.gameObject);

        ui.PlayerLifeUpdate(player.battle.lifeLogic.current, player.battle.lifeLogic.max);
        ui.PlayerExpUpdate(player.battle.exp, player.battle.nextExp);
        ui.PlayerLevelUpDate(player.battle.level, false);

        player.battle.lifeLogic.OnLifeUpdate = ui.PlayerLifeUpdate;
        player.battle.OnLevelUp = ui.PlayerLevelUpDate;
        player.battle.OnExpUpdate = ui.PlayerExpUpdate;
    }

    public void CreateStage(int stage)
    {
        if (stageData[stage].isCreated == true) return;
        stageData[stage].isCreated = true;

        foreach (MapObjectData data in stageData[stage].objectData)
        {
            switch(data.type)
            {
                case 0: // Enemy
                    stageData[stage].objects.Add(CreateEnemy(data));
                    break;
                case 1: // Statue
                    stageData[stage].objects.Add(CreateItem(data));
                    break;
            }
        }
        formation.ResetRetainerFormation();
    }
    private Character CreateEnemy(MapObjectData data)
    {
        return null;


        // 旧システム　あとでまとめて消す。
        /*
        Character obj = creater.CreateEnemy(data.enemyName, data.position);
        obj.OnCharacterDead = EnemyDead;
        return obj;
        */
    }

    private void EnemyDead(Character obj)
    {
        /*
        int power = (int)obj.battle.attackPower;

        // objectManager.player.battle.Heal(power);
        // ui.PlayerLifeUpdate(player.battle.life, player.battle.maxLife);

        foreach (Character re in objectManager.retainers)
        {
            re.battle.Heal(power);
        }
        */

        if (obj.objectType == ObjectType.Enemy)
            if (obj.battle.level == 12)
                bossDead = true;
    }

    private MassObject CreateItem(MapObjectData data)
    {
        MassObject obj;
        switch (data.level)
        {
            case 0:
                obj = creater.Create(ObjectName.ItemStatue, data.position);
                Statue statue = obj.GetComponent<Statue>();
                statue.onOpen = OpenStatue;
                return obj;
            default:
                throw new System.Exception("not found.");
        }
    }

    private void OpenStatue(Vector3 position)
    {
        int[] list = spawnLogic.GetRetainerSpawnList(currentStage, 1);
        for (int i = 0; i < list.Length; i++)
        {
            AddRetainer(list[i], position);
        }
    }

    private void AddRetainer(int level, Vector3 initPosition)
    {
        /*
        Retainer retainer = creater.CreateRetainer(level, ObjectName.Retainer, initPosition);
        formation.ResetRetainerFormation();
        retainer.OnCharacterDead = RetainerDead;
        */
    }

    private void RetainerDead(Character obj)
    {
        formation.ResetRetainerFormation();
    }

    private void OnEventHit(string eventName)
    {
        switch (eventName)
        {
            case "stage1":
                if (OnStartGame != null) OnStartGame();
                ChangeStage(1, BgmType.Field1);
                break;
            case "stage2":
                ChangeStage(2, BgmType.Field2);
                break;
            case "stage3":
                ChangeStage(3, BgmType.Field3);
                break;
            case "stage4":
                ChangeStage(4, BgmType.Field4);
                break;
            case "stage5":
                ChangeStage(5, BgmType.Castel);
                break;
            case "boss":
                sound.BGMFadeOutAndPlayIn(BgmType.LastBattle);
                break;
        }
    }

    private void ChangeStage(int stage, BgmType bgm)
    {
        currentStage = stage;
        CreateStage(currentStage);
        sound.BGMFadeOutAndPlayIn(bgm);

    }

    public delegate void StartGameDelegate();
    public StartGameDelegate OnStartGame;

    private void ChangeFormation()
    {
        formation.ChangeFormation();
        ui.ChangeFlag(formation.formationIndex);
//        player.ChangeFlag(formation.formationIndex);
        sound.PlayOneShotOnChannel(4,SeType.Change, 0.5f);
    }
}