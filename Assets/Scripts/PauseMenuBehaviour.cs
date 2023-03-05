using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject pauseMenuPanel;

    private GameStats gameStats;

    // Start is called before the first frame update
    void Start()
    {
        // set the panel within this object to be inactive
        pauseMenuPanel.SetActive(false);
        gameStats = GameObject.Find("GameManager").GetComponent<GameStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Menu"))
        {
            Debug.Log("Menu pressed");
            if (!pauseMenuPanel.activeSelf)
            {
                //if the menu is not active yet, set it to be active
                pauseMenuPanel.SetActive(true);
                //TODO: pause the game
                gameStats.PauseGame();
            }
            else
            {
                pauseMenuPanel.SetActive(false);
                gameStats.ResumeGame();
            }
            
        }

    }
}
