using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class InputBehavior : MonoBehaviour
{
    [SerializeField] private TMP_InputField[] inputFields; // input field array
    [SerializeField] private TMP_Text[] outputTexts; // text display array for 2, 3, 4 or name, goals, goal type
    private SceneSystem sceneFunction;

    public static List<string> namesList = new List<string>();
    public static List<string> goalsList = new List<string>();
    public static List<string> goalTypesList = new List<string>();

    void Start()
    {
        sceneFunction = FindObjectOfType<SceneSystem>();
        
        if (inputFields == null || inputFields.Length == 0) // make sure is not null
        {
            Debug.LogError("no component assigned");
            enabled = false; //disable
            return;
        }

        if (outputTexts == null || outputTexts.Length == 0) //same for output
        {
            Debug.LogError("No TextMeshPro components assigned!");
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

        switch (tag)
        {
            case "Add2":
                namesList.Add(inputFields[index].text); // also adds the names, goals, and goal types to their respective list. so everything at place 1 in the list describes fish 1
                sceneFunction.AddFishPopUp3Start();
                break;
            case "Add3":
                goalsList.Add(inputFields[index].text);
                sceneFunction.AddFishPopUp4Start();
                break;
            case "Add4":
                goalTypesList.Add(inputFields[index].text);
                sceneFunction.AddFishPopUp5Start();
                break;
            case "Add2T":
                namesList.Add(inputFields[index].text); // also adds the names, goals, and goal types to their respective list. so everything at place 1 in the list describes fish 1
                sceneFunction.AddFishPopUp3StartT();
                sceneFunction.InfoPopUpTutorial3();
                break;
            case "Add3T":
                goalsList.Add(inputFields[index].text);
                sceneFunction.AddFishPopUp4StartT();
                sceneFunction.InfoPopUpTutorial4();
                break;
            case "Add4T":
                goalTypesList.Add(inputFields[index].text);
                sceneFunction.AddFishPopUp5StartT();
                sceneFunction.InfoPopUpTutorial5();
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