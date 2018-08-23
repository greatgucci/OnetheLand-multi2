using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayMode
{
    WAITING,
    ONLINE
}
public enum GameUpdate
{
    START,
    GAMING,
    END,
}
/// <summary>
/// No Syncronized Class
/// </summary>
public class PlayerManager : MonoBehaviour
{
    public GameUpdate gameUpdate = GameUpdate.START;
    public PlayMode playMode = PlayMode.WAITING;
    public static PlayerManager instance;
    private void Awake()
    {
        instance = this;
    }

    public PlayerData Local;
    public PlayerData Opponent;
    public int myPnum;

    
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

    int count;
    public void Updated()
    {
        count++;
        if(count>=2)
        {
            NetworkManager.instance.PlayerReady(myPnum);
        }
    }

}
