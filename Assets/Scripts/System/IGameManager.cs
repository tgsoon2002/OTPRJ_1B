using UnityEngine;
using System.Collections;
using ObjectTypes;

public interface IGameManager : ISystemElement
{
    GameStateType Game_State
    {
        get;
        set;
    }

    SystemDataType System_Type
    {
        get;
    }
}
