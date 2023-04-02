using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class MenuSelectionHelper
{
    private int currentCol = 0;
    private int currentRow = 0;
    private int selectedCol = -1;
    private int selectedRow = -1;
    private float holdTime = 0.1f;
    private float coolDownTime = 0.4f;
    private float horizontalHoldTime = 0;
    private float verticalHoldTime = 0;
    private List<float> horizontalThresholdTimes;
    private List<float> verticalThresholdTimes;

private List<List<Button>> buttons;
    private int maxCol = 0;
    private int maxRow = 0;
    private List<int> playerNums = new List<int> { 1 };

    // For horizontal scrollable menus
    private bool hScrollable = false;
    private int firstVisible = 0;
    private int lastVisible = 0;
    private RectTransform grid = null;
    private float widthOffset = 0;
    private float cellWidth;
    private bool offsetOnRight = true;

    public MenuSelectionHelper(List<List<Button>> buttons, int maxCol, int maxRow, List<int> playerNums = null, int defaultCol = -1, int defaultRow = -1)
    {
        this.buttons = buttons;
        this.maxCol = maxCol;
        this.maxRow = maxRow;
        selectedCol = defaultCol;
        selectedRow = defaultRow;
        this.playerNums = playerNums ?? this.playerNums;

        Init();
    }

    public MenuSelectionHelper(List<List<Button>> buttons, int maxCol, int maxRow, int firstVisible, int lastVisible, 
        RectTransform grid, float widthOffset, float cellWidth, int defaultCol = -1, int defaultRow = -1, List<int> playerNums = null)
    {
        this.buttons = buttons;
        this.maxCol = maxCol;
        this.maxRow = maxRow;
        selectedCol = defaultCol;
        selectedRow = defaultRow;
        this.firstVisible = firstVisible;
        this.lastVisible = lastVisible;
        this.grid = grid;
        this.widthOffset = widthOffset;
        this.cellWidth = cellWidth;
        this.playerNums = playerNums ?? this.playerNums;

        hScrollable = true;
        Init();
    }

    public void Init()
    {
        ShowBorderHover(currentRow, currentCol);
        ShowBorderSelect(selectedRow, selectedCol);
        horizontalThresholdTimes = new List<float> { holdTime, holdTime, holdTime, holdTime };
        verticalThresholdTimes = new List<float> { holdTime, holdTime, holdTime, holdTime };
    }


    public void SelectionInput()
    {
        HorizontalSelection();
        VerticalSelection();
    }

    private void HorizontalSelection()
    {
        foreach (int playerNum in playerNums)
        {
            int pIndex = playerNum - 1;
            float threshold = horizontalThresholdTimes[pIndex];
            if (Input.GetAxis("Horizontal" + (playerNum).ToString()) >= 0.9f)
            {
                horizontalHoldTime += Time.deltaTime;

                if (horizontalHoldTime >= threshold)
                {
                    int prevCol = currentCol;
                    currentCol += 1;
                    if (hScrollable && currentCol > lastVisible)
                    {
                        MoveGridRight();
                    }

                    if (currentCol > maxCol)
                    {
                        currentCol = 0;
                        if (hScrollable)
                        {
                            ResetGridLeft();
                        }
                    }

                    HideBorderHover(currentRow, prevCol);
                    ShowBorderHover(currentRow, currentCol);
                    horizontalHoldTime = 0;
                    // if player continues to hold, threshold changes to cooldown time
                    horizontalThresholdTimes[pIndex] = coolDownTime;
                    RuntimeManager.PlayOneShot("event:/Menu Scroll");
                }
            }
            else if (Input.GetAxis("Horizontal" + (playerNum).ToString()) <= -0.9f)
            {

                horizontalHoldTime += Time.deltaTime;
                if (horizontalHoldTime >= threshold)
                {
                    int prevCol = currentCol;
                    currentCol -= 1;
                    if (hScrollable && currentCol < firstVisible)
                    {
                        MoveGridLeft();
                    }

                    if (currentCol < 0)
                    {
                        currentCol = maxCol;
                        if (hScrollable)
                        {
                            ResetGridRight();
                        }
                    }

                    HideBorderHover(currentRow, prevCol);
                    ShowBorderHover(currentRow, currentCol);
                    horizontalHoldTime = 0;
                    // if player continues to hold, threshold changes to cooldown time
                    horizontalThresholdTimes[pIndex] = coolDownTime;
                    RuntimeManager.PlayOneShot("event:/Menu Scroll");
                }
            }
            else if (Input.GetAxis("Horizontal" + (playerNum).ToString()) >= -0.1f && Input.GetAxis("Horizontal" + (playerNum).ToString()) <= 0.1f)
            {

                // player "let go" of controller resets first contact, making threshold shorter = more respondant
                horizontalThresholdTimes[pIndex] = holdTime;
            }
        }
    }

    private void VerticalSelection()
    {
        foreach (int playerNum in playerNums)
        {
            int pIndex = playerNum - 1;
            float threshold = verticalThresholdTimes[pIndex];
            if (Input.GetAxis("Vertical" + (playerNum).ToString()) <= -0.9f)
            {
                verticalHoldTime += Time.deltaTime;
                if (verticalHoldTime >= threshold)
                {
                    int prevRow = currentRow;
                    currentRow += 1;

                    if (currentRow > maxRow)
                    {
                        currentRow = 0;
                    }

                    RuntimeManager.PlayOneShot("event:/Menu Scroll");

                    HideBorderHover(prevRow, currentCol);
                    ShowBorderHover(currentRow, currentCol);
                    verticalHoldTime = 0;
                    // if player continues to hold, threshold changes to cooldown time
                    verticalThresholdTimes[pIndex] = coolDownTime;                    
                }
            }
            else if (Input.GetAxis("Vertical" + (playerNum).ToString()) >= 0.9f)
            {
                verticalHoldTime += Time.deltaTime;
                if (verticalHoldTime >=threshold)
                {
                    int prevRow = currentRow;
                    currentRow -= 1;

                    if (currentRow < 0)
                    {
                        currentRow = maxRow;
                        
                    }

                    RuntimeManager.PlayOneShot("event:/Menu Scroll");

                    HideBorderHover(prevRow, currentCol);
                    ShowBorderHover(currentRow, currentCol);
                    verticalHoldTime = 0;
                    // if player continues to hold, threshold changes to cooldown time
                    verticalThresholdTimes[pIndex] = coolDownTime;

             

                }
            }
            else if (Input.GetAxis("Vertical" + (playerNum).ToString()) >= -0.1f && Input.GetAxis("Vertical" + (playerNum).ToString()) <= 0.1f)
            {

                // player "let go" of controller resets first contact, making threshold shorter = more respondant
                verticalThresholdTimes[pIndex] = holdTime;
            }
        }
    }

    public bool Select()
    {
        foreach (int playerNum in playerNums)
        {
            if (Input.GetButtonDown("Jump" + (playerNum).ToString()))
            {
                HideBorderSelect(selectedRow, selectedCol);
                selectedCol = currentCol;
                selectedRow = currentRow;
                ShowBorderSelect(selectedRow, selectedCol);
                RuntimeManager.PlayOneShot("event:/Menu Select");
                return true;
            }
        }

        return false;
    }

    public void InvokeSelection()
    {
        buttons[selectedRow][selectedCol].onClick.Invoke();
    }

    public void ShowBorderHover(int row, int col)
    {
        buttons[row][col].GetComponent<ButtonComponents>().hover.SetActive(true);
        
    }

    public void HideBorderHover(int row, int col)
    {
        buttons[row][col].GetComponent<ButtonComponents>().hover.SetActive(false);
    }

    public void ShowBorderSelect(int row, int col)
    {
        if (row >= 0 && col >= 0)
        {
            buttons[row][col].GetComponent<ButtonComponents>().select.SetActive(true);
        }
    }

    public void HideBorderSelect(int row, int col)
    {
        if (row >= 0 && col >= 0)
        {
            buttons[row][col].GetComponent<ButtonComponents>().select.SetActive(false);
        }
    }

    private void MoveGridRight()
    {
        grid.offsetMin += new Vector2(offsetOnRight ? widthOffset - cellWidth : -cellWidth, 0);
        offsetOnRight = false;

        firstVisible += 1;
        lastVisible += 1;
    }

    private void MoveGridLeft()
    {
        grid.offsetMin += new Vector2(!offsetOnRight ? cellWidth - widthOffset : cellWidth, 0);
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
        grid.offsetMin = new Vector2(-cellWidth * (maxCol + 1) + 5, grid.offsetMin.y);
        offsetOnRight = false;

        firstVisible += maxCol - lastVisible;
        lastVisible = maxCol;
    }
}
