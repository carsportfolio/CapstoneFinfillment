using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;

public class AchievementSelectionBehavior : MonoBehaviour
{
    [System.Serializable]
    public class Achievement
    {
        public string achievementName;
        public int reward;
        public string description;
        public Vector3 position;
        public Boolean isCompleted;

        //can add an image reference

        public Achievement(string name, int reward, string description, Vector3 positionVec3, Boolean completed)
        {
            this.achievementName = name;
            this.reward = reward;
            this.description = description;
            this.position = positionVec3;
            this.isCompleted = completed;
        }
    }

    [SerializeField] private List<Achievement> achievementList = new List<Achievement>(); // list of items + their info
    [SerializeField] private GameObject buttonPrefab; // prefab of the button to instantiate
    [SerializeField] private int numberOfButtons = 6; // number of buttons to create
    [SerializeField] private GameObject groupGameObject; // AchievementBlocks
    private SceneSystem sceneFunctions;

    void Start()
    {
        sceneFunctions = FindObjectOfType<SceneSystem>(); // better than just having an empty variable

        // make your items
        Achievement item1 = new Achievement("A1", 100, "Description of 1", new Vector3(-90, 90, 1), false);
        achievementList.Add(item1);

        Achievement item2 = new Achievement("A2", 200, "Description of 2", new Vector3(0, 90, 1), false);
        achievementList.Add(item2);

        Achievement item3 = new Achievement("A3", 300, "Description of 3", new Vector3(90, 90, 1), false);
        achievementList.Add(item3);

        Achievement item4 = new Achievement("A4", 100, "Description of 4", new Vector3(-90, 30, 1), true);
        achievementList.Add(item4);

        Achievement item5 = new Achievement("A5", 200, "Description of 5", new Vector3(0, 30, 1), false);
        achievementList.Add(item5);

        Achievement item6 = new Achievement("A6", 200, "Description of 6", new Vector3(90, 30, 1), false);
        achievementList.Add(item6);

        PopulateButtons();
    }

    void Update()
    {
        for(int i = 0; i < achievementList.Count; i++)
        {
            if (achievementList[i].isCompleted)
            {
                achievementList.RemoveAt(i);
            }
        }
    }

    void PopulateButtons()
    {
        if (groupGameObject == null)
        {
            Debug.LogError("Group GameObject reference is not set.");
            return;
        }

        float totalHeight = numberOfButtons * (buttonPrefab.GetComponent<RectTransform>().sizeDelta.y + 30f);
        Vector3 startPos = new Vector3(0f, totalHeight / 2f, 0f);

        bool groupActiveState = groupGameObject.activeSelf;
        groupGameObject.SetActive(true); // Activate temporarily to assign buttons

        for (int i = 0; i < achievementList.Count; i++)
        {
            Achievement achievement = achievementList[i];
            GameObject buttonItem = Instantiate(buttonPrefab, groupGameObject.transform);
            buttonItem.transform.localPosition = achievement.position;

            TextMeshProUGUI buttonText = buttonItem.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = "A" + (i + 1);

            Button buttonComponent = buttonItem.GetComponent<Button>();
            if (buttonComponent != null) //shouldnt be
            {
                int buttonIndex = i; //current index that has been triggered
                buttonComponent.onClick.AddListener(() => OnButtonClick(buttonIndex));
            }
        }

        groupGameObject.SetActive(groupActiveState);
    }

    void OnButtonClick(int buttonIndex)
    {
        if (buttonIndex >= 0 && buttonIndex < achievementList.Count)
        {
            Achievement selectedItem = achievementList[buttonIndex];
            if (sceneFunctions != null)
            {
                sceneFunctions.InfoPopUpAchievementButton(selectedItem, selectedItem.achievementName, selectedItem.reward, selectedItem.description);
            }
            else
            {
                Debug.LogError("SceneSystem reference is not set.");
            }
        }
        else
        {
            Debug.LogError("Invalid button index.");
        }
    }
}
