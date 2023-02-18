using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSelectionHelper
{
    private int currentIndex = 0;
    private int currentSelection = -1;
    private float holdTime = 0.5f;
    private float currHoldTime = 0;

    private List<Button> buttons;
    private int maxIndex = 0;
    private int playerNum = 1;

    // For horizontal scrollable menus
    private bool hScrollable = false;
    private int firstVisible = 0;
    private int lastVisible = 0;
    private RectTransform grid = null;
    private float widthOffset = 0;
    private float cellWidth = 105;
    private bool offsetOnRight = true;

    public MenuSelectionHelper(List<Button> buttons, int maxIndex, int playerNum = 1, int defaultSelection = -1)
    {
        this.buttons = buttons;
        this.maxIndex = maxIndex;
        currentSelection = defaultSelection;
        this.playerNum = playerNum;

        Init();
    }

    public MenuSelectionHelper(List<Button> buttons, int maxIndex, int firstVisible, int lastVisible, 
        RectTransform grid, float widthOffset, float cellWidth, int defaultSelection = -1, int playerNum = 1)
    {
        this.buttons = buttons;
        this.maxIndex = maxIndex;
        currentSelection = defaultSelection;
        this.firstVisible = firstVisible;
        this.lastVisible = lastVisible;
        this.grid = grid;
        this.widthOffset = widthOffset;
        this.cellWidth = cellWidth;
        this.playerNum = playerNum;

        hScrollable = true;
        Init();
    }

    private void Init()
    {
        UpdateButtonColors(currentIndex, currentIndex);
        if (currentSelection >= 0)
        {
            UpdateSelectedButtonColors(currentSelection, currentSelection);
        }
    }

    public void HorizontalSelection()
    {
        if (Input.GetAxis("Horizontal" + (playerNum).ToString()) >= 0.9f)
        {
            currHoldTime += Time.deltaTime;
            if (currHoldTime >= holdTime)
            {
                int prevIndex = currentIndex;
                currentIndex += 1;
                if (hScrollable && currentIndex > lastVisible)
                {
                    MoveGridRight();
                }

                if (currentIndex > maxIndex)
                {
                    currentIndex = 0;
                    if (hScrollable)
                    {
                        ResetGridLeft();
                    }
                }

                UpdateButtonColors(prevIndex, currentIndex);
                currHoldTime = 0;

                Debug.Log(currentIndex);
            }
        }
        else if (Input.GetAxis("Horizontal" + (playerNum).ToString()) <= -0.9f)
        {
            currHoldTime += Time.deltaTime;
            if (currHoldTime >= holdTime)
            {
                int prevIndex = currentIndex;
                currentIndex -= 1;
                if (hScrollable && currentIndex < firstVisible)
                {
                    MoveGridLeft();
                }

                if (currentIndex < 0)
                {
                    currentIndex = maxIndex;
                    if (hScrollable)
                    {
                        ResetGridRight();
                    }
                }

                UpdateButtonColors(prevIndex, currentIndex);
                currHoldTime = 0;

                Debug.Log(currentIndex);
            }
        }
    }

    public bool Select()
    {
        if (Input.GetButtonDown("Jump" + (playerNum).ToString()))
        {
            UpdateSelectedButtonColors(currentSelection, currentIndex);
            currentSelection = currentIndex;
            return true;
        }

        return false;
    }

    public void InvokeSelection()
    {
        buttons[currentSelection].onClick.Invoke();
    }

    private void UpdateButtonColors(int prevIndex, int currIndex)
    {
        if (currIndex != currentSelection)
        {
            buttons[currIndex].GetComponent<Image>().color = ButtonColors.hoverColor;
        }

        if (prevIndex != currentSelection && prevIndex != currIndex)
        {
            buttons[prevIndex].GetComponent<Image>().color = ButtonColors.normalColor;
        }
    }

    private void UpdateSelectedButtonColors(int prevSelection, int currSelection)
    {
        if (prevSelection >= 0)
        {
            buttons[prevSelection].GetComponent<Image>().color = ButtonColors.normalColor;
        }
        buttons[currSelection].GetComponent<Image>().color = ButtonColors.selectedColor;
    }

    private void MoveGridRight()
    {
        grid.position += new Vector3(offsetOnRight ? widthOffset - cellWidth : -cellWidth, 0, 0);
        offsetOnRight = false;

        firstVisible += 1;
        lastVisible += 1;
    }

    private void MoveGridLeft()
    {
        grid.position += new Vector3(!offsetOnRight ? cellWidth - widthOffset : cellWidth, 0, 0);
        offsetOnRight = true;

        firstVisible -= 1;
        lastVisible -= 1;
    }

    private void ResetGridLeft()
    {
        grid.offsetMin = new Vector2(0, grid.offsetMin.y);
        offsetOnRight = true;

        lastVisible -= firstVisible;
        firstVisible = 0;
    }

    private void ResetGridRight()
    {
        grid.offsetMin = new Vector2(-cellWidth * (maxIndex + 1) + 5, grid.offsetMin.y);
        offsetOnRight = false;

        firstVisible += maxIndex - lastVisible;
        lastVisible = maxIndex;
    }
}
