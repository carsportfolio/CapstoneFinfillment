using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

// this class is where i save fish information like their goal and name, which will be inputted by the user
// it uses a JSON file. this file original worked for iOS keyboards but the code is different while testing on a computer
public class SaveAndLoadBehavior : MonoBehaviour
{
    public TMP_InputField tmpInputField; // the input field that the user clicks on in order to type
    //public TextMeshProUGUI tmpColor; // text color, which can also be changed in Unity
    public Transform checklistContainer; // transform of the parent of my Checklist prefab (location)
    public GameObject checklistItemPrefab; // the actual copy of the Checklist prefab gameobject

    public Transform fishNameHolderContainer;
    public GameObject fishNameHolderPrefab;
    private List<Toggle> toggleList = new List<Toggle>(); // a list of "Checklists" (it is actually a list of toggles but thats the 2nd part to my checklist)

    //private TouchScreenKeyboard touchScreenKeyboard; // opening iOS specific keyboard
    private string inputText; // blank input initially that is changed after user input

    private float yOffset = 100f; // basic number to help me visualize a layout, i know that you should do it relative to other objects, but for the sake of time i did it like this

    void Start()
    {
        //tmpColor.color = Color.white; // on start turn text white

        LoadItems(); // on start LOAD IN any items that are saved to the JSON, if applicable (explained more below)
    }

    void Update()
    {
        HandleTextEntry(); // on every update, we need to check if the user is inputting something

        RemoveCheckedItems(); // on every update we also need to check and see if the user is removing a checklist item via toggle
    }

    void HandleTextEntry() // revamped from iOS
    {
        if (Input.GetKeyDown(KeyCode.Return)) // check if the return/enter key is pressed
        {
            string itemName = tmpInputField.text; // get the text from the input field

            if (!string.IsNullOrEmpty(itemName)) // if the input is not empty
            {
                AddNewItem(itemName); // add a new item with the provided name
                tmpInputField.text = ""; // reset
            }
        }
    }

    public void AddNewFishName(string fishName = "") // always makes it empty so that it will always be the TMP input field
    {
        if (fishName == ""){ // if there is an empty string, then the itemName will be the input in the TMP (it will always be empty, we never call it with a string)

            fishName = tmpInputField.text;
        } 
        Debug.Log(fishName);
        //SaveFishInfo();

        if(!string.IsNullOrEmpty(fishName))
        {
            Vector2 centerItem = new Vector2(0, 20);
            GameObject newItem = Instantiate(fishNameHolderPrefab, fishNameHolderContainer);
            TextMeshProUGUI fishNameText = newItem.GetComponentInChildren<TextMeshProUGUI>();
            fishNameText.text = fishName;
        }
         
    }

    public void SaveFishInfoName()
    {

    }

    public void AddNewItem(string itemName = "") // a function that takes an optional string of text in order to instantiate a checklist prefab 
    {


        if (itemName == ""){ // if there is an empty string, then the itemName will be the input in the TMP (it will always be empty, we never call it with a string)

            itemName = tmpInputField.text;

        }
        Debug.Log(itemName); // working

        SaveItem(itemName); // once the variable is populated with our text, call our Save to JSON function

        if (!string.IsNullOrEmpty(itemName)) // and now since the variable is not empty,
        {
            Vector2 centerItem = new Vector2(0, 20); // variable for initial starting position of the list (1200, 750) -> 

            GameObject newItem = Instantiate(checklistItemPrefab, checklistContainer); // we instantiate a Checklist prefab (toggle + string) at the original position of the transform (out of sight)

            TextMeshProUGUI itemNameText = newItem.GetComponentInChildren<TextMeshProUGUI>(); // we create another variable to get the text inside the prefab we spawned out of sight
        
            itemNameText.text = itemName; // the text in this variable is now the text copied from our TMP (added to our prefab) and applied as a Text for a TMP

            Toggle toggle = newItem.GetComponentInChildren<Toggle>(); // we create another variable to get the toggle inside the checklist prefab we spawned out of sight

            toggleList.Add(toggle); // append it to the list of toggles (useful later)

            toggle.isOn = false; // automatically apply the toggle as false, we don't want this checked until the user taps it

            RectTransform newItemRect = newItem.GetComponent<RectTransform>(); // get the rect transform of our checklist prefab out of sight

            if (toggleList.Count == 1) // if it is the first to ever be added to the list of toggles, then we know it needs the original position
            {
                newItemRect.anchoredPosition = centerItem; // give its rect transform the original position variable
            }
            else // otherwise, there is already information in that spot
            {
                newItemRect.anchoredPosition = new Vector2(0, 0 - yOffset * (toggleList.Count - 1)); // we will then move downward in the y axis based on how many exist in the list (creating a visual checklist)
            }
        }
    }


