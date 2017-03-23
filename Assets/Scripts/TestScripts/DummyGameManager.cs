using UnityEngine;
using System.Collections;
using ObjectTypes;

public class DummyGameManager : MonoBehaviour, IGameManager 
{
    GameStateType gameType = GameStateType.COMBATMODE;
    SystemDataType dataType = SystemDataType.GAMEMANAGER;

    public GameStateType Game_State
    {
        get{ return gameType;}
        set{ gameType = value; }
    }

    public SystemDataType System_Type
    {
        get{ return dataType;}
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
