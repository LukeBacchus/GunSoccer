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
    public List<float> playerHoldRightTimes = new List<float> { 0, 0, 0, 0 };
    public List<float> playerHoldRightCooldown = new List<float> { 0.1f, 0.1f, 0.1f, 0.1f };
    public List<float> playerHoldLeftTimes = new List<float> { 0, 0, 0, 0 };
    public List<float> playerHoldLeftCooldown = new List<float> { 0.1f, 0.1f, 0.1f, 0.1f };

    private float longHoldThreshold = 2;
    private Slider slider;

    private void Awake()
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
                playerHoldRightTimes[pIndex] += Time.unscaledDeltaTime;
                playerHoldLeftTimes[pIndex] = 0;

                if (playerHoldRightCooldown[pIndex] <= 0)
                {
                    slider.value += (slider.wholeNumbers ? 1 : Time.unscaledDeltaTime) * (playerHoldRightTimes[pIndex] > longHoldThreshold ? 10 : 1);
                        
                    playerHoldRightCooldown[pIndex] = 0.1f;
                }
                playerHoldRightCooldown[pIndex] -= Time.unscaledDeltaTime;
            }
            else if (Input.GetAxis("Horizontal" + (playerNum).ToString()) <= -0.9f)
            {
                playerHoldLeftTimes[pIndex] += Time.unscaledDeltaTime;
                playerHoldRightTimes[pIndex] = 0;

                if (playerHoldLeftCooldown[pIndex] <= 0)
                {
                    slider.value -= (slider.wholeNumbers ? 1 : Time.unscaledDeltaTime) * (playerHoldLeftTimes[pIndex] > longHoldThreshold ? 10 : 1);
                        
                    playerHoldLeftCooldown[pIndex] = 0.1f;
                }
                playerHoldLeftCooldown[pIndex] -= Time.unscaledDeltaTime;
            }
            else
            {
                playerHoldLeftTimes[pIndex] = 0;
                playerHoldRightTimes[pIndex] = 0;

                playerHoldLeftCooldown[pIndex] = 0.1f;
                playerHoldRightCooldown[pIndex] = 0.1f;
            }
        }
    }

    public float getValue()
    {
        return slider.value;
    }

    public void updateText()
    {
        valueText.text = slider.value.ToString("F2");
    }

    public void InitValue(float value)
    {
        slider.value = value;
        updateText();
    }
}
