using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLoadoutMenu : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI title;
    [SerializeField]
    private RectTransform weaponGrid;
    [SerializeField]
    private RectTransform viewport;
    [SerializeField]
    private Button rifleButton;
    [SerializeField]
    private Button sniperButton;
    [SerializeField]
    private Button smgButton;
    [SerializeField]
    private Button shotgunButton;
    [SerializeField]
    private Button grenadeLauncherButton;

    private MenuSelectionHelper weaponSelector;
    private Color readyColor = new Color(0.05f, 1, 0, 0.3f);

    public int playerNum;
    public bool ready = false;
    public WeaponType currentSelection = WeaponType.AssaultRifle;

    public enum WeaponType
    {
        AssaultRifle,
        SniperRifle,
        SMG,
        Shotgun,
        GrenadeLauncher
    }

    // Start is called before the first frame update
    void Start()
    {
        rifleButton.onClick.AddListener(SelectRifle);
        sniperButton.onClick.AddListener(SelectSniper);
        smgButton.onClick.AddListener(SelectSmg);
        shotgunButton.onClick.AddListener(SelectShotgun);
        grenadeLauncherButton.onClick.AddListener(SelectGrenadeLauncher);

        List<List<Button>> buttons = new List<List<Button>> { new List<Button> { rifleButton, sniperButton, smgButton, shotgunButton, grenadeLauncherButton } };
        weaponGrid.offsetMin = Vector2.zero;
        viewport.offsetMin = Vector2.zero;

        Canvas.ForceUpdateCanvases();
        float cellWidth = rifleButton.gameObject.GetComponent<RectTransform>().rect.width + 5;
        int lastVisible = (int)Mathf.Floor(viewport.rect.width / cellWidth) - 1;
        float widthOffset = viewport.rect.width % cellWidth;

        weaponSelector = new MenuSelectionHelper(buttons, 4, 0, 0, lastVisible, weaponGrid, widthOffset, cellWidth, 0, 0, playerNum);
    }

    public void LoadoutInput()
    {
        if (Input.GetButtonDown("Fire1" + (playerNum).ToString()))
        {
            ToggleReady();
        }

        if (!ready)
        {
            weaponSelector.SelectionInput();
            if (weaponSelector.Select())
            {
                weaponSelector.InvokeSelection();
            }
        }
    }

    public void UpdateTitle()
    {
        title.text = "Player " + playerNum + " Loadout";
    }

    private void ToggleReady()
    {
        ready = !ready;
        if (ready)
        {
            rifleButton.onClick.RemoveAllListeners();
            sniperButton.onClick.RemoveAllListeners();
            smgButton.onClick.RemoveAllListeners();
            shotgunButton.onClick.RemoveAllListeners();
            grenadeLauncherButton.onClick.RemoveAllListeners();

            GetComponent<Image>().color = readyColor;
        } else
        {
            rifleButton.onClick.AddListener(SelectRifle);
            sniperButton.onClick.AddListener(SelectSniper);
            smgButton.onClick.AddListener(SelectSmg);
            shotgunButton.onClick.AddListener(SelectShotgun);
            grenadeLauncherButton.onClick.AddListener(SelectGrenadeLauncher);

            GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }
    }

    private void SelectRifle()
    {
        currentSelection = WeaponType.AssaultRifle;
    }

    private void SelectSniper()
    {
        currentSelection = WeaponType.SniperRifle;
    }

    private void SelectSmg()
    {
        currentSelection = WeaponType.SMG;
    }

    private void SelectShotgun()
    {
        currentSelection = WeaponType.Shotgun;
    }

    private void SelectGrenadeLauncher()
    {
        currentSelection = WeaponType.GrenadeLauncher;
    }

}
