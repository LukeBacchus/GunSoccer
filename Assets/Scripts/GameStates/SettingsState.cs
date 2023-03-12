using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsState : GameStates
{
    public override StateTypes stateType { get; } = StateTypes.MENU;

    private List<bool> playersInSettings = new List<bool>();

    public SettingsState(int numPlayers)
    {
        for (int i = 0; i < numPlayers; i++)
        {
            playersInSettings.Add(false);
        }
    }

    public override void EnterState(GameStateManager gameStateManager) 
    {
        PauseGame();
    }

    public override void UpdateState(GameStateManager gameStateManager) 
    {
        bool canExit = false;
        for (int i = 0; i < playersInSettings.Count; i++)
        {
            canExit |= !playersInSettings[i];
        }

        if (canExit)
        {
            ResumeGame();
            gameStateManager.SwitchState(gameStateManager.prevState);
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
