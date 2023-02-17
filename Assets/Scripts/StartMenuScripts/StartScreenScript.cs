using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartScreenScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject coverPanel;

    [SerializeField]
    private GameObject gamemodePanel;
    [SerializeField]
    private Button tutorialButton;
    [SerializeField]
    private Button practiceButton;
    [SerializeField]
    private Button twoPlayerButton;
    [SerializeField]
    private Button fourPlayerButton;
    private List<Button> gamemodeButtons;
    private float gamemodeHoldTime = 0.5f;
    private float gamemodeCurrHoldTime = 0;
    private int gamemodeIndex = 0;

    [SerializeField]
    private GameObject loadoutPanel;
    [SerializeField]
    private GameObject playerLoadoutMenu;
    [SerializeField]
    private List<ScriptableObject> weaponScriptableObjects;
    private List<PlayerLoadoutMenu> loadoutMenuScripts = new List<PlayerLoadoutMenu>();
    private bool loadingLoadoutMenu = false;

    private ButtonColors buttonColors;
    private int numPlayers = 1;
    private MenuTypes currentMenu = MenuTypes.CoverMenu;

    private List<List<float>> twoPlayerMenuLocations = new List<List<float>>
    {
        new List<float>
        {
            0, 0, 0.5f, 1
        },
        new List<float>
        {
            0.5f, 0, 1, 1
        }
    };
    private List<List<float>> fourPlayerMenuLocations = new List<List<float>>
    {
        new List<float>
        {
            0, 0.5f, 0.5f, 1
        },
        new List<float>
        {
            0.5f, 0.5f, 1, 1
        },
        new List<float>
        {
            0, 0, 0.5f, 0.5f
        },
        new List<float>
        {
            0.5f, 0, 1, 0.5f
        }
    };
    public enum MenuTypes
    {
        CoverMenu,
        GamemodeMenu,
        LoadoutMenu,
        MapMenu

    }

    void Start()
    {
        buttonColors = GameObject.Find("StartScreenManager").GetComponent<ButtonColors>();

        tutorialButton.onClick.AddListener(SelectedTutorial);
        practiceButton.onClick.AddListener(SelectedPractice);
        twoPlayerButton.onClick.AddListener(SelectedTwoPlayerMode);
        fourPlayerButton.onClick.AddListener(SelectedFourPlayerMode);

        gamemodePanel.SetActive(false);
        loadoutPanel.SetActive(false);

        gamemodeButtons = new List<Button> { tutorialButton, practiceButton, twoPlayerButton, fourPlayerButton };
        gamemodeButtons[gamemodeIndex].GetComponent<Image>().color = buttonColors.hoverColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentMenu == MenuTypes.CoverMenu)
        {
            CoverMenuInput();
        } else if (currentMenu == MenuTypes.GamemodeMenu)
        {
            GamemodeMenuInput();
        } else
        {
            LoadoutMenuInput();
        }
    }

    private void CoverMenuInput()
    {
        if (Input.GetButtonDown("Jump1") || Input.GetButtonDown("Jump2"))
        {
            TransitionTogamemodeMenu();
            //SceneManager.LoadScene("Main Arena");
        }
    }

    private void GamemodeMenuInput()
    {
        if (Input.GetAxis("Horizontal1") >= 0.9f)
        {
            gamemodeCurrHoldTime += Time.deltaTime;
            if (gamemodeCurrHoldTime >= gamemodeHoldTime)
            {
                int prevIndex = gamemodeIndex;
                gamemodeIndex += 1;
                if (gamemodeIndex == 4)
                {
                    gamemodeIndex = 0;
                }

                gamemodeButtons[gamemodeIndex].GetComponent<Image>().color = buttonColors.hoverColor;
                gamemodeButtons[prevIndex].GetComponent<Image>().color = buttonColors.normalColor;
                gamemodeCurrHoldTime = 0;

                Debug.Log(gamemodeIndex);
            }
        }
        else if (Input.GetAxis("Horizontal1") <= -0.9f)
        {
            gamemodeCurrHoldTime += Time.deltaTime;
            if (gamemodeCurrHoldTime >= gamemodeHoldTime)
            {
                int prevIndex = gamemodeIndex;
                gamemodeIndex -= 1;
                if (gamemodeIndex < 0)
                {
                    gamemodeIndex = 3;
                }

                gamemodeButtons[gamemodeIndex].GetComponent<Image>().color = buttonColors.hoverColor;
                gamemodeButtons[prevIndex].GetComponent<Image>().color = buttonColors.normalColor;
                gamemodeCurrHoldTime = 0;

                Debug.Log(gamemodeIndex);
            }
        }

        if (Input.GetButtonDown("Jump1"))
        {
            gamemodeButtons[gamemodeIndex].onClick.Invoke();
            gamemodeButtons[gamemodeIndex].GetComponent<Image>().color = buttonColors.selectedColor;
        }
    }

    private void LoadoutMenuInput()
    {
        if (!loadingLoadoutMenu)
        {
            foreach (PlayerLoadoutMenu script in loadoutMenuScripts)
            {
                script.LoadoutInput();
            }
        }
    }

    private void SelectedTutorial()
    {
        Debug.Log("Selected tutorial. Tutorial Not Implemented yet.");
    }

    private void SelectedPractice()
    {
        Debug.Log("Selected practice. Practice Not Implemented yet.");
    }

    private void SelectedTwoPlayerMode()
    {
        numPlayers = 2;
        TransitionToLoadoutMenu();
    }

    private void SelectedFourPlayerMode()
    {
        numPlayers = 4;
        TransitionToLoadoutMenu();
    }

    private void TransitionTogamemodeMenu()
    {
        gamemodePanel.SetActive(true);
        currentMenu = MenuTypes.GamemodeMenu;
        coverPanel.SetActive(false);
    }

    private void TransitionToLoadoutMenu()
    {
        loadingLoadoutMenu = true;
        loadoutPanel.SetActive(true);
        currentMenu = MenuTypes.LoadoutMenu;
        gamemodePanel.SetActive(false);


        if (numPlayers == 1)
        {
            GameObject menu = GameObject.Instantiate(playerLoadoutMenu);
            menu.transform.SetParent(loadoutPanel.transform);
            menu.name = "Player 1 Loadout Menu";
            menu.GetComponent<PlayerLoadoutMenu>().playerNum = 1;
            RectTransform rTransform = menu.GetComponent<RectTransform>();
            rTransform.offsetMin = Vector2.zero;
            rTransform.offsetMax = Vector2.zero;

            loadoutMenuScripts.Add(menu.GetComponent<PlayerLoadoutMenu>());
        }
        else if (numPlayers == 2)
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject menu = GameObject.Instantiate(playerLoadoutMenu);
                menu.transform.SetParent(loadoutPanel.transform);
                menu.name = "Player " + (i + 1) + " Loadout Menu";
                menu.GetComponent<PlayerLoadoutMenu>().playerNum = i + 1;
                RectTransform rTransform = menu.GetComponent<RectTransform>();
                rTransform.anchorMin = new Vector2(twoPlayerMenuLocations[i][0], twoPlayerMenuLocations[i][1]);
                rTransform.anchorMax = new Vector2(twoPlayerMenuLocations[i][2], twoPlayerMenuLocations[i][3]);
                rTransform.offsetMin = Vector2.zero;
                rTransform.offsetMax = Vector2.zero;

                loadoutMenuScripts.Add(menu.GetComponent<PlayerLoadoutMenu>());
            }
        } 
        else if (numPlayers == 4)
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject menu = GameObject.Instantiate(playerLoadoutMenu);
                menu.transform.SetParent(loadoutPanel.transform);
                menu.name = "Player " + (i + 1) + " Loadout Menu";
                menu.GetComponent<PlayerLoadoutMenu>().playerNum = i + 1;
                RectTransform rTransform = menu.GetComponent<RectTransform>();
                rTransform.anchorMin = new Vector2(fourPlayerMenuLocations[i][0], fourPlayerMenuLocations[i][1]);
                rTransform.anchorMax = new Vector2(fourPlayerMenuLocations[i][2], fourPlayerMenuLocations[i][3]);
                rTransform.offsetMin = Vector2.zero;
                rTransform.offsetMax = Vector2.zero;

                loadoutMenuScripts.Add(menu.GetComponent<PlayerLoadoutMenu>());
            }
        }

        loadingLoadoutMenu = false;
    }
}
