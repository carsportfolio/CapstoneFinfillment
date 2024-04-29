using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using TMPro;

//This script handles the "scenes" the viewer can interact with. Hides and shows a lot of the interactivity based on conditions.
public class SceneSystem : MonoBehaviour
{
    [SerializeField] private GameObject finfillmentScreenGroup;
    [SerializeField] private GameObject finfillmentScreenText;
    [SerializeField] private GameObject finfillmentScreenTime;
    [SerializeField] private GameObject finfillmentAddButton;
    [SerializeField] private GameObject finfillmentQuestionMark;
    [SerializeField] private GameObject finfillmentStore;
    [SerializeField] private GameObject finfillmentAchievements;
    //[SerializeField] private GameObject finfillmentHome;
    [SerializeField] private GameObject finfillmentGoals;
    [SerializeField] public GameObject finfillmentList; // this is a button
    [SerializeField] private GameObject finfillmentListOfFish;
    [SerializeField] private GameObject tutorialScreenGroup;
    [SerializeField] public GameObject tutorialAddButton;
    [SerializeField] private GameObject homeScreenGroup;
    [SerializeField] private GameObject homeScreenStartButton;
    [SerializeField] private GameObject homeScreenReturningButton;
    //[SerializeField] private GameObject testScreenGroup;
    [SerializeField] private GameObject popUpGroup;
    [SerializeField] public GameObject popUpTrackingButton;
    [SerializeField] private GameObject popUpAddButton;
    [SerializeField] private TMP_Text popUpFishName;
    [SerializeField] private TMP_Text popUpFishGoal;
    [SerializeField] private TMP_Text popUpFishGoalType;
    [SerializeField] private TMP_Text popUpFishAchievementCategory;
    [SerializeField] private Image popUpFishImage;
    [SerializeField] private GameObject addFishBackButton;
    [SerializeField] private GameObject addFishBackground;
    [SerializeField] private GameObject addFishBackgroundT;
    [SerializeField] private GameObject addFishGroup;
    [SerializeField] private GameObject addFishPopUp1;
    [SerializeField] private GameObject addFishPopUp2;
    [SerializeField] private GameObject addFishPopUp3;
    [SerializeField] private GameObject addFishPopUp4;
    [SerializeField] private GameObject addFishPopUp5;
    [SerializeField] private GameObject addFishGroupT;
    [SerializeField] private GameObject addFishPopUp1T;
    [SerializeField] private GameObject addFishPopUp2T;
    [SerializeField] private GameObject addFishPopUp3T;
    [SerializeField] private GameObject addFishPopUp4T;
    [SerializeField] private GameObject addFishPopUp5T;
    [SerializeField] private GameObject addFishQuestionMark;
    [SerializeField] private GameObject infoGroup;
    [SerializeField] private TMP_Text infoGroupText;
    [SerializeField] private GameObject infoGroupX;
    [SerializeField] private Button infoGroupNext;
    [SerializeField] private Button infoGroupBackNext;
    [SerializeField] private GameObject storeGroup;
    [SerializeField] private GameObject storeGroupBackButton;
    [SerializeField] private TMP_Text storeGroupText;
    [SerializeField] private GameObject storeGroupQuestionMark;
    [SerializeField] private GameObject storeGroupItems;
    [SerializeField] private Button storeBuyButton;
    [SerializeField] private TMP_Text storeMoneyText;
    [SerializeField] private GameObject achievementGroup;
    [SerializeField] private TMP_Text achievementText;
    [SerializeField] private GameObject achievementBlocks;
    [SerializeField] private GameObject achievementQuestionMark;
    [SerializeField] private GameObject achievementBackButton;
    [SerializeField] private Button fishListQuestionMark;
    [SerializeField] private Button fishListBackButton;
    [SerializeField] private TMP_Text fishListText;
    [SerializeField] public GameObject fishListGroup; // this is the group that holds the fish not the button prefab
    [SerializeField] private SpriteRenderer fishListBackground;
    [SerializeField] private Button fishGoalsQuestionMark;
    [SerializeField] private Button fishGoalsBackButton;
    [SerializeField] private TMP_Text fishGoalsText;
    [SerializeField] public GameObject fishGoalsGroup; // again, group that holds the fish nothing more
    [SerializeField] private SpriteRenderer fishGoalsBackground;
    [SerializeField] private GameObject fishGoalsHolder;
    [SerializeField] private Stack<GameObject> triggeredGroups = new Stack<GameObject>();
    [SerializeField] private bool doesGameExist; // once you press the continue button, this var is true as in now there is a returning spot
    public TimeSystem timeHolder;
    public StoreBehavior storeHolder;
    [SerializeField] public FishBehavior fish; // red fish. not in scene
    [SerializeField] public GameObject parentObjectPopUp; 
    public int money;
    [SerializeField] public Button trackButton;
    private Boolean isButtonListenerAdded = false;

