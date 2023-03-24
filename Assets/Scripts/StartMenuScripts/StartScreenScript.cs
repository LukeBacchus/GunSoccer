using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using FMODUnity;


public class StartScreenScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject coverPanel;

    [SerializeField]
    private GameObject gamemodePanel;
    [SerializeField]
    private Button twoPlayerButton;
    [SerializeField]
    private Button fourPlayerButton;
    [SerializeField]
    private Button creditsButton;
    [SerializeField]
    private Button settingsButton;
    private MenuSelectionHelper gameModeSelector;

    [SerializeField]
    private GameObject loadoutPanel;
    [SerializeField]
    private GameObject playerLoadoutMenu;
    [SerializeField]
    private List<Weapons> weaponScriptableObjects;
    private List<PlayerLoadoutMenu> loadoutMenuScripts = new List<PlayerLoadoutMenu>();
    private bool loadingLoadoutMenu = false;

    [SerializeField]
    private GameObject mapPanel;
    [SerializeField]
    private Button stadiumButton1;
    [SerializeField]
    private Button stadiumButton2;
    private MenuSelectionHelper mapSelector;

    [SerializeField]
    private GameObject playerPrefab;

    private int numPlayers = 1;
    private MenuTypes currentMenu = MenuTypes.CoverMenu;

    public StudioEventEmitter gunsfx;

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
        // Gamemode menu button onclick events
        twoPlayerButton.onClick.AddListener(SelectedTwoPlayerMode);
        fourPlayerButton.onClick.AddListener(SelectedFourPlayerMode);
        creditsButton.onClick.AddListener(SelectedCredits);
        settingsButton.onClick.AddListener(SelectedSettings);

        // Map menu button onclick events
        stadiumButton1.onClick.AddListener(delegate { LoadMap(stadiumButton1.name); });
        stadiumButton2.onClick.AddListener(delegate { LoadMap(stadiumButton2.name); });

        coverPanel.SetActive(true);
        gamemodePanel.SetActive(false);
        loadoutPanel.SetActive(false);
        mapPanel.SetActive(false);

        List<List<Button>> gamemodeButtons = new List<List<Button>> { new List<Button> { twoPlayerButton }, new List<Button> { fourPlayerButton }, new List<Button> { creditsButton }, new List<Button>{ settingsButton } };
        gameModeSelector = new MenuSelectionHelper(gamemodeButtons, 0, 3, new List<int> { 1, 2, 3, 4 });

        List<List<Button>> mapButtons = new List<List<Button>> { new List<Button> { stadiumButton1, stadiumButton2 } };
        mapSelector = new MenuSelectionHelper(mapButtons, 1, 0, new List<int> { 1, 2, 3, 4 });
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
        } else if (currentMenu == MenuTypes.LoadoutMenu)
        {
            LoadoutMenuInput();
        } else
        {
            MapMenuInput();
        }
    }

    private void CoverMenuInput()
    {
        if (Input.GetButtonDown("Jump1") || Input.GetButtonDown("Jump2") || Input.GetButtonDown("Jump3") || Input.GetButtonDown("Jump4"))
        {
            TransitionToGamemodeMenu();
        }
    }

    private void GamemodeMenuInput()
    {
        gameModeSelector.SelectionInput();
        if (gameModeSelector.Select())
        {
            gameModeSelector.InvokeSelection();
        }
    }

    private void LoadoutMenuInput()
    {
        if (!loadingLoadoutMenu)
        {
            int numReady = 0;
            foreach (PlayerLoadoutMenu script in loadoutMenuScripts)
            {

                gunsfx = GetComponent<StudioEventEmitter>();

                if (script.menuLoaded)
                {
                    script.LoadoutInput();
                }
                numReady += script.ready ? 1 : 0;
            }

            if (numReady == numPlayers)
            {
                TransitionMapMenu();
            }
        }
    }

    private void MapMenuInput()
    {
        mapSelector.SelectionInput();
        if (mapSelector.Select())
        {
            mapSelector.InvokeSelection();
        }
    }

    private void SelectedCredits()
    {
        Debug.Log("Selected credits. Credits Not Implemented yet.");
    }

    private void SelectedSettings()
    {
        Debug.Log("Selected settings. Settings Not Implemented yet.");
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

    private void TransitionToGamemodeMenu()
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
            CreatePlayerLoadoutMenu(1, new List<List<float>> { new List<float> { 0, 0, 1, 1 } });
        }
        else if (numPlayers == 2)
        {
            for (int i = 0; i < 2; i++)
            {
                CreatePlayerLoadoutMenu(i + 1, twoPlayerMenuLocations);
            }
        } 
        else if (numPlayers == 4)
        {
            for (int i = 0; i < 4; i++)
            {
                CreatePlayerLoadoutMenu(i + 1, fourPlayerMenuLocations);
            }
        }

        loadingLoadoutMenu = false;
    }

    private void TransitionMapMenu()
    {
        mapPanel.SetActive(true);
        currentMenu = MenuTypes.MapMenu;
        loadoutPanel.SetActive(false);
    }

    private void LoadMap(string sceneName)
    {
        for (int i = 1; i <= numPlayers; i++)
        {
            SpawnPlayer(i);
        }
        SceneManager.LoadScene(sceneName);
    }

    private void CreatePlayerLoadoutMenu(int playerNum, List<List<float>> menuLocs)
    {
        GameObject menu = Instantiate(playerLoadoutMenu);
        menu.transform.SetParent(loadoutPanel.transform);
        menu.name = "Player " + playerNum + " Loadout Menu";
        RectTransform rTransform = menu.GetComponent<RectTransform>();
        rTransform.anchorMin = new Vector2(menuLocs[playerNum - 1][0], menuLocs[playerNum - 1][1]);
        rTransform.anchorMax = new Vector2(menuLocs[playerNum - 1][2], menuLocs[playerNum - 1][3]);
        rTransform.offsetMin = Vector2.zero;
        rTransform.offsetMax = Vector2.zero;

        PlayerLoadoutMenu menuScript = menu.GetComponent<PlayerLoadoutMenu>();
        menuScript.playerNum = playerNum;
        menuScript.weapons = weaponScriptableObjects;
        menuScript.Init();
        menuScript.UpdateTitle();

        loadoutMenuScripts.Add(menu.GetComponent<PlayerLoadoutMenu>());
    }

    private void SpawnPlayer(int playerNum)
    {
        GameObject player = Object.Instantiate(playerPrefab);
        player.name = "Player " + (playerNum).ToString();
        PlayerStats playerStats = player.GetComponent<PlayerStats>();


        if(playerNum % 2 == 0){
            playerStats.team = "Blue";
        } else {
            playerStats.team = "Red";
        }

        playerStats.playerNum = playerNum;
        playerStats.weapon = loadoutMenuScripts[playerNum - 1].currentSelection;

        if (numPlayers == 2)
        {
            playerStats.cam.rect = new Rect(playerNum == 1 ? 0 : 0.5f, 0, 0.5f, 1);
        }
        else if (numPlayers == 4)
        {
            playerStats.cam.rect = new Rect(playerNum % 2 == 1 ? 0 : 0.5f, playerNum >= 3 ? 0.5f : 0, 0.5f, 0.5f);
        }

        playerStats.AssignLayer(playerNum);
        DontDestroyOnLoad(player);
    }
}
