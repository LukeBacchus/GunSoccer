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
    [SerializeField]
    private GameObject selectedWeaponstwo;
    [SerializeField]
    private GameObject selectedWeaponsfour;

    public bool menuLoaded = false;
    public bool ready = false;
    private List<bool> readys;
    public List<Weapons> currentSelections = new List<Weapons> { null, null, null, null };
    public List<Weapons> weapons;
    public List<int> playerNums = new List<int> { 1 };
 

    private WeaponSelectionHelper weaponSelector;
    private Color readyColor = new Color(0.05f, 1, 0, 0.3f);

    
    // Start is called before the first frame update
    public void Init()
    {
        // set up the buttons that's already linked to this menu
        for (int i = 0; i < weapons.Count; i++)
        {
            Button newbutton = weaponButtons[i];
            newbutton.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = weapons[i].name;
            newbutton.transform.GetChild(1).GetComponent<Image>().sprite = weapons[i].icon;
        }
        List<List<Button>> buttons = new List<List<Button>> { weaponButtons };

        weaponSelector = new WeaponSelectionHelper(buttons, weaponButtons.Count - 1, 0,  playerNums);
        readys = new List<bool>();
        foreach (int playerNum in playerNums)
        {
            currentSelections[playerNum-1] = weapons[0]; // set everyone to use assault rifle by default
            readys.Add(false);
        }
        menuLoaded = true;
        
        if (playerNums.Count == 2)
        {
            selectedWeaponstwo.SetActive(true);
            selectedWeaponsfour.SetActive(false);
        }
        else
        {
            selectedWeaponsfour.SetActive(true);
            selectedWeaponstwo.SetActive(false);
        }
        UpdateSelectedDisplay();
    }

    public void LoadoutInput()
    {
        foreach (int playerNum in playerNums)
        {
            if (Input.GetButtonDown("Jump" + playerNum.ToString()))
            {
                ToggleReady(playerNum);
            }
        }


        if (!ready)
        {
            // if anyone is still not ready, will need to keep taking input, checking what their selection is
            weaponSelector.SelectionInput();
            foreach (int playerNum in playerNums)
            {
                int index = weaponSelector.InvokeSelection(playerNum);
                SelectWeapon(playerNum, index);
                UpdateSelectedDisplay();
            }
        }
    }


    private void ToggleReady(int playerNum)
    {
        readys[playerNum-1] = !readys[playerNum-1];
        weaponSelector.ToggleAllowedInput(playerNum);

        GameObject weaponDisplays;
        if (playerNums.Count == 2)
        {
            weaponDisplays = selectedWeaponstwo;
        }
        else
        {
            weaponDisplays = selectedWeaponsfour;
        }

        GameObject weaponDisplayForP = weaponDisplays.transform.GetChild(playerNum - 1).gameObject; // get the display per player

        if (readys[playerNum - 1])
        {
            weaponDisplayForP.GetComponent<Image>().color = readyColor;
        }
        else
        {
            weaponDisplayForP.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }

        if (readys.TrueForAll(x => x))
        {
            ready = true;
        }


        Debug.Log(ready);
    }

    public void SetAllReadyFalse()
    {
        foreach (int playerNum in playerNums)
        {
            if (readys[playerNum - 1])
            {
                ToggleReady(playerNum);
            }
        }

        ready = false;
    }

    private void SelectWeapon(int playerNum, int index)
    {
        currentSelections[playerNum-1] = weapons[index];
        // update the display as well
    }

    private void UpdateSelectedDisplay()
    {
        GameObject weaponDisplays;
        if (playerNums.Count == 2)
        {
            weaponDisplays = selectedWeaponstwo;
        }
        else
        {
            weaponDisplays = selectedWeaponsfour;
        }

        foreach (int playerNum in playerNums)
        {
            // for each player, update their selection
            int pIndex = playerNum - 1;

            Transform weaponDisplayForP = weaponDisplays.transform.GetChild(pIndex); // get the display per player
            // Debug.Log(weaponDisplayForP.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>());
            // update the stuff for each part witin this display per player

            weaponDisplayForP.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = currentSelections[pIndex].weaponType;
            weaponDisplayForP.GetChild(2).GetComponent<Image>().sprite = currentSelections[pIndex].icon;
            weaponDisplayForP.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text = currentSelections[pIndex].description;
        }
    }
}