    void Start()
    {
        storeHolder = FindObjectOfType<StoreBehavior>();

        storeBuyButton.gameObject.SetActive(false);
        storeMoneyText.gameObject.SetActive(false);

        fishListBackButton.gameObject.SetActive(false);
        fishListGroup.gameObject.SetActive(false);
        fishListQuestionMark.gameObject.SetActive(false);
        fishListText.gameObject.SetActive(false);
        fishListBackground.gameObject.SetActive(false);
        finfillmentListOfFish.SetActive(false);
        
        homeScreenGroup.SetActive(true);
        homeScreenReturningButton.SetActive(false);

        //finfillmentScreenGroup.SetActive(false);
        finfillmentScreenText.SetActive(false);
        finfillmentScreenTime.SetActive(false);
        finfillmentAddButton.SetActive(false);
        finfillmentQuestionMark.SetActive(false);
        finfillmentGoals.SetActive(false);
        finfillmentList.SetActive(false);
        //finfillmentHome.SetActive(false);
        finfillmentAchievements.SetActive(false);
        finfillmentStore.SetActive(false);

        tutorialScreenGroup.SetActive(false);
        tutorialAddButton.SetActive(false);
        //testScreenGroup.SetActive(false);

        popUpGroup.SetActive(false);
        popUpTrackingButton.SetActive(false);
        popUpAddButton.SetActive(false);
        popUpFishAchievementCategory.gameObject.SetActive(false);
        popUpFishGoal.gameObject.SetActive(false);
        popUpFishGoalType.gameObject.SetActive(false);
        popUpFishImage.gameObject.SetActive(false);
        popUpFishName.gameObject.SetActive(false);

        addFishGroup.SetActive(false);
        addFishBackButton.SetActive(false);
        addFishPopUp1.SetActive(false);
        addFishPopUp2.SetActive(false);
        addFishPopUp3.SetActive(false);
        addFishPopUp4.SetActive(false);
        addFishPopUp5.SetActive(false);

        addFishGroupT.SetActive(false);
        addFishGroupT.SetActive(false);
        addFishPopUp1T.SetActive(false);
        addFishPopUp2T.SetActive(false);
        addFishPopUp3T.SetActive(false);
        addFishPopUp4T.SetActive(false);
        addFishPopUp5T.SetActive(false);

        infoGroup.SetActive(false);
        infoGroupText.enabled = false;
        infoGroupX.SetActive(false);
        infoGroupNext.gameObject.SetActive(false);
        infoGroupBackNext.gameObject.SetActive(false);
        infoGroupText.fontSize = 16;

        storeGroup.SetActive(false);
        storeGroupBackButton.SetActive(false);
        storeGroupText.enabled = false;
        storeGroupQuestionMark.SetActive(false);
        storeGroupItems.SetActive(false);

        achievementBackButton.SetActive(false);
        achievementBlocks.SetActive(false);
        achievementGroup.SetActive(false);
        achievementText.enabled = false;
        achievementQuestionMark.SetActive(false);

        fishGoalsBackButton.gameObject.SetActive(false);
        fishGoalsQuestionMark.gameObject.SetActive(false);
        fishGoalsText.gameObject.SetActive(false);
        fishGoalsBackground.gameObject.SetActive(false);
        finfillmentGoals.SetActive(false);
        fishGoalsGroup.SetActive(false);
        fishGoalsHolder.SetActive(false);
    }

