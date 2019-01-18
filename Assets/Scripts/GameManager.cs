using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NetworkMode
{
    OFFLINE,
    ONLINE
}
public enum GameUpdate
{
    START,
    INGAME,
    END,
}
public enum GameMode
{
    HP,
    DAMAGE
}
/// <summary>
/// No Syncronized Class
/// </summary>
public class GameManager : MonoBehaviour
{
    public GameUpdate gameUpdate = GameUpdate.START;
    public NetworkMode networkMode = NetworkMode.OFFLINE;
    public GameMode gameMode = GameMode.DAMAGE;

    public static GameManager instance;
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
    public void PlayerUpdated()
    {
        if (networkMode == NetworkMode.OFFLINE)
            return;

        count++;
        if(count>=2)
        {
            NetworkManager.instance.PlayerReady(myPnum);
        }
    }

}
