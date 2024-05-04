using UnityEngine;
using TMPro;
using System.IO;
using System.Collections.Generic;

public class InputBehavior : MonoBehaviour
{
    [SerializeField] private TMP_InputField[] inputFields; // input field array
    [SerializeField] private TMP_Text[] outputTexts; // text display array for 2, 3, 4 or name, goals, goal type
    private SceneSystem sceneFunction;

    public static List<string> namesList = new List<string>();
    public static List<string> goalsList = new List<string>();
    public static List<string> goalTypesList = new List<string>();
    public static List<FishProperties> finishedFishList = new List<FishProperties>();
    //private string fileName = "FishData.json";

    void Start()
    {
        sceneFunction = FindObjectOfType<SceneSystem>();
        
        if (inputFields == null || inputFields.Length == 0) // make sure is not null
        {
            enabled = false; //disable
            return;
        }

        if (outputTexts == null || outputTexts.Length == 0) //same for output
        {
            enabled = false; //disable
            return;
        }

        for (int i = 0; i < inputFields.Length; i++)
        {
            int index = i; 
            inputFields[i].onSubmit.RemoveAllListeners(); // Remove all existing listeners
            inputFields[i].onSubmit.AddListener(newValue => OnInputSubmit(index)); // add the new text to from the input to the output basically
        }
    }

    void OnInputSubmit(int index) // when you press enter,
    {
        outputTexts[index].text = "";

        // Display the input text in the output field
        outputTexts[index].text = inputFields[index].text;

        string tag = inputFields[index].tag; // i tagged them with certain tags so i knew which scene to open next
        string lowercaseInput = inputFields[index].text.ToLower();

        switch (tag)
        {
            case "Add2":
                if(inputFields[index].text == "")
                {
                    Debug.Log("Please input a name.");
                    break;
                }
                else
                {
                    string capitalizedString = char.ToUpper(inputFields[index].text[0]) + inputFields[index].text.Substring(1);
                    namesList.Add(capitalizedString); // also adds the names, goals, and goal types to their respective list. so everything at place 1 in the list describes fish 1
                    sceneFunction.AddFishPopUp3Start();
                    outputTexts[index].text = capitalizedString;
                }
                break;
            case "Add3":
                if(inputFields[index].text == "")
                {
                    Debug.Log("Please input a goal.");
                    break;
                }
                else
                {
                    string capitalizedString = char.ToUpper(inputFields[index].text[0]) + inputFields[index].text.Substring(1);
                    goalsList.Add(capitalizedString); // also adds the names, goals, and goal types to their respective list. so everything at place 1 in the list describes fish 1
                    sceneFunction.AddFishPopUp4Start();
                    outputTexts[index].text = capitalizedString;
                }
                break;
            case "Add4":
                switch(lowercaseInput)
                {
                    case "daily":
                        goalTypesList.Add("Daily");
                        sceneFunction.AddFishPopUp5Start();
                        outputTexts[index].text = "Daily";
                        break;
                    case "weekly":
                        goalTypesList.Add("Weekly");
                        sceneFunction.AddFishPopUp5Start();
                        outputTexts[index].text = "Weekly";
                        break;
                    case "monthly":
                        goalTypesList.Add("Monthly");
                        sceneFunction.AddFishPopUp5Start();
                        outputTexts[index].text = "Monthly";
                        break;
                    default:
                        Debug.Log("Please choose a daily, weekly, or monthly goal type.");
                        outputTexts[index].text = "";
                    break;
                }
                break;
            case "Add2T":
                if(inputFields[index].text == "")
                {
                    Debug.Log("Please input a name.");
                    break;
                }
                else
                {
                    string capitalizedString = char.ToUpper(inputFields[index].text[0]) + inputFields[index].text.Substring(1);
                    namesList.Add(capitalizedString); // also adds the names, goals, and goal types to their respective list. so everything at place 1 in the list describes fish 1
                    sceneFunction.AddFishPopUp3StartT();
                    outputTexts[index].text = capitalizedString;
                    sceneFunction.InfoPopUpTutorial3();
                }
                break;
            case "Add3T":
                if(inputFields[index].text == "")
                {
                    Debug.Log("Please input a goal.");
                    break;
                }
                else
                {
                    string capitalizedString = char.ToUpper(inputFields[index].text[0]) + inputFields[index].text.Substring(1);
                    goalsList.Add(capitalizedString); // also adds the names, goals, and goal types to their respective list. so everything at place 1 in the list describes fish 1
                    sceneFunction.AddFishPopUp4StartT();
                    outputTexts[index].text = capitalizedString;
                    sceneFunction.InfoPopUpTutorial4();
                }
                break;
            case "Add4T":
                switch(lowercaseInput)
                {
                    case "daily":
                        goalTypesList.Add("Daily");
                        sceneFunction.AddFishPopUp5StartT();
                        sceneFunction.InfoPopUpTutorial5();
                        outputTexts[index].text = "Daily";
                        break;
                    default:
                        Debug.Log("Please choose the daily goal type. You will have the chance to do others later.");
                        outputTexts[index].text = "";
                    break;
                }
                break;
            default:
                Debug.LogWarning("Unknown tag: " + tag);
                break;
        }
        inputFields[index].text = "";
    }

    public string GetStringAtIndex(List<string> stringList, int index)
    {
        if (stringList == null || index < 0 || index >= stringList.Count)
        {
            Debug.LogError("Invalid list or index.");
            return null; 
        }
        return stringList[index];
    }
}