    void Update()
    {
        if(doesGameExist == true)
        {
            homeScreenStartButton.SetActive(false);
            homeScreenReturningButton.SetActive(true);

            timeHolder = FindObjectOfType<TimeSystem>();
        }
        
        storeMoneyText.text = "Money: " + money;
    }

    public void StartToReturningButton() 
    {
        doesGameExist = true;
        Debug.Log("game now exists"); // after tutorial completion there will be a variable saved to make sure you never see the beginning screen with the ability to do the tutorial again

        // save and load code
    }

    public void HomeToTutorial() //home (start) to tutorial
    {
        homeScreenGroup.SetActive(false);
        tutorialAddButton.SetActive(true);
        tutorialScreenGroup.SetActive(true);
        Debug.Log("tutorial inserted here, continue");
    }

    public void HomeToFinfillment() // home (returning) to gameplay
    {
        homeScreenGroup.SetActive(false);
        finfillmentScreenGroup.SetActive(true);
        foreach (Transform child in finfillmentScreenGroup.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void TutorialToFinfillment() // after completing the tutorial, now you have access to the game
    {
        homeScreenGroup.SetActive(false);
        tutorialScreenGroup.SetActive(false);
        finfillmentScreenGroup.SetActive(true);
        foreach (Transform child in finfillmentScreenGroup.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void PopUpFish(FishProperties fishProp)
    {
        popUpGroup.SetActive(true);
        popUpTrackingButton.SetActive(true);
        popUpFishAchievementCategory.gameObject.SetActive(true);
        popUpFishGoal.gameObject.SetActive(true);
        popUpFishGoalType.gameObject.SetActive(true);
        popUpFishImage.gameObject.SetActive(true);
        popUpFishName.gameObject.SetActive(true);

        int numberOfTracks = fishProp.StreakTracked;
        SpriteRenderer spriteRenderer = fishProp.GetComponent<SpriteRenderer>();

        popUpFishName.text = fishProp.Name;
        popUpFishGoal.text = "Goal: " + fishProp.Goal;
        popUpFishGoalType.text = "Goal Type: " + fishProp.GoalType;
        popUpFishAchievementCategory.text = "Specialty: " + fishProp.AchievementsType;
        popUpFishImage.color = spriteRenderer.color;

        for (int i = 0; i < parentObjectPopUp.transform.childCount; i++)
        {
            GameObject child = parentObjectPopUp.transform.GetChild(i).gameObject;

            Image renderer = child.GetComponent<Image>();
            
            if (renderer != null)
            {
                if (i < numberOfTracks)
                {
                    renderer.color = Color.blue;
                }
                else
                {
                    renderer.color = Color.white;
                }
            }
        }
    }

    public void PopUpFishFinish()
    {
        if(money > 0)
        {
            popUpTrackingButton.SetActive(false);
        }

        popUpGroup.SetActive(false);
        popUpTrackingButton.SetActive(false);
        popUpFishAchievementCategory.gameObject.SetActive(false);
        popUpFishGoal.gameObject.SetActive(false);
        popUpFishGoalType.gameObject.SetActive(false);
        popUpFishImage.gameObject.SetActive(false);
        popUpFishName.gameObject.SetActive(false);
    }

    public void AddFishGroup()
    {
        addFishGroup.SetActive(true); // this doesnt work for the background
        addFishBackButton.SetActive(true);
        addFishQuestionMark.SetActive(true);
        addFishBackground.SetActive(true);

        addFishPopUp1.SetActive(true);
        addFishPopUp2.SetActive(false);
        addFishPopUp3.SetActive(false);
        addFishPopUp4.SetActive(false);
        addFishPopUp5.SetActive(false);

        finfillmentScreenGroup.SetActive(false);
        foreach (Transform child in finfillmentScreenGroup.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void AddFishGroupTutorial()
    {
        addFishGroupT.SetActive(true);
        addFishBackgroundT.SetActive(true);

        addFishPopUp1T.SetActive(true);
        addFishPopUp2T.SetActive(false);
        addFishPopUp3T.SetActive(false);
        addFishPopUp4T.SetActive(false);
        addFishPopUp5T.SetActive(false);

        //finfillmentScreenGroup.SetActive(false);
        finfillmentScreenText.SetActive(false);
        finfillmentScreenTime.SetActive(false);
        finfillmentAddButton.SetActive(false);
        finfillmentQuestionMark.SetActive(false);
        finfillmentGoals.SetActive(false);
        finfillmentList.SetActive(false);
        finfillmentListOfFish.SetActive(false);
        //finfillmentHome.SetActive(false);
        finfillmentAchievements.SetActive(false);
        finfillmentStore.SetActive(false);

        tutorialAddButton.SetActive(false);
        tutorialScreenGroup.SetActive(false);
        addFishQuestionMark.SetActive(false);
    }

    public void AddFishGroupFinish()
    {
        addFishGroup.SetActive(false);
        addFishBackButton.SetActive(false);
        addFishQuestionMark.SetActive(false);
        addFishBackground.SetActive(false);

        addFishPopUp1.SetActive(false);
        addFishPopUp2.SetActive(false);
        addFishPopUp3.SetActive(false);
        addFishPopUp4.SetActive(false);
        addFishPopUp5.SetActive(false);

        finfillmentScreenGroup.SetActive(true);
        foreach (Transform child in finfillmentScreenGroup.transform)
        {
            child.gameObject.SetActive(true);
        }
    }
    public void AddFishGroupFinishTutorial()
    {
        addFishGroupT.SetActive(false);
        addFishBackgroundT.SetActive(false);

        addFishPopUp1T.SetActive(false);
        addFishPopUp2T.SetActive(false);
        addFishPopUp3T.SetActive(false);
        addFishPopUp4T.SetActive(false);
        addFishPopUp5T.SetActive(false);

        finfillmentScreenGroup.SetActive(true);
        finfillmentScreenText.SetActive(true);
        finfillmentScreenTime.SetActive(true);
        finfillmentAddButton.SetActive(true);
        finfillmentQuestionMark.SetActive(true);
        finfillmentGoals.SetActive(true);
        finfillmentList.SetActive(true);
        //finfillmentHome.SetActive(true);
        finfillmentAchievements.SetActive(true);
        finfillmentStore.SetActive(true);
    }

    public void FinfillmentAchievementActivate()
    {
        finfillmentScreenGroup.SetActive(false);
        finfillmentScreenText.SetActive(false);
        finfillmentScreenTime.SetActive(false);
        finfillmentAddButton.SetActive(false);
        finfillmentQuestionMark.SetActive(false);
        finfillmentGoals.SetActive(false);
        finfillmentList.SetActive(false);
        //finfillmentHome.SetActive(false);
        finfillmentAchievements.SetActive(false);
        finfillmentStore.SetActive(false);

        achievementBackButton.SetActive(true);
        achievementBlocks.SetActive(true);
        achievementGroup.SetActive(true);
        achievementText.enabled = true;
        achievementQuestionMark.SetActive(true);
    }

    public void FinfillmentAchievementDeactivate()
    {
        finfillmentScreenGroup.SetActive(true);
        finfillmentScreenText.SetActive(true);
        finfillmentScreenTime.SetActive(true);
        finfillmentAddButton.SetActive(true);
        finfillmentQuestionMark.SetActive(true);
        finfillmentGoals.SetActive(true);
        finfillmentList.SetActive(true);
        //finfillmentHome.SetActive(true);
        finfillmentAchievements.SetActive(true);
        finfillmentStore.SetActive(true);

        achievementBackButton.SetActive(false);
        achievementBlocks.SetActive(false);
        achievementGroup.SetActive(false);
        achievementText.enabled = false;
        achievementQuestionMark.SetActive(false);
    }

    public void FinfillmentStoreActivate()
    {
        finfillmentScreenGroup.SetActive(false);
        foreach (Transform child in finfillmentScreenGroup.transform)
        {
            child.gameObject.SetActive(false);
        }

        storeGroup.SetActive(true);
        storeGroupBackButton.SetActive(true);
        storeGroupText.enabled = true;
        storeGroupQuestionMark.SetActive(true);
        storeGroupItems.SetActive(true);
        storeMoneyText.gameObject.SetActive(true);
    }

    public void FinfillmentStoreDeactivate()
    {
        finfillmentScreenGroup.SetActive(true);
        foreach (Transform child in finfillmentScreenGroup.transform)
        {
            child.gameObject.SetActive(true);
        }

        storeGroup.SetActive(false);
        storeGroupBackButton.SetActive(false);
        storeGroupText.enabled = false;
        storeGroupQuestionMark.SetActive(false);
        storeGroupItems.SetActive(false);
        storeMoneyText.gameObject.SetActive(false);
    }

    public void FinfillmentListActivate()
    {
        finfillmentScreenGroup.SetActive(false);
        foreach (Transform child in finfillmentScreenGroup.transform)
        {
            child.gameObject.SetActive(false);
        }

        fishListGroup.SetActive(true);
        foreach (Transform child in fishListGroup.transform)
        {
            child.gameObject.SetActive(true);
        }

        fishListBackButton.gameObject.SetActive(true);
        fishListQuestionMark.gameObject.SetActive(true);
        fishListText.gameObject.SetActive(true);
        fishListBackground.gameObject.SetActive(true);
        finfillmentListOfFish.SetActive(true);

        fish.PopulateFishList();
    }

    public void FinfillmentListDeactivate()
    {
        finfillmentScreenGroup.SetActive(true);
        foreach (Transform child in finfillmentScreenGroup.transform)
        {
            child.gameObject.SetActive(true);
        }

        fishListGroup.SetActive(false);
        foreach (Transform child in fishListGroup.transform)
        {
            child.gameObject.SetActive(false);
        }

        fishListBackButton.gameObject.SetActive(false);
        fishListQuestionMark.gameObject.SetActive(false);
        fishListText.gameObject.SetActive(false);
        fishListBackground.gameObject.SetActive(false);
        finfillmentListOfFish.SetActive(false);
    }

    public void FinfillmentGoalsActivate()
    {
        finfillmentScreenGroup.SetActive(false);
        foreach (Transform child in finfillmentScreenGroup.transform)
        {
            child.gameObject.SetActive(false);
        }

        fishGoalsGroup.SetActive(true);
        foreach (Transform child in fishGoalsGroup.transform)
        {
            child.gameObject.SetActive(true);
        }

        fishGoalsBackButton.gameObject.SetActive(true);
        fishGoalsQuestionMark.gameObject.SetActive(true);
        fishGoalsText.gameObject.SetActive(true);
        fishGoalsBackground.gameObject.SetActive(true);
        fishGoalsHolder.SetActive(true);

        fish.PopulateFishGoals();
    }

    public void FinfillmentGoalsDeactivate()
    {
        finfillmentScreenGroup.SetActive(true);
        foreach (Transform child in finfillmentScreenGroup.transform)
        {
            child.gameObject.SetActive(true);
        }

        fishGoalsGroup.SetActive(false);
        foreach (Transform child in fishGoalsGroup.transform)
        {
            child.gameObject.SetActive(false);
        }

        fishGoalsBackButton.gameObject.SetActive(false);
        fishGoalsQuestionMark.gameObject.SetActive(false);
        fishGoalsText.gameObject.SetActive(false);
        fishGoalsBackground.gameObject.SetActive(false);
        fishGoalsHolder.SetActive(false);
    }

    public void FinfillmentFishProfileActivate(FishProperties item, string name)
    {
        Debug.Log("fishProfile: " + item + "Name: " + name);
    }

    public void FinfillmentFishProfileDeactivate()
    {

    }

    public void SetActiveTrueAll()
    {
        foreach (Transform child in finfillmentScreenGroup.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    private void DeactivateLastTriggeredGroup()
    {
        if (triggeredGroups.Count > 0)
        {
            GameObject lastGroup = triggeredGroups.Peek();
            lastGroup.SetActive(false);
        }
    }

    public void AddFishPopUp2Start()
    {
        addFishPopUp2.SetActive(true);
    }
    public void AddFishPopUp3Start()
    {
        addFishPopUp3.SetActive(true);
    }
    public void AddFishPopUp4Start()
    {
        addFishPopUp4.SetActive(true);
    }
    public void AddFishPopUp5Start()
    {
        addFishPopUp5.SetActive(true);
    }
    public void AddFishPopUp2StartT()
    {
        addFishPopUp2T.SetActive(true);
    }
    public void AddFishPopUp3StartT()
    {
        addFishPopUp3T.SetActive(true);
    }
    public void AddFishPopUp4StartT()
    {
        addFishPopUp4T.SetActive(true);
    }
    public void AddFishPopUp5StartT()
    {
        addFishPopUp5T.SetActive(true);
    }

    public void InfoPopUpTutorial1()
    {
        infoGroup.SetActive(true);
        infoGroupText.enabled = true;
        infoGroupX.SetActive(true);
        infoGroupNext.gameObject.SetActive(false);
        infoGroupBackNext.gameObject.SetActive(false);

        storeBuyButton.gameObject.SetActive(false);

        infoGroupText.text = "          First, we will start with tapping one of the three categories for your goal: Productivity, Mental, or Physical. \n\n Since this is the tutorial, I will suggest the options for you. \n\n Tap the last, red, button to select the Physical category.";
    }

    public void InfoPopUpTutorial2()
    {
        infoGroup.SetActive(true);
        infoGroupText.enabled = true;
        infoGroupX.SetActive(true);
        infoGroupNext.gameObject.SetActive(false);
        infoGroupBackNext.gameObject.SetActive(false);

        storeBuyButton.gameObject.SetActive(false);

        infoGroupText.text = "           Nice job! Our fish is now tied to a specific achievement branch which we can start earning rewards for immediately after creation! \n\n Next, we will go ahead and give our fish a name. \n\n Let's name him Fishy!";
    }

    public void InfoPopUpTutorial3()
    {
        infoGroup.SetActive(true);
        infoGroupText.enabled = true;
        infoGroupX.SetActive(true);
        infoGroupNext.gameObject.SetActive(false);
        infoGroupBackNext.gameObject.SetActive(false);

        storeBuyButton.gameObject.SetActive(false);

        infoGroupText.text = "           Perfect! Now that our fish has a name, let's give it a goal. \n\n Remember, you should have a goal description that matches one of the three categories as best as you possibly can. It can be anything! \n\n We will focus on walking a mile.";
    }

    public void InfoPopUpTutorial4()
    {
        infoGroup.SetActive(true);
        infoGroupText.enabled = true;
        infoGroupX.SetActive(true);
        infoGroupNext.gameObject.SetActive(false);
        infoGroupBackNext.gameObject.SetActive(false);

        storeBuyButton.gameObject.SetActive(false);

        infoGroupText.text = "           Awesome! Now let's be sure to set the goal to daily for a daily progress check and reminder. ";
    }

    public void InfoPopUpTutorial5()
    {
        infoGroup.SetActive(true);
        infoGroupText.enabled = true;
        infoGroupX.SetActive(true);
        infoGroupNext.gameObject.SetActive(false);
        infoGroupBackNext.gameObject.SetActive(false);

        storeBuyButton.gameObject.SetActive(false);

        infoGroupText.text = "           Awesome! Now that we have all the information filled out, we can focus on customizing! \n\n If you had any other animal types, you could tap the arrows to cycle through them. \n\n For now, we will just choose the basic goldfish in the middle.";
    }

    public void InfoPopUpTutorial6()
    {
        infoGroup.SetActive(true);
        infoGroupText.enabled = true;
        infoGroupX.SetActive(true);
        infoGroupNext.gameObject.SetActive(false);
        infoGroupBackNext.gameObject.SetActive(false);
        //tutorialFishButton.SetActive(true);

        storeBuyButton.gameObject.SetActive(false);

        infoGroupText.text = "           Fantastic! You have completed the process of making your first fish. \n\n Take care of it by tracking progress, building streaks, and completing goals! \n\n\n\n\n\n Time for you to dive in!";
    }

    public void InfoPopUpTutorial7()
    {
        infoGroup.SetActive(true);
        infoGroupText.enabled = true;
        infoGroupX.SetActive(true);
        infoGroupNext.gameObject.SetActive(true);
        infoGroupBackNext.gameObject.SetActive(false);
        //tutorialFishButton.SetActive(false);
        infoGroupNext.onClick.AddListener(infoNextT7T8);

        storeBuyButton.gameObject.SetActive(false);

        infoGroupText.text = "           This is your tank. All your fish will live here on your screen! \n\n Their information is just a tap away. Tap Fishy to view his information from his fish profile."; 
    }

    public void infoNextT7T8()
    {
        infoGroupNext.gameObject.SetActive(false);
        infoGroupBackNext.gameObject.SetActive(true);

        storeBuyButton.gameObject.SetActive(false);

        infoGroupText.text = "           + button is for adding fish. \n\n = button is for the list of fish. \n\n Y button is for achievements. \n\n GOALS button is to see what fish represents what goal and their profile. \n\n STORE button is where you can shop for the latest items."; 
        infoGroupBackNext.onClick.AddListener(InfoPopUpTutorial7);
    }
    public void InfoPopUpTutorial8()
    {
        infoGroup.SetActive(true);
        infoGroupText.enabled = true;
        infoGroupX.SetActive(true);
        infoGroupNext.gameObject.SetActive(false);
        infoGroupBackNext.gameObject.SetActive(true);

        storeBuyButton.gameObject.SetActive(false);

        infoGroupText.text = "           There are three ways to access your fish. \n\n You can tap: \n\n - The fish from the tank for specific information. \n\n - The goals tab for a list of fish and their goals. \n\n -  The list tab on the top right corner to see a visual list.";
    }

    public void InfoPopUpAddFishQuestionMark()
    {
        infoGroup.SetActive(true);
        infoGroupText.enabled = true;
        infoGroupX.SetActive(true);

        storeBuyButton.gameObject.SetActive(false);

        infoGroupText.text = "           You are creating a fish that will represent a goal. \n\n The first row represents the fish's goal type: Productivity, Mental, and Physical. \n\n Then, name your fish and describe its goal. You will want to relate the goal to the Achievement category to encourage variety in your life. \n\n Finally, select how often you would like to track the goal.";
    }

    public void InfoPopUpAchievementsQuestionMark()
    {
        infoGroup.SetActive(true);
        infoGroupText.enabled = true;
        infoGroupX.SetActive(true);

        storeBuyButton.gameObject.SetActive(false);

        infoGroupText.text = "           This app helps you focus on three specific goal types: Productivity, Mental, and Physical goals. The goal type will contribute progress to the corrresponding achievement tree. \n\n Strive to complete achivements to unlock rewards, items, money, and more! ";
    }

    public void InfoPopUpFinfillmentListQuestionMark()
    {
        infoGroup.SetActive(true);
        infoGroupText.enabled = true;
        infoGroupX.SetActive(true);

        storeBuyButton.gameObject.SetActive(false);

        infoGroupText.text = "           This is your list of fish who currently have goals active. Click on any of them to be taken to their fish profile.";
    }

    public void InfoPopUpStoreQuestionMark()
    {
        infoGroup.SetActive(true);
        infoGroupText.enabled = true;
        infoGroupX.SetActive(true);
        infoGroupNext.gameObject.SetActive(false);
        infoGroupBackNext.gameObject.SetActive(false);

        storeBuyButton.gameObject.SetActive(false);

        infoGroupText.text = "           The store has different decorations and rewards you can buy with your money earned from streaks. \n\n 1. Track your goal progress each day to earn a daily streak and receive compensation. \n\n 2. Tap the item you want to buy and purchase it.";
    }

    public void InfoPopUpFinfillmentQuestionMark()
    {
        infoGroup.SetActive(true);
        infoGroupText.enabled = true;
        infoGroupX.SetActive(true);
        infoGroupNext.gameObject.SetActive(true);

        storeBuyButton.gameObject.SetActive(false);

        infoGroupText.text = "            This is your fish tank. Fish represent goals. The more you track your goals, the more the fish grow and flourish. Tracking goals leads to rewards, achievements, and money! Click the fish to interact with them.";

        infoGroupNext.onClick.AddListener(infoNextFinfillment);
    }

    public void infoNextFinfillment()
    {
        infoGroupNext.gameObject.SetActive(false);
        infoGroupBackNext.gameObject.SetActive(true);

        storeBuyButton.gameObject.SetActive(false);

        infoGroupText.text = "           + button is for adding fish. \n\n = button is for the list of fish. \n\n Y button is for achievements. \n\n GOALS button is to see what fish represents what goal and their profile. \n\n STORE button is where you can shop for the latest items."; 
        infoGroupBackNext.onClick.AddListener(InfoPopUpFinfillmentQuestionMark);
    }

    public void InfoPopUpTutorialClose()
    {
        infoGroup.SetActive(false);
        infoGroupText.enabled = false;
        infoGroupX.SetActive(false);
        infoGroupNext.gameObject.SetActive(false);
        infoGroupBackNext.gameObject.SetActive(false);

        storeBuyButton.gameObject.SetActive(false);

        infoGroupText.text = "";
    }

    public void InfoPopUpStoreButton(StoreBehavior.StoreItem item, string name, int price, string description)
    {
        if (storeHolder != null)
        {
            storeHolder.currentItem = item; 
        }

        infoGroup.SetActive(true);
        infoGroupText.enabled = true;
        infoGroupX.SetActive(true);
        infoGroupNext.gameObject.SetActive(false);

        storeBuyButton.gameObject.SetActive(true);

        if (!isButtonListenerAdded) // allows the program to only apply one listener since it automatically wants to apply one for every button
        {
            storeBuyButton.onClick.RemoveAllListeners();
            storeBuyButton.onClick.AddListener(() => BuyFunction());
            isButtonListenerAdded = true;
        }

        infoGroupText.text = "            " + name + "\n$" + price + "\n" + description;
    }

    public void BuyFunction()
    {
        if (storeHolder != null)
        {
            if (storeHolder.currentItem != null)
            {
                StoreBehavior.StoreItem currentItem = storeHolder.currentItem;

                if (money >= currentItem.price)
                {
                    money -= currentItem.price;
                    currentItem.isBoughtYet = true;
                    Debug.Log("Buying item: " + currentItem.itemName);

                    storeHolder.currentItem = null;

                    storeHolder.PopulateButtons(); 
                }
                else
                {
                    Debug.Log("Insufficient money");
                }
            }
            else
            {
                Debug.Log("No item selected");
            }
        }
    }

    public void InfoPopUpAchievementButton(AchievementSelectionBehavior.Achievement item, string name, int reward, string description)
    {
        infoGroup.SetActive(true);
        infoGroupText.enabled = true;
        infoGroupX.SetActive(true);
        infoGroupNext.gameObject.SetActive(false);

        storeBuyButton.gameObject.SetActive(true);

        //if (!isButtonListenerAdded) // allows the program to only apply one listener since it automatically wants to apply one for every button
        //{
            //storeBuyButton.onClick.AddListener(() => BuyFunction(price));
            //isButtonListenerAdded = true;
        //}

        infoGroupText.text = "            " + name + "\nReward: $" + reward + "\n" + description;
    }

    public void InfoPopUpGoalsQuestionMark()
    {
        infoGroup.SetActive(true);
        infoGroupText.enabled = true;
        infoGroupX.SetActive(true);
        infoGroupNext.gameObject.SetActive(false);

        storeBuyButton.gameObject.SetActive(false);

        infoGroupText.text = "            Goals";
    }
}
