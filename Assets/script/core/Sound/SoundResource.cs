using System.Collections.Generic;
using UnityEngine;

public class SoundResource : MonoBehaviour
{
    public AudioClip Attack;
    public AudioClip BossDead;
    public AudioClip EnemyDead;
    public AudioClip ReatinerDead;
    public AudioClip Rescue;
    public AudioClip Click;
    public AudioClip Levelup;
    public AudioClip Pause;
    public AudioClip Lucky;
    public AudioClip Change;

    public AudioClip BGM_Catel;
    public AudioClip BGM_Clear;
    public AudioClip BGM_Feald1;
    public AudioClip BGM_Feald2;
    public AudioClip BGM_Feald3;
    public AudioClip BGM_Feald4;
    public AudioClip BGM_GameOver;
    public AudioClip BGM_LastBattle;

    public void Initialize()
    {
    }

    public AudioClip GetEffect(SeType type)
    {
        switch(type)
        {
            case SeType.Attack: return Attack;
            case SeType.BossDead: return BossDead;
            case SeType.EnemyDead: return EnemyDead;
            case SeType.ReatinerDead: return ReatinerDead;
            case SeType.Rescue: return Rescue;
            case SeType.Click: return Click;
            case SeType.Levelup: return Levelup;
            case SeType.Pause: return Pause;
            case SeType.Lucky: return Lucky;
            case SeType.Change: return Change;
        }
        return null;
    }

    public AudioClip GetBgm(BgmType type)
    {
        switch (type)
        {
            case BgmType.Castel: return BGM_Catel;
            case BgmType.Clear: return BGM_Clear;
            case BgmType.Field1: return BGM_Feald1;
            case BgmType.Field2: return BGM_Feald2;
            case BgmType.Field3: return BGM_Feald3;
            case BgmType.Field4: return BGM_Feald4;
            case BgmType.GameOver: return BGM_GameOver;
            case BgmType.LastBattle: return BGM_LastBattle;
        }
        return null;
    }
}
