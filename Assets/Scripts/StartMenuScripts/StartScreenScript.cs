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
    private Button controlsButton;
    [SerializeField]
    private Button settingsButton;
    private MenuSelectionHelper gameModeSelector;

    [SerializeField]
    private GameObject loadoutPanel;
    [SerializeField]
    private List<Weapons> weaponScriptableObjects;
    private PlayerLoadoutMenu loadoutMenuScript;
    private bool loadingLoadoutMenu = false;

    [SerializeField]
    private GameObject mapPanel;
    [SerializeField]
    private Button stadiumButton1;
    [SerializeField]
    private Button stadiumButton2;
    private MenuSelectionHelper mapSelector;

    [SerializeField]
    private GameObject settingsPanel;

    [SerializeField]
    private GameObject controlsPanel;

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
        MapMenu,
        Settings,
        Controls
    }

    void Start()
    {
        // Gamemode menu button onclick events
        twoPlayerButton.onClick.AddListener(SelectedTwoPlayerMode);
        fourPlayerButton.onClick.AddListener(SelectedFourPlayerMode);
        controlsButton.onClick.AddListener(SelectedControls);
        settingsButton.onClick.AddListener(SelectedSettings);

        // Map menu button onclick events
        stadiumButton1.onClick.AddListener(delegate { LoadMap(stadiumButton1.name); });
        stadiumButton2.onClick.AddListener(delegate { LoadMap(stadiumButton2.name); });

        coverPanel.SetActive(true);
        gamemodePanel.SetActive(false);
        loadoutPanel.SetActive(false);
        mapPanel.SetActive(false);
        settingsPanel.SetActive(false);
        controlsPanel.SetActive(false);

        settingsPanel.GetComponent<SettingsManager>().SetupSelector();

        List<List<GameObject>> gamemodeButtons = new List<List<GameObject>> { new List<GameObject> { twoPlayerButton.gameObject }, 
            new List<GameObject> { fourPlayerButton.gameObject }, new List<GameObject> { controlsButton.gameObject }, 
            new List<GameObject> { settingsButton.gameObject } };
        gameModeSelector = new MenuSelectionHelper(gamemodeButtons, 0, 4, new List<int> { 1, 2, 3, 4 });

        List<List<GameObject>> mapButtons = new List<List<GameObject>> { new List<GameObject> { stadiumButton1.gameObject, stadiumButton2.gameObject } };
        mapSelector = new MenuSelectionHelper(mapButtons, 1, 0, new List<int> { 1, 2, 3, 4 });
    }

    // Update is called once per frame
    void Update()
    {
        if (currentMenu == MenuTypes.CoverMenu)
        {
            CoverMenuInput();
        }
        else if (currentMenu == MenuTypes.GamemodeMenu)
        {
            GamemodeMenuInput();

            if (BackInput())
            {
                gamemodePanel.SetActive(false);
                coverPanel.SetActive(true);
                currentMenu = MenuTypes.CoverMenu;
            }
        }
        else if (currentMenu == MenuTypes.LoadoutMenu)
        {
            LoadoutMenuInput();

            if (BackInput())
            {
                loadoutPanel.SetActive(false);
                DestroyLoadoutMenus();
                TransitionToGamemodeMenu();
            }
        }
        else if (currentMenu == MenuTypes.MapMenu)
        {
            MapMenuInput();

            if (BackInput())
            {
                mapPanel.SetActive(false);
                currentMenu = MenuTypes.LoadoutMenu;
                foreach (PlayerLoadoutMenu loadoutMenu in loadoutMenuScripts)
                {
                    if (loadoutMenu.ready)
                    {
                        loadoutMenu.ToggleReady();
                    }
                }
            }
        }
        else if (currentMenu == MenuTypes.Controls)
        {
            if (BackInput())
            {
                controlsPanel.SetActive(false);
                TransitionToGamemodeMenu();
            }
        }
        else
        {
            SettingsInput();

            if (BackInput())
            {
                settingsPanel.SetActive(false);
                TransitionToGamemodeMenu();
            }
        }
    }

    private void CoverMenuInput()
    {
        if (Input.GetButtonDown("Jump1") || Input.GetButtonDown("Jump2") || Input.GetButtonDown("Jump3") || Input.GetButtonDown("Jump4"))
        {
            coverPanel.SetActive(false);
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
            gunsfx = GetComponent<StudioEventEmitter>();

            if (loadoutMenuScript.menuLoaded)
            {
                loadoutMenuScript.LoadoutInput();
            }

            if (loadoutMenuScript.ready)
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

    private bool BackInput()
    {
        if (Input.GetButtonDown("Back1") | Input.GetButtonDown("Back2") | Input.GetButtonDown("Back3") | Input.GetButtonDown("Back4"))
        {
            return true;
        }
        return false;
    }

    private void SettingsInput()
    {
        settingsPanel.GetComponent<SettingsManager>().SettingsInput();
    }

    private void SelectedSettings()
    {
        settingsPanel.SetActive(true);
        currentMenu = MenuTypes.Settings;
    }

    private void SelectedControls()
    {
        controlsPanel.SetActive(true);
        currentMenu = MenuTypes.Controls;
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
    }

    private void TransitionToLoadoutMenu()
    {
        loadingLoadoutMenu = true;
        loadoutPanel.SetActive(true);
        currentMenu = MenuTypes.LoadoutMenu;

        InitPlayerLoadoutMenu(numPlayers);
     

        loadingLoadoutMenu = false;
    }

    private void TransitionMapMenu()
    {
        mapPanel.SetActive(true);
        currentMenu = MenuTypes.MapMenu;
    }

    private void LoadMap(string sceneName)
    {
        for (int i = 1; i <= numPlayers; i++)
        {
            SpawnPlayer(i);
        }
        SceneManager.LoadScene(sceneName);
    }

    private void InitPlayerLoadoutMenu(int playerNums)
    {
        PlayerLoadoutMenu menuScript = loadoutPanel.GetComponentInChildren<PlayerLoadoutMenu>();

        //menu.transform.SetParent(loadoutPanel.transform);

        /* no need for per player stuff and transforming?
         * menu.name = "Player " + playerNum + " Loadout Menu";
        RectTransform rTransform = menu.GetComponent<RectTransform>();
        rTransform.anchorMin = new Vector2(menuLocs[playerNum - 1][0], menuLocs[playerNum - 1][1]);
        rTransform.anchorMax = new Vector2(menuLocs[playerNum - 1][2], menuLocs[playerNum - 1][3]);
        rTransform.offsetMin = Vector2.zero;
        rTransform.offsetMax = Vector2.zero;*/

        // menuScript.weapons = weaponScriptableObjects;

        if (numPlayers == 2)
        {
            menuScript.playerNums = new List<int> { 1, 2 };
        }
        else
        {
            menuScript.playerNums = new List<int> { 1, 2, 3, 4 };
        }

        menuScript.Init();

        loadoutMenuScript = menuScript;
    }

    private void DestroyLoadoutMenus()
    {
        foreach (PlayerLoadoutMenu loadoutMenu in loadoutMenuScripts)
        {
            Destroy(loadoutMenu.gameObject);
        }
        loadoutMenuScripts = new List<PlayerLoadoutMenu>();
    }

    private void SpawnPlayer(int playerNum)
    {
        GameObject player = Object.Instantiate(playerPrefab);
        player.name = "Player " + (playerNum).ToString();
        PlayerStats playerStats = player.GetComponent<PlayerStats>();


        if (playerNum % 2 == 0)
        {
            playerStats.team = "Blue";
        }
        else
        {
            playerStats.team = "Red";
        }

        playerStats.playerNum = playerNum;
        playerStats.weapon = loadoutMenuScript.currentSelections[playerNum-1];
        GameObject Gun = Instantiate(playerStats.weapon.gunModel, playerStats.gunPos);
        Gun.transform.parent = playerStats.gunPos;

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