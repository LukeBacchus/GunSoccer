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
    private Button buttonPreset;

    public bool menuLoaded = false;
    public int playerNum;
    public bool ready = false;
    private bool justPressed = false;
    public Weapons currentSelection;
    public List<Weapons> weapons;

    private MenuSelectionHelper weaponSelector;
    private Color readyColor = new Color(0.05f, 1, 0, 0.3f);

    private List<GameObject> weaponButtons = new List<GameObject>();

    // Start is called before the first frame update
    public void Init()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            Button newbutton = Instantiate(buttonPreset);
            newbutton.transform.SetParent(weaponGrid);
            newbutton.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = weapons[i].name;
            newbutton.transform.GetChild(1).GetComponent<Image>().sprite = weapons[i].icon;
            int index = i;
            newbutton.onClick.AddListener(delegate { SelectWeapon(index); });

            weaponButtons.Add(newbutton.gameObject);
        }
        List<List<GameObject>> buttons = new List<List<GameObject>> { weaponButtons };

        weaponGrid.position = new Vector3(0, weaponGrid.position.y, weaponGrid.position.z);

        weaponSelector = new MenuSelectionHelper(buttons, weaponButtons.Count - 1, 0, viewport, weaponGrid, true, false, new List<int> { playerNum }, 0, 0);
        currentSelection = weapons[0];
        menuLoaded = true;
    }

    public void LoadoutInput()
    {
        if (Input.GetAxisRaw("Fire1" + (playerNum).ToString()) > 0.0f)
        {
            if(!justPressed){
                justPressed = true;
                ToggleReady();
            }
        } else {
            justPressed = false;
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

    public void ToggleReady()
    {
        ready = !ready;
        if (ready)
        {
            for (int i = 0; i < weapons.Count; i++)
            {
                weaponButtons[i].GetComponent<Button>().onClick.RemoveAllListeners();
            }

            GetComponent<Image>().color = readyColor;
        } else
        {
            for (int i = 0; i < weapons.Count; i++)
            {
                int index = i;
                weaponButtons[i].GetComponent<Button>().onClick.AddListener(delegate { SelectWeapon(index); });
            }

            GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }
    }

    private void SelectWeapon(int index)
    {
        currentSelection = weapons[index];
    }
}
