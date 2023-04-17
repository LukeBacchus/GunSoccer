using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class WeaponSelectionHelper
{
    // changed to save multiple positions, one per player.
    private List<List<int>> current_positions = new List<List<int>> { new List<int> { 0, 0 }, new List<int> { 0, 0 }, new List<int> { 0, 0 }, new List<int> { 0, 0} }; //TODO: issues with starting position!
    private List<bool> allowedInput = new List<bool> { true, true, true, true };
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


    public WeaponSelectionHelper(List<List<Button>> buttons, int maxCol, int maxRow, List<int> playerNums = null)
    {
        this.buttons = buttons;
        this.maxCol = maxCol;
        this.maxRow = maxRow;

        this.playerNums = playerNums ?? this.playerNums;

        Init();
    }


    public void Init()
    {
        HideAllBorderHover();
        foreach (int playerNum in playerNums)
        {
            int currentRow = current_positions[playerNum-1][0];
            int currentCol = current_positions[playerNum-1][1];
            ShowBorderHover(currentRow, currentCol, playerNum);
        }

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
            int currentRow = current_positions[playerNum - 1][0];
            int currentCol = current_positions[playerNum - 1][1];

            int pIndex = playerNum - 1;
            if (!allowedInput[pIndex])
            {
                continue;
            }

            float threshold = horizontalThresholdTimes[pIndex];
            if (Input.GetAxis("Horizontal" + (playerNum).ToString()) >= 0.9f)
            {
                horizontalHoldTime += Time.unscaledDeltaTime;

                if (horizontalHoldTime >= threshold)
                {
                    int prevCol = currentCol;
                    currentCol += 1;

                    if (currentCol > maxCol)
                    {
                        currentCol = 0;
                    }
                    HideBorderHover(currentRow, prevCol, playerNum);
                    ShowBorderHover(currentRow, currentCol, playerNum);

                    // also update within the list
                    current_positions[playerNum - 1][0] = currentRow;
                    current_positions[playerNum - 1][1] = currentCol;

                    horizontalHoldTime = 0;
                    // if player continues to hold, threshold changes to cooldown time
                    horizontalThresholdTimes[pIndex] = coolDownTime;

                    if (prevCol != currentCol)
                    {
                        //Scroll sfx
                        RuntimeManager.PlayOneShot("event:/MenuScroll");
                    }
                }
            }
            else if (Input.GetAxis("Horizontal" + (playerNum).ToString()) <= -0.9f)
            {

                horizontalHoldTime += Time.unscaledDeltaTime;
                if (horizontalHoldTime >= threshold)
                {
                    int prevCol = currentCol;
                    currentCol -= 1;

                    if (currentCol < 0)
                    {
                        currentCol = maxCol;
                    }

                    HideBorderHover(currentRow, prevCol, playerNum);
                    ShowBorderHover(currentRow, currentCol, playerNum);


                    current_positions[playerNum - 1][0] = currentRow;
                    current_positions[playerNum - 1][1] = currentCol;

                    horizontalHoldTime = 0;
                    // if player continues to hold, threshold changes to cooldown time
                    horizontalThresholdTimes[pIndex] = coolDownTime;
                    if (prevCol != currentCol)
                    {
                        //Scroll sfx
                        RuntimeManager.PlayOneShot("event:/MenuScroll");
                    }
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
            int currentRow = current_positions[playerNum - 1][0];
            int currentCol = current_positions[playerNum - 1][1];

            int pIndex = playerNum - 1;
            if (!allowedInput[pIndex])
            {
                continue;
            }

            float threshold = verticalThresholdTimes[pIndex];
            if (Input.GetAxis("Vertical" + (playerNum).ToString()) <= -0.9f)
            {
                verticalHoldTime += Time.unscaledDeltaTime;
                if (verticalHoldTime >= threshold)
                {
                    int prevRow = currentRow;
                    currentRow += 1;

                    if (currentRow > maxRow)
                    {
                        currentRow = 0;
                    }

                    HideBorderHover(prevRow, currentCol, playerNum);
                    ShowBorderHover(currentRow, currentCol, playerNum);

                    current_positions[playerNum - 1][0] = currentRow;
                    current_positions[playerNum - 1][1] = currentCol;

                    verticalHoldTime = 0;
                    // if player continues to hold, threshold changes to cooldown time
                    verticalThresholdTimes[pIndex] = coolDownTime;
                    if (prevRow != currentRow)
                    {
                        //Scroll sfx
                        RuntimeManager.PlayOneShot("event:/MenuScroll");
                    }
                }
            }
            else if (Input.GetAxis("Vertical" + (playerNum).ToString()) >= 0.9f)
            {
                verticalHoldTime += Time.unscaledDeltaTime;
                if (verticalHoldTime >= threshold)
                {
                    int prevRow = currentRow;
                    currentRow -= 1;

                    if (currentRow < 0)
                    {
                        currentRow = maxRow;
                    }

                    HideBorderHover(prevRow, currentCol, playerNum);
                    ShowBorderHover(currentRow, currentCol, playerNum);

                    current_positions[playerNum - 1][0] = currentRow;
                    current_positions[playerNum - 1][1] = currentCol;

                    verticalHoldTime = 0;
                    // if player continues to hold, threshold changes to cooldown time
                    verticalThresholdTimes[pIndex] = coolDownTime;
                    if (prevRow != currentRow)
                    {
                        //Scroll sfx
                        RuntimeManager.PlayOneShot("event:/MenuScroll");
                    }

                }
            }
            else if (Input.GetAxis("Vertical" + (playerNum).ToString()) >= -0.1f && Input.GetAxis("Vertical" + (playerNum).ToString()) <= 0.1f)
            {

                // player "let go" of controller resets first contact, making threshold shorter = more respondant
                verticalThresholdTimes[pIndex] = holdTime;
            }
        }
    }

    public int InvokeSelection(int playerNum)
    {

        // returns the row which is the index of the button selected
        int selectedRow = current_positions[playerNum-1][0];
        int selectedCol = current_positions[playerNum-1][1];

        buttons[selectedRow][selectedCol].GetComponent<Button>().onClick.Invoke();
        return selectedCol;
    }

    public void ToggleAllowedInput(int playerNum)
    {
        allowedInput[playerNum - 1] = !allowedInput[playerNum - 1];
    }

    public void ShowBorderHover(int row, int col, int playerNum)
    {
        int pIndex = playerNum - 1;
        buttons[row][col].GetComponent<WeaponButtonComponents>().hover[pIndex].SetActive(true);
    }

    public void HideBorderHover(int row, int col, int playerNum)
    {
        int pIndex = playerNum - 1;
        buttons[row][col].GetComponent<WeaponButtonComponents>().hover[pIndex].SetActive(false);
    }

    public void HideAllBorderHover()
    {
        for (int row = 0; row <= maxRow; row++)
        {
            for (int col = 0; col <= maxCol; col++)
            {
                for (int pIndex = 0; pIndex < playerNums.Count; pIndex++)
                {
                    buttons[row][col].GetComponent<WeaponButtonComponents>().hover[pIndex].SetActive(false);
                }
            }
        }
    }

}