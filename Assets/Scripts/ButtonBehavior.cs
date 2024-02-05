using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script is for button behavior throughout the goal tracking, each button is different and I will need other scripts leading me to different scenes but that can be added here

public class ButtonBehavior : MonoBehaviour
{
    private static List<ButtonBehavior> buttonsWithScript = new List<ButtonBehavior>(); // list of every button 
    private Button button;
    private Image buttonImage;
    private Color originalColor;
    private Color newColor;

    private bool clicked = false;

    void Start()
    {
        button = GetComponent<Button>(); // get every button component
        buttonImage = GetComponent<Image>(); // get the color of the button

        originalColor = buttonImage.color; // store it for adding on color later
        
        buttonsWithScript.Add(this); // add this script to all the buttons in the list

        button.onClick.AddListener(ChangeButtonColor); // listener listens to what is being clicked and applys it
    }

    void ChangeButtonColor()
    {
        if (button.name == "Restore") // if labeled restore
        {
            foreach (ButtonBehavior btn in buttonsWithScript) // restore every button to white
            {
                if (btn.clicked)
                {
                    btn.RestoreColor();
                }
            }
        }
        else
        {
            if (!clicked) // if the button has never been clicked: flag it now for being clicked, which will help me later as i develop the game more
            {
                clicked = true;
            }


            originalColor.r -= 0.1f; // make it darker in all RGB
            originalColor.g -= 0.1f;
            originalColor.b -= 0.1f;

            buttonImage.color = originalColor; // apply

            Debug.Log("GOAL TRACKED: " + button.name); // check you clicked the button
        }
    }

    public void RestoreColor()
    {
        buttonImage.color = new Color(1f, 1f, 1f, 1f);
    }
}