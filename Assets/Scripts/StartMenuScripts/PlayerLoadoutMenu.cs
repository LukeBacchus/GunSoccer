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

    private float cellWidth = 105;
    private MenuSelectionHelper weaponSelector;

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

        List<Button> buttons = new List<Button> { rifleButton, sniperButton, smgButton, shotgunButton, grenadeLauncherButton };
        weaponGrid.offsetMin = Vector2.zero;

        Canvas.ForceUpdateCanvases();
        int lastVisible = (int)Mathf.Floor(viewport.rect.width / cellWidth) - 1;
        float widthOffset = viewport.rect.width % cellWidth;

        weaponSelector = new MenuSelectionHelper(buttons, 4, 0, lastVisible, weaponGrid, widthOffset, cellWidth, 0, playerNum);
    }

    public void LoadoutInput()
    {
        if (Input.GetButtonDown("Fire1" + (playerNum).ToString()))
        {
            ToggleReady();
            Debug.Log("Ready? " + ready);
        }

        if (!ready)
        {
            weaponSelector.HorizontalSelection();
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
        } else
        {
            rifleButton.onClick.AddListener(SelectRifle);
            sniperButton.onClick.AddListener(SelectSniper);
            smgButton.onClick.AddListener(SelectSmg);
            shotgunButton.onClick.AddListener(SelectShotgun);
            grenadeLauncherButton.onClick.AddListener(SelectGrenadeLauncher);
        }
    }

    private void SelectRifle()
    {
        currentSelection = WeaponType.AssaultRifle;
        Debug.Log("Selected AR");
    }

    private void SelectSniper()
    {
        currentSelection = WeaponType.SniperRifle;
        Debug.Log("Selected Sniper");
    }

    private void SelectSmg()
    {
        currentSelection = WeaponType.SMG;
        Debug.Log("Selected SMG");
    }

    private void SelectShotgun()
    {
        currentSelection = WeaponType.Shotgun;
        Debug.Log("Selected Shotgun");
    }

    private void SelectGrenadeLauncher()
    {
        currentSelection = WeaponType.GrenadeLauncher;
        Debug.Log("Selected GL");
    }

}
