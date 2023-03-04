using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject pauseMenu;

    private GameStats gameStats;

    // Start is called before the first frame update
    void Start()
    {
        gameStats = GameObject.Find("GameManager").GetComponent<GameStats>();
        // in the beginning, if active, set it to be false
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("listening for menu...");
        if(Input.GetButtonDown("Menu"))
        {
            if (!pauseMenu.activeSelf)
            {
                //if the menu is not active yet, set it to be active
                pauseMenu.SetActive(true);
            }
            else
            {
                Debug.Log("Menu already open, do we want to implement pressing then closing it?");
            }
        }

    }
}
