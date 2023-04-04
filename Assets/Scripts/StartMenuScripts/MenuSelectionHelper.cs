using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private List<List<GameObject>> buttons;
    private int maxCol = 0;
    private int maxRow = 0;
    private List<int> playerNums = new List<int> { 1 };

    // For horizontal scrollable menus
    private bool hScrollable = false;
    private bool vScrollable = false;
    private RectTransform grid = null;
    private RectTransform viewport = null;


    public MenuSelectionHelper(List<List<GameObject>> buttons, int maxCol, int maxRow, List<int> playerNums = null, int defaultCol = -1, int defaultRow = -1)
    {
        this.buttons = buttons;
        this.maxCol = maxCol;
        this.maxRow = maxRow;
        selectedCol = defaultCol;
        selectedRow = defaultRow;
        this.playerNums = playerNums ?? this.playerNums;

        Init();
    }

    public MenuSelectionHelper(List<List<GameObject>> buttons, int maxCol, int maxRow, RectTransform viewport, RectTransform grid, bool hScrollable = true, bool vScrollable = true, List<int> playerNums = null, int defaultCol = -1, int defaultRow = -1)
    {
        this.buttons = buttons;
        this.maxCol = maxCol;
        this.maxRow = maxRow;
        selectedCol = defaultCol;
        selectedRow = defaultRow;
        this.playerNums = playerNums ?? this.playerNums;
        this.viewport = viewport;
        this.grid = grid;
        this.vScrollable = vScrollable;
        this.hScrollable = hScrollable;

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
                horizontalHoldTime += Time.unscaledDeltaTime;

                if (horizontalHoldTime >= threshold)
                {
                    int prevCol = currentCol;
                    currentCol += 1;

                    if (currentCol > maxCol)
                    {
                        currentCol = 0;
                        if (hScrollable)
                        {
                            MoveGridLeft();
                        }
                    } else if (hScrollable)
                    {
                        MoveGridRight();
                    }

                    HideBorderHover(currentRow, prevCol);
                    ShowBorderHover(currentRow, currentCol);
                    horizontalHoldTime = 0;
                    // if player continues to hold, threshold changes to cooldown time
                    horizontalThresholdTimes[pIndex] = coolDownTime;
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
                        if (hScrollable)
                        {
                            MoveGridRight();
                        }
                    } else if (hScrollable)
                    {
                        MoveGridLeft();
                    }

                    HideBorderHover(currentRow, prevCol);
                    ShowBorderHover(currentRow, currentCol);
                    horizontalHoldTime = 0;
                    // if player continues to hold, threshold changes to cooldown time
                    horizontalThresholdTimes[pIndex] = coolDownTime;
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
                verticalHoldTime += Time.unscaledDeltaTime;
                if (verticalHoldTime >= threshold)
                {
                    int prevRow = currentRow;
                    currentRow += 1;

                    if (currentRow > maxRow)
                    {
                        currentRow = 0;
                        if (vScrollable)
                        {
                            MoveGridUp();
                        }
                    }
                    else if (vScrollable)
                    {
                        MoveGridDown();
                    }

                    HideBorderHover(prevRow, currentCol);
                    ShowBorderHover(currentRow, currentCol);
                    verticalHoldTime = 0;
                    // if player continues to hold, threshold changes to cooldown time
                    verticalThresholdTimes[pIndex] = coolDownTime;
                }
            }
            else if (Input.GetAxis("Vertical" + (playerNum).ToString()) >= 0.9f)
            {
                verticalHoldTime += Time.unscaledDeltaTime;
                if (verticalHoldTime >=threshold)
                {
                    int prevRow = currentRow;
                    currentRow -= 1;

                    if (currentRow < 0)
                    {
                        currentRow = maxRow;
                        if (vScrollable)
                        {
                            MoveGridDown();
                        }
                    }
                    else if (vScrollable)
                    {
                        MoveGridUp();
                    }

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
                return true;
            }
        }

        return false;
    }

    public void InvokeSelection()
    {
        buttons[selectedRow][selectedCol].GetComponent<Button>().onClick.Invoke();
    }

    public GameObject GetCurrent()
    {
        return buttons[currentRow][currentCol];
    }

    public void ResetCurrent()
    {
        HideBorderHover(currentRow, currentCol);
        currentRow = 0;
        currentCol = 0;
        ShowBorderHover(currentRow, currentCol);
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
        RectTransform current = GetCurrent().GetComponent<RectTransform>();

        float viewportX = viewport.TransformPoint(new Vector2(viewport.rect.xMax, 0)).x;
        float currentX = current.TransformPoint(new Vector2(current.rect.xMax, 0)).x;

        Debug.Log(viewportX);
        Debug.Log(currentX);

        if (currentX > viewportX)
        {
            grid.position -= new Vector3(currentX - viewportX, 0, 0);
        }
    }

    private void MoveGridLeft()
    {
        RectTransform current = GetCurrent().GetComponent<RectTransform>();

        float viewportX = viewport.TransformPoint(new Vector2(viewport.rect.xMin, 0)).x;
        float currentX = current.TransformPoint(new Vector2(current.rect.xMin, 0)).x;

        if (currentX < viewportX)
        {
            grid.position += new Vector3(viewportX - currentX, 0, 0);
        }
    }

    private void MoveGridDown()
    {
        RectTransform current = GetCurrent().GetComponent<RectTransform>();

        float viewportY = viewport.TransformPoint(new Vector2(0, viewport.rect.yMin)).y;
        float currentY = current.TransformPoint(new Vector2(0, current.rect.yMin)).y;

        if (currentY < viewportY)
        {
            grid.position += new Vector3(0, viewportY - currentY, 0);
        }
    }

    private void MoveGridUp()
    {
        RectTransform current = GetCurrent().GetComponent<RectTransform>();

        float viewportY = viewport.TransformPoint(new Vector2(0, viewport.rect.yMax)).y;
        float currentY = current.TransformPoint(new Vector2(0, current.rect.yMax)).y;

        if (currentY > viewportY)
        {
            grid.position -= new Vector3(0, currentY - viewportY, 0);
        }
    }
}
