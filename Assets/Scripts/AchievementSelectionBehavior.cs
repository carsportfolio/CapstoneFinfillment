using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;
using System.Collections;

// the achievement class holds all the code to do with handling achievements, which at the moment is not much since there isnt any interaction

public class AchievementSelectionBehavior : MonoBehaviour
{
    [System.Serializable]
    public class Achievement
    {
        public enum AchievementCategoryWithin
        {
            Physical,
            Mental,
            Productivity
        }

        public string achievementName; 
        public AchievementCategoryWithin achievementGroup;
        public int achievementOutsideNumberHolder;
        public int reward;
        public string description;
        public Vector3 position;
        public Boolean isCompleted;

        //can add an image reference

        public Achievement(string name, AchievementCategoryWithin group, int reward, string description, Vector3 positionVec3, Boolean completed, int number)
        {
            this.achievementGroup = group;
            this.achievementName = name;
            this.reward = reward;
            this.description = description;
            this.position = positionVec3;
            this.isCompleted = completed;
            this.achievementOutsideNumberHolder = number;
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

        Achievement item1 = new Achievement("P1", Achievement.AchievementCategoryWithin.Physical, 100, "Track a Physical related goal for the first time.", new Vector3(-90, 90, 1), true, 1);
        achievementList.Add(item1);

        Achievement item2 = new Achievement("M1", Achievement.AchievementCategoryWithin.Mental, 100, "Track a Mental related goal for the first time.", new Vector3(0, 90, 1), true, 1);
        achievementList.Add(item2);

        Achievement item3 = new Achievement("R1", Achievement.AchievementCategoryWithin.Productivity, 100, "Track a Productivity related goal for the first time.", new Vector3(90, 90, 1), false, 1);
        achievementList.Add(item3);

        //__________ row 2

        Achievement item4 = new Achievement("P2", Achievement.AchievementCategoryWithin.Physical, 200, "Description of 4", new Vector3(-90, 30, 1), true, 2);
        achievementList.Add(item4);

        Achievement item5 = new Achievement("M2", Achievement.AchievementCategoryWithin.Mental, 200, "Description of 5", new Vector3(0, 30, 1), true, 2);
        achievementList.Add(item5);

        Achievement item6 = new Achievement("R2", Achievement.AchievementCategoryWithin.Productivity, 200, "Description of 6", new Vector3(90, 30, 1), false, 2);
        achievementList.Add(item6);

        //__________ row 3

        Achievement item7 = new Achievement("P3", Achievement.AchievementCategoryWithin.Physical, 300, "Description of 7", new Vector3(-90, -30, 1), true, 3);
        achievementList.Add(item7);

        Achievement item8 = new Achievement("M3", Achievement.AchievementCategoryWithin.Mental, 300, "Description of 8", new Vector3(0, -30, 1), false, 3);
        achievementList.Add(item8);

        Achievement item9 = new Achievement("R3", Achievement.AchievementCategoryWithin.Productivity, 300, "Description of 9", new Vector3(90, -30, 1), false, 3);
        achievementList.Add(item9);

        //__________ row 4

        Achievement item10 = new Achievement("P4", Achievement.AchievementCategoryWithin.Physical, 400, "Description of 10", new Vector3(-90, -90, 1), false, 4);
        achievementList.Add(item10);

        Achievement item11 = new Achievement("M4", Achievement.AchievementCategoryWithin.Mental, 400, "Description of 11", new Vector3(0, -90, 1), false, 4);
        achievementList.Add(item11);

        Achievement item12 = new Achievement("R4", Achievement.AchievementCategoryWithin.Productivity, 400, "Description of 12", new Vector3(90, -90, 1), false, 4);
        achievementList.Add(item12);

        PopulateButtonsAchievement();
    }

    void PopulateButtonsAchievement()
    {
        if (groupGameObject == null)
        {
            Debug.LogError("group gameobject reference in achievement is not set");
            return;
        }

        float totalHeight = numberOfButtons * (buttonPrefab.GetComponent<RectTransform>().sizeDelta.y + 30f);
        Vector3 startPos = new Vector3(0f, totalHeight / 2f, 0f);

        bool groupActiveState = groupGameObject.activeSelf;
        groupGameObject.SetActive(true); // Activate temporarily to assign buttons

        for (int i = 0; i < achievementList.Count; i++)
        {
            Achievement achievement = achievementList[i];
            string achievementNameHolder = achievement.achievementOutsideNumberHolder.ToString();
            Color achievementColorHolder = Color.white;

            switch(achievement.achievementGroup)
            {
                case Achievement.AchievementCategoryWithin.Physical:
                    achievementColorHolder = Color.red;
                break;
                case Achievement.AchievementCategoryWithin.Productivity:
                    achievementColorHolder = Color.yellow;
                break;
                case Achievement.AchievementCategoryWithin.Mental:
                    achievementColorHolder = Color.blue;
                break;
            }

            if (achievement.isCompleted)
            {
                GameObject buttonItem = Instantiate(buttonPrefab, groupGameObject.transform);
                Image buttonRender = buttonItem.GetComponent<Image>();
                buttonItem.transform.localPosition = achievement.position;

                TextMeshProUGUI buttonText = buttonItem.GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = "C" + (i + 1);

                if (buttonItem.CompareTag("buttontint"))
                {
                    if (buttonRender.material != null)
                    {
                        buttonRender.material = new Material(buttonRender.material);
                        buttonRender.material.color = Color.green;
                    }
                    
                    Button buttonComponent = buttonItem.GetComponent<Button>();
                    if (buttonComponent != null) //shouldnt be
                    {
                        int buttonIndex = i; //current index that has been triggered
                        buttonComponent.onClick.AddListener(() => OnButtonClickCompleted(buttonIndex));
                    }

                    continue;
                }
            }
            else
            {
                GameObject buttonItem = Instantiate(buttonPrefab, groupGameObject.transform);
                Image buttonRender = buttonItem.GetComponent<Image>();
                buttonItem.transform.localPosition = achievement.position;

                TextMeshProUGUI buttonText = buttonItem.GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = achievementNameHolder;

                if (buttonRender.material != null)
                {
                    buttonRender.material = new Material(buttonRender.material);
                    buttonRender.material.color = achievementColorHolder;
                }

                Button buttonComponent = buttonItem.GetComponent<Button>();
                if (buttonComponent != null) //shouldnt be
                {
                    int buttonIndex = i; //current index that has been triggered
                    buttonComponent.onClick.AddListener(() => OnButtonClick(buttonIndex));
                }
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
                sceneFunctions.InfoPopUpAchievementButton(selectedItem, selectedItem.achievementName, selectedItem.reward, selectedItem.description, selectedItem.achievementGroup);
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

    void OnButtonClickCompleted(int buttonIndex)
    {
        if (buttonIndex >= 0 && buttonIndex < achievementList.Count)
        {
            Achievement selectedItem = achievementList[buttonIndex];
            if (sceneFunctions != null)
            {
                sceneFunctions.InfoPopUpAchievementButtonCompleted(selectedItem, selectedItem.achievementName, selectedItem.reward, selectedItem.description);
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
