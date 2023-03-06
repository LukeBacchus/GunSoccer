using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroState : GameStates
{
    public override StateTypes stateType { get; } = StateTypes.CINEMATIC;

    private GameObject fourPlayerUI;
    private GameObject twoPlayerUI;
    private GameObject scoreBoard;
    private GameObject blackScreen;
    private StadiumCamera introCamera;

    public IntroState(GameObject twoPlayerUI, GameObject fourPlayerUI, GameObject scoreBoard, StadiumCamera introCamera, GameObject blackScreen)
    {
        this.fourPlayerUI = fourPlayerUI;
        this.twoPlayerUI = twoPlayerUI;
        this.scoreBoard = scoreBoard;
        this.introCamera = introCamera;
        this.blackScreen = blackScreen;
    }

    public override void EnterState(GameStateManager gameStateManager) 
    {
        gameStateManager.StartCoroutine(introCamera.IntroPan(delegate { CompletedIntroCameraPan(gameStateManager); }));
    }
    public override void UpdateState(GameStateManager gameStateManager) { }

    private void CompletedIntroCameraPan(GameStateManager gameStateManager)
    {
        if (gameStateManager.players.Count == 2)
        {
            twoPlayerUI.SetActive(true);
            fourPlayerUI.SetActive(false);
        }
        else
        {
            twoPlayerUI.SetActive(false);
            fourPlayerUI.SetActive(true);
        }

        scoreBoard.SetActive(true);
        blackScreen.SetActive(false);

        gameStateManager.SwitchState(gameStateManager.countdownState);
    }
}
