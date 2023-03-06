using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameStates
{
    public enum StateTypes
    {
        INGAME,
        PREGAME,
        CINEMATIC,
        MENU
    }
    public abstract StateTypes stateType { get; }
    public abstract void EnterState(GameStateManager gameStateManager);
    public abstract void UpdateState(GameStateManager gameStateManager);
}
