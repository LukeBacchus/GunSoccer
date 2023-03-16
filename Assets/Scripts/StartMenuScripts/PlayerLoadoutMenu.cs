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

    private List<Button> weaponButtons = new List<Button>();

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
            
            weaponButtons.Add(newbutton);
        }
        List<List<Button>> buttons = new List<List<Button>> { weaponButtons };

        weaponGrid.offsetMin = Vector2.zero;
        viewport.offsetMin = Vector2.zero;

        Canvas.ForceUpdateCanvases();
        float cellWidth = weaponButtons[0].GetComponent<RectTransform>().rect.width + 5;
        int lastVisible = (int)Mathf.Floor(viewport.rect.width / cellWidth) - 1;
        float widthOffset = viewport.rect.width % cellWidth;

        weaponSelector = new MenuSelectionHelper(buttons, weaponButtons.Count - 1, 0, 0, lastVisible, weaponGrid, widthOffset, cellWidth, 0, 0, new List<int> { playerNum });
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

    private void ToggleReady()
    {
        ready = !ready;
        if (ready)
        {
            for (int i = 0; i < weapons.Count; i++)
            {
                weaponButtons[i].onClick.RemoveAllListeners();
            }

            GetComponent<Image>().color = readyColor;
        } else
        {
            for (int i = 0; i < weapons.Count; i++)
            {
                int index = i;
                weaponButtons[i].onClick.AddListener(delegate { SelectWeapon(index); });
            }

            GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }
    }

    private void SelectWeapon(int index)
    {
        currentSelection = weapons[index];
    }
}
