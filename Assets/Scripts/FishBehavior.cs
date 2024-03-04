using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.CompilerServices;
using System.Net.NetworkInformation;

//This script features basic fish behavior and allows them to be selected. I will be adding the ability to pull up specific fish information w/ a fish class

public class FishBehavior : MonoBehaviour
{
    private float speed = 5f; // how fast it swim
    private float changeDirectionInterval = 10f; // time before change direction
    private Vector2 swimDirection; // current direction
    private Rigidbody2D rb; // fish's rigidbody which allows me to add movement
    [SerializeField] public static int totalFishCount;
    //________________________________________________________________________________________________
    private FishProperties fishProperties; // reference to the properties the fish has (basic Fish, meaning the ability to access the enum and what it means)
    private GameObject fishPrefabHolder; // prefab holder before we can assign it to the game object for instantiation
    private string fishStringAchievementHolder;
    private FishProperties properties; // reference to the other properties fish, assigning a name, goal, properties (the new fish, Goldfish, Angelfish, Betafish, etc.)
    [SerializeField] private GameObject goldfish; //prefab holders vvvvv
    [SerializeField] private GameObject betafish;
    [SerializeField] private GameObject angelfish;
    //________________________________________________________________________________________________
    private SceneSystem sceneFunctions;

    void Start()
    {
        sceneFunctions = FindObjectOfType<SceneSystem>();
        totalFishCount = 0;

        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("ChangeDirection", 0f, changeDirectionInterval); // basically a method that constantly runs, i guess this could go into update
    }

