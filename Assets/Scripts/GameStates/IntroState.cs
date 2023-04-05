using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroState : GameStates
{
    public override StateTypes stateType { get; } = StateTypes.CINEMATIC;

    private GameObject currPlayerUI;
    private GameObject gameUI;
    private GameObject blackScreen;
    private StadiumCamera introCamera;

    public IntroState(GameObject currPlayerUI, GameObject gameUI, StadiumCamera introCamera, GameObject blackScreen)
    {
        this.currPlayerUI = currPlayerUI;
        this.gameUI = gameUI;
        this.introCamera = introCamera;
        this.blackScreen = blackScreen;
    }

    public override void EnterState(GameStateManager gameStateManager) 
    {
        gameStateManager.StartCoroutine(introCamera.IntroPan(delegate { CompletedIntroCameraPan(gameStateManager); }));
    }
    public override void UpdateState(GameStateManager gameStateManager) 
    { 
        if (Input.GetButtonDown("Menu"))
        {
            gameStateManager.SwitchState(gameStateManager.pauseState);
        }
    }

    private void CompletedIntroCameraPan(GameStateManager gameStateManager)
    {
        currPlayerUI.SetActive(true);

        gameUI.SetActive(true);
        blackScreen.SetActive(false);

        gameStateManager.SwitchState(gameStateManager.countdownState);
    }
}
