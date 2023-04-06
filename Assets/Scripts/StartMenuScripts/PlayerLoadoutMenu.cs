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
    private List<Button> weaponButtons;


    public bool menuLoaded = false;
    public bool ready = false;
    private List<bool> readys = new List<bool> { false, false, false, false };
    private List<bool> justPressed = new List<bool> { false, false, false, false };
    public List<Weapons> currentSelections = new List<Weapons> { null, null, null, null };
    public List<Weapons> weapons;
    public List<int> playerNums = new List<int> { 1 };

    private WeaponSelectionHelper weaponSelector;
    private Color readyColor = new Color(0.05f, 1, 0, 0.3f);


    
    // Start is called before the first frame update
    public void Init()
    {
        Debug.Log(weapons.Count);
        // set up the buttons that's already linked to this menu
        for (int i = 0; i < weapons.Count; i++)
        {
            Button newbutton = weaponButtons[i];
            newbutton.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = weapons[i].name;
            newbutton.transform.GetChild(1).GetComponent<Image>().sprite = weapons[i].icon;
            int index = i;
            newbutton.onClick.AddListener(delegate { ShowWeaponInfo(index); });

        }
        List<List<Button>> buttons = new List<List<Button>> { weaponButtons };

        weaponSelector = new WeaponSelectionHelper(buttons, weaponButtons.Count - 1, 0,  playerNums);
        foreach (int playerNum in playerNums)
        {
            currentSelections[playerNum-1] = weapons[0];
        }
        menuLoaded = true;
    }

    public void LoadoutInput()
    {
        foreach (int playerNum in playerNums)
        {
            if (Input.GetAxisRaw("Fire1" + (playerNum).ToString()) > 0.0f)
            {
                if (!justPressed[playerNum - 1])
                {
                    justPressed[playerNum - 1] = true;
                    ToggleReady(playerNum);
                }
            }
            else
            {
                justPressed[playerNum - 1] = false;
            }
        }


        if (!ready)
        {
            // if anyone is still not ready, will need to keep taking input, checking what their selection is
            weaponSelector.SelectionInput();
            foreach (int playerNum in playerNums)
            {
                if (weaponSelector.Select(playerNum))
                {
                    int index = weaponSelector.InvokeSelection(playerNum);
                    SelectWeapon(playerNum, index);
                }
            }
        }
    }


    private void ToggleReady(int playerNum)
    {
        readys[playerNum-1] = !readys[playerNum-1];
        if (readys.TrueForAll(x => x))
        {
            ready = true;
        }
    }

    private void ShowWeaponInfo(int index)
    {
        // this is the new function invoked when the button is pressed
        Debug.Log("Weapon button number"+(index)+" pressed, since invoked function is per button not per player, dunno what should do with invoke?");
    }
    private void SelectWeapon(int playerNum, int index)
    {
        currentSelections[playerNum-1] = weapons[index];
    }
}