    public void RemoveCheckedItems() // function to remove the items from the checklist 
    {
        List<Toggle> itemsToRemove = new List<Toggle>(); // add the items we want to remove to another list

        foreach (Toggle toggle in toggleList) // for every toggle located in that list
        {
            if (toggle.isOn) // if the toggle has been ticked on at any point, the user tapped its completion, so we need to remove
            {
                itemsToRemove.Add(toggle); // add to the list to remove
            }
        }

        foreach (Toggle toggle in itemsToRemove) // for each toggle in the list to REMOVE...
        {
            int indexToRemove = toggleList.IndexOf(toggle); // get the index of that toggle

            toggleList.Remove(toggle); // and remove it from the original list

            GameObject itemToRemove = toggle.transform.parent.gameObject; // get the actual checklist prefab to remove and add to gameobject variable

            TextMeshProUGUI textMeshRemove = itemToRemove.GetComponentInChildren<TextMeshProUGUI>(); // get the text located with the toggle 

            DeleteItem(textMeshRemove.text); // add this to our delete JSON function (we don't need to save it anymore)

            Destroy(itemToRemove); // finally, actually destroy all parts of our prefab


            for (int i = indexToRemove; i < toggleList.Count; i++) // using the index we pulled earlier at the remove index in the original list, we need to move everything once removing one (in the middle for example)
            {
                RectTransform itemRect = toggleList[i].transform.parent.GetComponent<RectTransform>(); // apply the rect transform to a variable

                itemRect.anchoredPosition = CalculatePosition(i); // give them a new position as you itterate, take the place of the original and move every one under it up to that place and give them a new position
            }
        }
    }

    private Vector2 CalculatePosition(int index) // take the index 
    {
        return new Vector2(0, 0 - yOffset * index); // return the new index based on our numbers from before
    }

    [System.Serializable] // visuaize the data basically 
    public class JsonData // make a JSON class to store our strings
    {
        public string[] strings; // store our strings, we only need our string because that is used to apply our position, toggle, everything in our AddNewItem()
    }

    void SaveItem(string item) // save to our JSON
    {
        string filePath = Path.Combine(Application.persistentDataPath, "dataNew.json"); // define / check by making a file var that holds our JSON path + file

        if (File.Exists(filePath)) // if the file exists, 
        {
            string existingJson = File.ReadAllText(filePath); // read the existing file (could be full, could be blank)

            JsonData existingData = JsonUtility.FromJson<JsonData>(existingJson); // deserialize JSON string to JSON object (which we need it to be an object to add it)

            List<string> currentList = new List<string>(existingData.strings); // convert the data in array form to split list form

            if (!currentList.Contains(item)) // if the current list already has the existing item, don't replicate it again
            {
                List<string> updatedList = new List<string>(existingData.strings); // new var for the new information

                updatedList.Add(item); // append the new information

                existingData.strings = updatedList.ToArray(); // add the new information to the array again so we can access it as JSON object

                string updatedJson = JsonUtility.ToJson(existingData); // serialize to new string 

                File.WriteAllText(filePath, updatedJson); // write the new string to the JSON file
            }

        }
        else
        {
            JsonData newData = new JsonData // otherwise the file doesn't exist and we need to add it to a new JSON file
            {
                strings = new string[] { item } // instantiating the file with our added string array
            };

            string newJson = JsonUtility.ToJson(newData); // serialize the data to new JSON string

            File.WriteAllText(filePath, newJson); // write the string to the file 
        }
    }

    void DeleteItem(string item) // delete an item from our JSON file function
    {
        string filePath = Path.Combine(Application.persistentDataPath, "dataNew.json"); // define / check by making file var that holds our JSON + path again

        if (File.Exists(filePath)) // if it exists
        {
            string existingJson = File.ReadAllText(filePath); //read the existing strings from the file

            JsonData existingData = JsonUtility.FromJson<JsonData>(existingJson); // deserialize the string to a JSON object

            List<string> updatedList = new List<string>(existingData.strings); // convert from array to a list 

            if (updatedList.Contains(item)) // if item already exists in the list
            {
                updatedList.Remove(item); // remove it
            }
            else
            {
                updatedList.Add(item); // otherwise it doesn't exist (it should always exist unless an accident happens)
            }

            existingData.strings = updatedList.ToArray(); // update the JSON with our modified list (make it back into array)

            string updatedJson = JsonUtility.ToJson(existingData); // serialize JSON data

            File.WriteAllText(filePath, updatedJson); // write it to the file with the updated things removed 
        }
        else
        {
            JsonData newData = new JsonData // if the file doesnt exist, create a new one
            {
                strings = new string[] { item } // with an array + our string
            };

            string newJson = JsonUtility.ToJson(newData); // serialize data

            File.WriteAllText(filePath, newJson); // write string to file 
        }
    }

    void LoadItems()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "dataNew.json"); // define / check the path with the var

        if (File.Exists(filePath)) // if existence
        {
            string json = File.ReadAllText(filePath); //load everything in the JSON and read it

            JsonData loadedData = JsonUtility.FromJson<JsonData>(json); //convert the string to a JSON object

            for (int i = 0; i < loadedData.strings.Length; i++) // load each one back into the app
            {
                Debug.Log(loadedData.strings[i]);
                Debug.Log("is this being called");
                AddNewItem(loadedData.strings[i]); // by using the access to the string within the loadedData var
            }
        }
    }
}