    void FixedUpdate()
    {
        rb.velocity = swimDirection * speed; // gives it movement

        Vector3 minBounds = new Vector3(20, 20, 1);
        Vector3 maxBounds = new Vector3(230, 420, 1);
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minBounds.x, maxBounds.x); // bounds on the mini makeshift screen :)
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minBounds.y, maxBounds.y);
        transform.position = clampedPosition;

        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
            
            if (hitCollider != null)
            {
                properties = hitCollider.GetComponent<FishProperties>(); // Get the FishProperties component

                if (properties != null) // Check if FishProperties component exists
                {
                    Debug.Log("Fish Clicked!"); 
                    Debug.Log("Hi, I'm " + properties.Name);
                    Debug.Log("My goal is: " + properties.Goal);
                    Debug.Log("My Achievement Type is: " + properties.AchievementsType);

                    SceneSystem scene = FindObjectOfType<SceneSystem>();
                    scene.PopUpFish(properties);
                    SetButtonListener(scene.trackButton, properties);
                }
                else
                {
                    Debug.Log("No FishProperties component found on clicked object.");
                }
            }
            else
            {
                Debug.Log("No collider hit.");
            }
        }
    }

    public void CreationOfAchievement(int input)
    {
        fishStringAchievementHolder = FishAchievementAssignment(input);
        Debug.Log(fishStringAchievementHolder);
    }

    public void CreationOfFish(int input)
    {
        fishPrefabHolder = FishVariableAssignment(input);

        SceneSystem scene = FindObjectOfType<SceneSystem>();
        scene.tutorialAddButton.SetActive(false);

        Vector3 minBounds = new Vector3(20, 20, 1);
        Vector3 maxBounds = new Vector3(230, 420, 1);
        Vector3 randomPosition = new Vector3(UnityEngine.Random.Range(minBounds.x, maxBounds.x), UnityEngine.Random.Range(minBounds.y, maxBounds.y), UnityEngine.Random.Range(minBounds.z, maxBounds.z));

        string parentFishHierarchy = "parentfish";
        GameObject parent = GameObject.FindWithTag(parentFishHierarchy);

        GameObject instantiatedFish = Instantiate(fishPrefabHolder, randomPosition, Quaternion.identity, parent.transform);
        instantiatedFish.SetActive(false);

        properties = instantiatedFish.GetComponent<FishProperties>();
        if (properties == null)
        {
            properties = instantiatedFish.AddComponent<FishProperties>(); //add FishProperties if not found
        }

        FishProperties.FishType InstanceFishType = (FishProperties.FishType)input;

        switch(InstanceFishType)
        {
            case FishProperties.FishType.Betafish:
                properties.Name = InputBehavior.namesList[totalFishCount]; //this will absolutely not work unless "Fish"'s total fish count is 0. idk why it keeps incrementing on its own
                properties.Goal = InputBehavior.goalsList[totalFishCount];
                properties.StreakTracked = 0;
                properties.TotalDaysTracked = 0;
                properties.FishPrefab = fishPrefabHolder;
                properties.Tracked = false;
                properties.AchievementsType = fishStringAchievementHolder;
                totalFishCount++;
            break;
            case FishProperties.FishType.Angelfish:
                properties.Name = InputBehavior.namesList[totalFishCount];
                properties.Goal = InputBehavior.goalsList[totalFishCount];
                properties.StreakTracked = 0;
                properties.TotalDaysTracked = 0;
                properties.FishPrefab = fishPrefabHolder;
                properties.Tracked = false;
                properties.AchievementsType = fishStringAchievementHolder;
                totalFishCount++;
            break;
            case FishProperties.FishType.Goldfish:
                properties.Name = InputBehavior.namesList[totalFishCount];
                properties.Goal = InputBehavior.goalsList[totalFishCount];
                properties.StreakTracked = 0;
                properties.TotalDaysTracked = 0;
                properties.FishPrefab = fishPrefabHolder;
                properties.Tracked = false;
                properties.AchievementsType = fishStringAchievementHolder;
                totalFishCount++;
            break;
        }
    }

    public GameObject FishVariableAssignment(int input)
    {
        FishProperties.FishType InstanceFishType = (FishProperties.FishType)input;

        if (fishProperties == null)
        {
            fishProperties = gameObject.GetComponent<FishProperties>();
            if (fishProperties == null)
            {
                fishProperties = gameObject.AddComponent<FishProperties>();
            }
        }

        switch(InstanceFishType)
        {
            case FishProperties.FishType.Betafish:
                fishProperties.FishPrefab = betafish;
            break;
            case FishProperties.FishType.Angelfish:
                fishProperties.FishPrefab = angelfish;
            break;
            case FishProperties.FishType.Goldfish:
                fishProperties.FishPrefab = goldfish;
            break;
        }
        Debug.Log("properties added " + fishProperties.FishPrefab);
        return fishProperties.FishPrefab;
    }

    public string FishAchievementAssignment(int input)
    {
        FishProperties.AchievementCategory InstanceFish = (FishProperties.AchievementCategory)input;

        if (fishProperties == null)
        {
            fishProperties = gameObject.GetComponent<FishProperties>();
            if (fishProperties == null)
            {
                fishProperties = gameObject.AddComponent<FishProperties>();
            }
        }

        switch(InstanceFish)
        {
            case FishProperties.AchievementCategory.Mental:
                fishProperties.AchievementsType = "Mental";
            break;
            case FishProperties.AchievementCategory.Physical:
                fishProperties.AchievementsType = "Physical";
            break;
            case FishProperties.AchievementCategory.Productivity:
                fishProperties.AchievementsType = "Productivity";
            break;
        }
        Debug.Log("Achievement type added " + fishProperties.AchievementsType);
        return fishProperties.AchievementsType;
    }

    private void ChangeDirection()
    {
        swimDirection = UnityEngine.Random.insideUnitCircle.normalized; // generate random direction

        if (swimDirection.x > 0) // figure out if the fish needs to be flipped
        {
            transform.localScale = new Vector3(-20, 20, 20);
        }  
        else
        {
            transform.localScale = new Vector3(20, 20, 20);
        }   
    }

    public void IfTrackingButtonPressed(FishProperties fishProperties)
    {
        DateTime currentTime = DateTime.Now;
        TimeSpan timeSinceLastPress = currentTime - fishProperties.lastPressTime;

        if (timeSinceLastPress.TotalHours >= 24 && !fishProperties.Tracked)
        {
            Debug.Log("Fish Tracked: " + fishProperties.Name);
            fishProperties.Tracked = true;
            fishProperties.TotalDaysTracked =+ 1;
            fishProperties.StreakTracked =+ 1;
            fishProperties.lastPressTime = currentTime;
            sceneFunctions.money += 200;

            if(sceneFunctions.timeHolder.dayOfWeekInt >= 0 && sceneFunctions.timeHolder.dayOfWeekInt < sceneFunctions.parentObjectPopUp.transform.childCount)
            {
                GameObject child = sceneFunctions.parentObjectPopUp.transform.GetChild(sceneFunctions.timeHolder.dayOfWeekInt).gameObject;

                SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.color = Color.black;
                }
            }
        }
        else if(timeSinceLastPress.TotalHours < 24 || fishProperties.Tracked == true)
        {
            Debug.Log("Not enough time has passed, try again in " + (24 - timeSinceLastPress.TotalHours));
        }
    }

    public void SetButtonListener(Button button, FishProperties fishProp)
    { 
        button.onClick.AddListener(() => IfTrackingButtonPressed(fishProp));
    }
}
