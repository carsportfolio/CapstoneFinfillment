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
    [SerializeField] private GameObject tutorialScreenGroup;
    [SerializeField] public GameObject tutorialAddButton;
    [SerializeField] private GameObject homeScreenGroup;
    [SerializeField] private GameObject homeScreenStartButton;
    [SerializeField] private GameObject homeScreenReturningButton;
    [SerializeField] private GameObject testScreenGroup;
    [SerializeField] private GameObject popUpGroup;
    [SerializeField] public GameObject popUpTrackingButton;
    [SerializeField] private GameObject popUpAddButton;
    [SerializeField] private GameObject addFishBackButton;
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
    [SerializeField] private GameObject infoGroup;
    [SerializeField] private TMP_Text infoGroupText;
    [SerializeField] private GameObject infoGroupX;
    [SerializeField] private Stack<GameObject> triggeredGroups = new Stack<GameObject>();
    [SerializeField] private bool doesGameExist; // once you press the continue button, this var is true as in now there is a returning spot
    public TimeSystem timeHolder;
    [SerializeField] public GameObject parentObjectPopUp; 
    private GameObject[] childrenArrayPopUp;  
    public int money;
    [SerializeField] public Button trackButton;

    void Start()
    {
        homeScreenGroup.SetActive(true);
        homeScreenReturningButton.SetActive(false);

        //finfillmentScreenGroup.SetActive(false);
        finfillmentScreenText.SetActive(false);
        finfillmentScreenTime.SetActive(false);
        finfillmentAddButton.SetActive(false);

        tutorialScreenGroup.SetActive(false);
        tutorialAddButton.SetActive(false);
        testScreenGroup.SetActive(false);

        popUpGroup.SetActive(false);
        popUpTrackingButton.SetActive(false);
        popUpAddButton.SetActive(false);

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
    }

    void Update()
    {
        if(doesGameExist == true)
        {
            homeScreenStartButton.SetActive(false);
            homeScreenReturningButton.SetActive(true);

            timeHolder = FindObjectOfType<TimeSystem>();
        }
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

        int numberOfTracks = fishProp.StreakTracked;

        for (int i = 0; i < parentObjectPopUp.transform.childCount; i++)
        {
            GameObject child = parentObjectPopUp.transform.GetChild(i).gameObject;

            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            
            if (spriteRenderer != null)
            {
                if (i < numberOfTracks)
                {
                    spriteRenderer.color = Color.green;
                }
                else
                {
                    spriteRenderer.color = Color.white;
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
    }

    public void AddFishGroup()
    {
        addFishGroup.SetActive(true);
        addFishBackButton.SetActive(true);

        addFishPopUp1.SetActive(true);
        addFishPopUp2.SetActive(false);
        addFishPopUp3.SetActive(false);
        addFishPopUp4.SetActive(false);
        addFishPopUp5.SetActive(false);

        //finfillmentScreenGroup.SetActive(false);
        finfillmentScreenText.SetActive(false);
        finfillmentScreenTime.SetActive(false);
        finfillmentAddButton.SetActive(false);
    }

    public void AddFishGroupTutorial()
    {
        addFishGroupT.SetActive(true);

        addFishPopUp1T.SetActive(true);
        addFishPopUp2T.SetActive(false);
        addFishPopUp3T.SetActive(false);
        addFishPopUp4T.SetActive(false);
        addFishPopUp5T.SetActive(false);

        //finfillmentScreenGroup.SetActive(false);
        finfillmentScreenText.SetActive(false);
        finfillmentScreenTime.SetActive(false);
        finfillmentAddButton.SetActive(false);
        tutorialAddButton.SetActive(false);
        tutorialScreenGroup.SetActive(false);
    }

    public void AddFishGroupFinish()
    {
        addFishGroup.SetActive(false);
        addFishBackButton.SetActive(false);

        addFishPopUp1.SetActive(false);
        addFishPopUp2.SetActive(false);
        addFishPopUp3.SetActive(false);
        addFishPopUp4.SetActive(false);
        addFishPopUp5.SetActive(false);

        finfillmentScreenGroup.SetActive(true);
        finfillmentScreenText.SetActive(true);
        finfillmentScreenTime.SetActive(true);
        finfillmentAddButton.SetActive(true);
    }
    public void AddFishGroupFinishTutorial()
    {
        addFishGroupT.SetActive(false);

        addFishPopUp1T.SetActive(false);
        addFishPopUp2T.SetActive(false);
        addFishPopUp3T.SetActive(false);
        addFishPopUp4T.SetActive(false);
        addFishPopUp5T.SetActive(false);

        finfillmentScreenGroup.SetActive(true);
        finfillmentScreenText.SetActive(true);
        finfillmentScreenTime.SetActive(true);
        finfillmentAddButton.SetActive(true);
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

        infoGroupText.text = "Welcome to Finfillment! Assign your fish an Achievement style to begin.";
    }

    public void InfoPopUpTutorial2()
    {
        infoGroup.SetActive(true);
        infoGroupText.enabled = true;
        infoGroupX.SetActive(true);

        infoGroupText.text = "Great job! Next, give your fish a name.";
    }

    public void InfoPopUpTutorial3()
    {
        infoGroup.SetActive(true);
        infoGroupText.enabled = true;
        infoGroupX.SetActive(true);

        infoGroupText.text = "Super! Next, give it a goal to work on.";
    }

    public void InfoPopUpTutorial4()
    {
        infoGroup.SetActive(true);
        infoGroupText.enabled = true;
        infoGroupX.SetActive(true);

        infoGroupText.text = "Awesome! Next, give it a goal type (daily).";
    }

    public void InfoPopUpTutorial5()
    {
        infoGroup.SetActive(true);
        infoGroupText.enabled = true;
        infoGroupX.SetActive(true);

        infoGroupText.text = "Fantastic! Finally, assign it a fish type.";
    }

    public void InfoPopUpTutorial6()
    {
        infoGroup.SetActive(true);
        infoGroupText.enabled = true;
        infoGroupX.SetActive(true);

        infoGroupText.text = "Congratulations! You have made your first fish.";
    }

    public void InfoPopUpTutorialQuestionMark()
    {
        infoGroup.SetActive(true);
        infoGroupText.enabled = true;
        infoGroupX.SetActive(true);

        infoGroupText.text = "You are creating a fish that will represent a goal. Daily tracking is encouraged to build habits. Click around for next steps.";
    }
    public void InfoPopUpTutorialClose()
    {
        infoGroup.SetActive(false);
        infoGroupText.enabled = false;
        infoGroupX.SetActive(false);

        infoGroupText.text = "";
    }
}
