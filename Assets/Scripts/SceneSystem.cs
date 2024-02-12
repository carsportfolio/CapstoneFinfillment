using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script handles the "scenes" the viewer can interact with. Hides and shows a lot of the interactivity based on conditions.
public class SceneSystem : MonoBehaviour
{
    [SerializeField] private GameObject finfillmentScreenGroup;
    [SerializeField] private GameObject finfillmentScreenText;
    [SerializeField] private GameObject finfillmentScreenTime;
    [SerializeField] private GameObject tutorialScreenGroup;
    [SerializeField] public GameObject tutorialAddButton;
    [SerializeField] private GameObject homeScreenGroup;
    [SerializeField] private GameObject homeScreenStartButton;
    [SerializeField] private GameObject homeScreenReturningButton;
    [SerializeField] private GameObject testScreenGroup;
    [SerializeField] private GameObject popUpGroup;
    [SerializeField] public GameObject popUpTrackingButton;
    [SerializeField] private GameObject popUpAddButton;
    [SerializeField] private GameObject popUpTrueAll;

    [SerializeField] private bool doesGameExist; // once you press the continue button, this var is true as in now there is a returning spot


    private FishBehavior fish; 
    private TimeSystem timeHolder;
    [SerializeField] private GameObject parentObjectPopUp; 
    private GameObject[] childrenArrayPopUp;  
    public int money;

    void Start()
    {
        homeScreenGroup.SetActive(true);
        homeScreenReturningButton.SetActive(false);

        //finfillmentScreenGroup.SetActive(false);
        finfillmentScreenText.SetActive(false);
        finfillmentScreenTime.SetActive(false);

        tutorialScreenGroup.SetActive(false);
        tutorialAddButton.SetActive(false);
        testScreenGroup.SetActive(false);

        popUpGroup.SetActive(false);
        popUpTrackingButton.SetActive(false);
        popUpAddButton.SetActive(false);
        popUpTrueAll.SetActive(false);
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

    public void PopUpFish()
    {
        popUpGroup.SetActive(true);
        popUpTrackingButton.SetActive(true);

        for (int i = 0; i < parentObjectPopUp.transform.childCount; i++)
        {
            GameObject child = parentObjectPopUp.transform.GetChild(i).gameObject;

            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.white; 
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

    public void IfTrackingButtonPressed()
    {
        popUpTrackingButton.SetActive(false);
        money = money + 200;

        if(timeHolder.dayOfWeekInt >= 0 && timeHolder.dayOfWeekInt < parentObjectPopUp.transform.childCount)
        {
            GameObject child = parentObjectPopUp.transform.GetChild(timeHolder.dayOfWeekInt).gameObject;

            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.black; // set color to pink
            }
        }
    }

    public void SetActiveTrueAll()
    {
        finfillmentScreenGroup.SetActive(true);
        foreach (Transform child in finfillmentScreenGroup.transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
