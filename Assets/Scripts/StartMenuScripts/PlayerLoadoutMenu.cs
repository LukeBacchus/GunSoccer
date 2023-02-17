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
    private List<Button> buttons;

    private int selectedIndex = 0;
    private float holdTime = 0.5f;
    private float currHoldTime = 0;
    private int firstVisible = 0;
    private int lastVisible = 0;
    private float widthOffset = 0;
    private bool offsetOnRight = true;
    private float cellWidth = 105;

    private ButtonColors buttonColors;

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
        buttonColors = GameObject.Find("StartScreenManager").GetComponent<ButtonColors>();

        rifleButton.onClick.AddListener(SelectRifle);
        sniperButton.onClick.AddListener(SelectSniper);
        smgButton.onClick.AddListener(SelectSmg);
        shotgunButton.onClick.AddListener(SelectShotgun);
        grenadeLauncherButton.onClick.AddListener(SelectGrenadeLauncher);

        buttons = new List<Button> { rifleButton, sniperButton, smgButton, shotgunButton, grenadeLauncherButton };
        buttons[selectedIndex].GetComponent<Image>().color = buttonColors.selectedColor;
        weaponGrid.offsetMin = Vector2.zero;

        Canvas.ForceUpdateCanvases();
        lastVisible = (int)Mathf.Floor(viewport.rect.width / cellWidth) - 1;
        widthOffset = viewport.rect.width % cellWidth;
    }

    public void LoadoutInput()
    {
        if (Input.GetButtonDown("Jump" + (playerNum).ToString()))
        {
            ToggleReady();
            Debug.Log("Ready? " + ready);
        }

        if (!ready)
        {
            if (Input.GetAxis("Horizontal" + (playerNum).ToString()) >= 0.9f)
            {
                currHoldTime += Time.deltaTime;
                if (currHoldTime >= holdTime)
                {
                    int prevIndex = selectedIndex;
                    if (selectedIndex < 4)
                    {
                        selectedIndex += 1;
                        if (selectedIndex > lastVisible)
                        {
                            MoveGridRight();
                        }
                    }

                    UpdateButtonColors(prevIndex, selectedIndex);
                    currHoldTime = 0;

                    Debug.Log(selectedIndex);
                }
            }
            else if (Input.GetAxis("Horizontal" + (playerNum).ToString()) <= -0.9f)
            {
                currHoldTime += Time.deltaTime;
                if (currHoldTime >= holdTime)
                {
                    int prevIndex = selectedIndex;
                    if (selectedIndex > 0)
                    {
                        selectedIndex -= 1;
                        if (selectedIndex < firstVisible)
                        {
                            MoveGridLeft();
                        }
                    }

                    UpdateButtonColors(prevIndex, selectedIndex);
                    currHoldTime = 0;

                    Debug.Log(selectedIndex);
                }
            }
            else
            {
                currHoldTime = 0;
            }

            if (Input.GetButtonDown("Fire1" + (playerNum).ToString()))
            {
                UpdateSelectedButtonColors(true);
                currentSelection = (WeaponType)selectedIndex;
                UpdateSelectedButtonColors(false);
                Debug.Log("Selected " + currentSelection.ToString());
            }
        }
    }

    public void UpdateTitle()
    {
        title.text = "Player " + playerNum + " Loadout";
    }

    private void UpdateSelectedButtonColors(bool removeSelect)
    {
        if (removeSelect)
        {
            buttons[(int)currentSelection].GetComponent<Image>().color = buttonColors.normalColor;
        }
        else
        {
            buttons[selectedIndex].GetComponent<Image>().color = buttonColors.selectedColor;
        }
    }

    private void UpdateButtonColors(int prevIndex, int currIndex)
    {
        if (currIndex != (int)currentSelection)
        {
            buttons[currIndex].GetComponent<Image>().color = buttonColors.hoverColor;
        }

        if (prevIndex != (int)currentSelection && prevIndex != currIndex)
        {
            buttons[prevIndex].GetComponent<Image>().color = buttonColors.normalColor;
        }
    }

    private void MoveGridRight()
    {
        weaponGrid.position += new Vector3(offsetOnRight ? widthOffset - cellWidth : -cellWidth, 0, 0);
        offsetOnRight = false;

        firstVisible += 1;
        lastVisible += 1;
    }

    private void MoveGridLeft()
    {
        weaponGrid.position += new Vector3(!offsetOnRight ? cellWidth - widthOffset : cellWidth, 0, 0);
        offsetOnRight = true;

        firstVisible -= 1;
        lastVisible -= 1;
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
