using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuSliderController : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI valueText;

    public List<int> playerNums = new List<int> { 1, 2, 3, 4 };
    public List<float> playerHoldTimes = new List<float> { 0, 0, 0, 0 };

    private float longHoldThreshold = 2;
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        updateText();
    }

    public void SliderInput()
    {
        foreach (int playerNum in playerNums)
        {
            int pIndex = playerNum - 1;

            if (Input.GetAxis("Horizontal" + (playerNum).ToString()) >= 0.9f)
            {
                playerHoldTimes[pIndex] += Time.deltaTime;

                slider.value += 1;
                if (playerHoldTimes[pIndex] > longHoldThreshold)
                {
                    slider.value += 4;
                }
            }
            else
            {
                playerHoldTimes[pIndex] = 0;
            }
        }
    }

    public float getValue()
    {
        return slider.value;
    }

    public void updateText()
    {
        valueText.text = slider.value.ToString();
    }
}
