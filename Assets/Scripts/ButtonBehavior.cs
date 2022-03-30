using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonBehavior: MonoBehaviour
{
    public Button startButton;
    public Text buttonText;
    public bool isPressed = false;
    // Start is called before the first frame update
    private void Start()
    {
        startButton.onClick.AddListener(OnButtonPress);
    }

    public void OnButtonPress()
    {
        if (isPressed)
        {
            isPressed = false;
        }
        else
        {
            isPressed = true;
        }
    }

    public void SetButtonText(string text)
    {
       buttonText.text = text;
    }

}