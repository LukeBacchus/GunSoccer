using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameStates
{
    public abstract void EnterState(GameStateManager gameStateManager);
    public abstract void UpdateState(GameStateManager gameStateManager);
}
