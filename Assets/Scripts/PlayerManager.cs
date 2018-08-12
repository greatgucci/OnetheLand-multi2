using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayMode
{
    WAITING,
    ONLINE
}
/// <summary>
/// No Syncronized Class
/// </summary>
public class PlayerManager : MonoBehaviour
{
    public PlayMode playMode = PlayMode.WAITING;
    public static PlayerManager instance;
    private void Awake()
    {
        instance = this;
    }

    public PlayerData Local;
    public PlayerData Opponent;
    public int myPnum;

    int count;
    
    public PlayerData GetPlayerByNum(int i)
    {
        if(Local.playerNum ==i)
        {
            return Local;
        }else
        {
            return Opponent;
        }
    }
    public PlayerControl GetPlayerControlByNum(int i)
    {
        if (Local.playerNum == i)
        {
            return Local.GetComponentInParent<PlayerControl>();
        }
        else
        {
            return Opponent.GetComponentInParent<PlayerControl>();
        }
    }
}
