using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

//This script features basic fish behavior and allows them to be selected. I will be adding the ability to pull up specific fish information w/ a fish class

public class FishBehavior : MonoBehaviour
{
    private float speed = 5f; // how fast it swim
    private float changeDirectionInterval = 10f; // time before change direction
    private Vector2 swimDirection; // current direction
    private Rigidbody2D rb; // fish's rigidbody which allows me to add movement
    [SerializeField] private int totalFishCount = 0;
    //________________________________________________________________________________________________
    private FishProperties fishProperties; // reference to the properties the fish has (basic Fish, meaning the ability to access the enum and what it means)
    [SerializeField] private GameObject fishPrefabHolder;  // prefab holder before we can assign it to the game object for instantiation, should be ON THE RED FISH
    private string fishStringAchievementHolder;
    private FishProperties properties; // reference to the other properties fish, assigning a name, goal, properties (the new fish, Goldfish, Angelfish, Betafish, etc.)
    [SerializeField] private GameObject goldfish; //prefab holders vvvvv
    [SerializeField] private GameObject betafish;
    [SerializeField] private GameObject angelfish;
    private GameObject parent;
    //________________________________________________________________________________________________
    [SerializeField] private SceneSystem sceneFunctions;
    [SerializeField] private GameObject fishListPrefabHolder; // prefab of the button to instantiate
    [SerializeField] private GameObject fishGoalsPrefabHolder; // prefab of group of buttons and image to instantiate
    [SerializeField] private float fishSpacing = 30f; // spacing between buttons
    public string[] tagsToSearch = {"fishnameTAG", "fishgoalTAG"};
    private string filePath;

    [System.Serializable]
    private class SerializableList<T>
    {
        public List<T> list;

        public SerializableList(List<T> list)
        {
            this.list = list;
        }
    }

    void OnApplicationQuit()
    {
        SaveData();
    }

    void Start()
    {
        filePath = Application.persistentDataPath + "/FishData.json";

        if (File.Exists(filePath))
        {
            Debug.Log("FishData.json found. Loading data.");
            LoadData();
            //PopulateButtons(); where the fish spawn
        }

        totalFishCount = 0; 
        rb = GetComponent<Rigidbody2D>();

        if (gameObject.CompareTag("fishprefabparent"))
        {
            Debug.Log("this shouldn't move");
        }
        else
        {
            InvokeRepeating("ChangeDirection", 0f, changeDirectionInterval); // basically a method that constantly runs, i guess this could go into update
        }
    }

    void FixedUpdate()
    {
        rb.velocity = swimDirection * speed; // gives it movement

        Vector3 minBounds = new Vector3(20, 20, 1);
        Vector3 maxBounds = new Vector3(150, 300, 1);
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
        Debug.Log("Names List:");
        foreach (string name in InputBehavior.namesList)
        {
            Debug.Log(name);
        }

        Debug.Log("Goals List:");
        foreach (string goal in InputBehavior.goalsList)
        {
            Debug.Log(goal);
        }

        Debug.Log("GoalType List: ");
        foreach (string goaltypes in InputBehavior.goalTypesList)
        {
            Debug.Log(goaltypes);
        }

        fishPrefabHolder = FishVariableAssignment(input);
        Debug.Log(fishPrefabHolder);

        SceneSystem scene = FindObjectOfType<SceneSystem>();
        scene.tutorialAddButton.SetActive(false);

        Vector3 minBounds = new Vector3(20, 20, 1);
        Vector3 maxBounds = new Vector3(150, 300, 1);
        Vector3 randomPosition = new Vector3(UnityEngine.Random.Range(minBounds.x, maxBounds.x), UnityEngine.Random.Range(minBounds.y, maxBounds.y), UnityEngine.Random.Range(minBounds.z, maxBounds.z));
        Debug.Log(randomPosition);

        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("parentfish"); // this helps us find it when it becomes "hidden" or deactivated

        foreach (GameObject obj in objectsWithTag)
        {
            bool wasActive = obj.activeSelf;
            obj.SetActive(true);

            Debug.Log("parent is: " + obj.transform);
            parent = obj;

            if (!wasActive)
            {
                obj.SetActive(false);
            }
        }
        Debug.Log("parent outside of the loop is " + parent);

        GameObject instantiatedFish = Instantiate(fishPrefabHolder, randomPosition, Quaternion.identity, parent.transform); 
        instantiatedFish.SetActive(false);

        properties = instantiatedFish.GetComponent<FishProperties>();
        if (properties == null)
        {
            properties = instantiatedFish.AddComponent<FishProperties>(); //add FishProperties if not found
        }

        properties.Name = InputBehavior.namesList[totalFishCount]; 
        properties.Goal = InputBehavior.goalsList[totalFishCount]; 
        properties.GoalType = InputBehavior.goalTypesList[totalFishCount]; 
        properties.StreakTracked = 0;
        properties.TotalDaysTracked = 0;
        properties.FishPrefab = fishPrefabHolder;
        properties.Tracked = false;
        properties.AchievementsType = fishStringAchievementHolder;
        totalFishCount++; 

        InputBehavior.finishedFishList.Add(properties);
        SaveData();
        properties = null;
    }

    public void CreationOfFishJSON(FishProperties fishInput)
    {
        Vector3 minBounds = new Vector3(20, 20, 1);
        Vector3 maxBounds = new Vector3(150, 300, 1);
        Vector3 randomPosition = new Vector3(UnityEngine.Random.Range(minBounds.x, maxBounds.x), UnityEngine.Random.Range(minBounds.y, maxBounds.y), UnityEngine.Random.Range(minBounds.z, maxBounds.z));

        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("parentfish"); // this helps us find it when it becomes "hidden" or deactivated

        foreach (GameObject obj in objectsWithTag)
        {
            bool wasActive = obj.activeSelf;
            obj.SetActive(true);

            Debug.Log("parent is: " + obj.transform);
            parent = obj;

            if (!wasActive)
            {
                obj.SetActive(false);
            }
        }
        Debug.Log("parent outside of the loop is " + parent);

        GameObject instantiatedFish = Instantiate(fishInput.FishPrefab, randomPosition, Quaternion.identity, parent.transform); 
        instantiatedFish.SetActive(false);

        properties = instantiatedFish.GetComponent<FishProperties>();
        if (properties == null)
        {
            properties = instantiatedFish.AddComponent<FishProperties>(); //add FishProperties if not found
        }

        properties.Name = fishInput.Name; 
        properties.Goal = fishInput.Goal; 
        properties.GoalType = fishInput.GoalType; 
        properties.StreakTracked = fishInput.StreakTracked;
        properties.TotalDaysTracked = fishInput.TotalDaysTracked;
        properties.FishPrefab = fishInput.FishPrefab;
        properties.Tracked = fishInput.Tracked;
        properties.AchievementsType = fishInput.AchievementsType;
        totalFishCount++; 
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

                Image renderer = child.GetComponent<Image>();
                if (renderer != null)
                {
                    renderer.color = Color.black;
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

    public void PopulateFishList()
    {   
        if (sceneFunctions.fishListGroup == null) // is there a place to put the fish? yes
        {
            Debug.Log("group gameobject referencenot not set");
            return;
        }

        foreach (Transform child in sceneFunctions.fishListGroup.transform) // get rid to redo
        {
            Destroy(child.gameObject);
        }

        float totalHeight = InputBehavior.finishedFishList.Count * (fishListPrefabHolder.GetComponent<RectTransform>().sizeDelta.y + fishSpacing); // number of fish in the list
        Vector3 startPos = new Vector3(0f, totalHeight / 2f, 0f);

        sceneFunctions.fishListGroup.SetActive(true); // activate temporarily to assign buttons

        for (int i = 0; i < InputBehavior.finishedFishList.Count; i++)
        {
            FishProperties fish = InputBehavior.finishedFishList[i];
            Debug.Log(InputBehavior.finishedFishList[i].Name);

            GameObject fishListProperties = Instantiate(fishListPrefabHolder, sceneFunctions.fishListGroup.transform); //sfdasd
            fishListProperties.transform.localPosition = startPos - new Vector3(0f, i * (fishListPrefabHolder.GetComponent<RectTransform>().sizeDelta.y + fishSpacing), 0f);

            TextMeshProUGUI buttonText = fishListProperties.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = InputBehavior.finishedFishList[i].Name; //fish's name

            Button buttonImageComponent = fishListProperties.GetComponent<Button>();

            if (buttonImageComponent != null) //shouldnt be
            {
                int buttonIndex = i; //current index that has been triggered
                buttonImageComponent.onClick.AddListener(() => OnButtonClick(buttonIndex));
            }
        }

        //groupGameObject.SetActive(false);
    }

    public void PopulateFishGoals()
    {
        if (sceneFunctions.fishGoalsGroup == null) // is there a place to put the fish? yes
        {
            Debug.Log("group gameobject referencenot not set");
            return;
        }

        foreach (Transform child in sceneFunctions.fishGoalsGroup.transform) // get rid to redo
        {
            Destroy(child.gameObject);
        }

        float totalHeight = InputBehavior.finishedFishList.Count * (fishGoalsPrefabHolder.GetComponent<RectTransform>().sizeDelta.y + fishSpacing); // number of fish in the list
        Vector3 startPosi = new Vector3(0f, totalHeight / 2f, 0f);

        sceneFunctions.fishGoalsGroup.SetActive(true); // activate temporarily to assign buttons

        for (int i = 0; i < InputBehavior.finishedFishList.Count; i++)
        {
            FishProperties fish = InputBehavior.finishedFishList[i];
            Debug.Log(InputBehavior.finishedFishList[i].Name);

            GameObject fishGoalsProperties = Instantiate(fishGoalsPrefabHolder, sceneFunctions.fishGoalsGroup.transform); 
            fishGoalsProperties.transform.localPosition = startPosi - new Vector3(0f, i * (fishGoalsPrefabHolder.GetComponent<RectTransform>().sizeDelta.y + fishSpacing), 0f);

            TextMeshProUGUI[] buttonTexts = fishGoalsProperties.GetComponentsInChildren<TextMeshProUGUI>();
            for (int x = 0; x < buttonTexts.Length; x++)
            {
                if(x == 0)
                {
                    buttonTexts[x].text = InputBehavior.finishedFishList[i].Goal; 
                }
                else
                {
                    buttonTexts[x].text = InputBehavior.finishedFishList[i].Name;
                }
            } 

            Button buttonImageComponent = fishGoalsProperties.GetComponent<Button>();

            if (buttonImageComponent != null) //shouldnt be
            {
                int buttonIndex = i; //current index that has been triggered
                buttonImageComponent.onClick.AddListener(() => OnButtonClick(buttonIndex));
            }
        }
    }

    void OnButtonClick(int buttonIndex)
    {
        if (buttonIndex >= 0 && buttonIndex < InputBehavior.finishedFishList.Count)
        {
            FishProperties selectedItem = InputBehavior.finishedFishList[buttonIndex];
            sceneFunctions.FinfillmentFishProfileActivate(selectedItem, selectedItem.Name);
        }
        else
        {
            Debug.LogError("Invalid button index.");
        }
    }

    public void SaveData()
    {
        string jsonString = JsonUtility.ToJson(new SerializableList<FishProperties>(InputBehavior.finishedFishList));
        File.WriteAllText(filePath, jsonString);
    }

    public void LoadData()
    {
        string jsonString = File.ReadAllText(filePath);
        SerializableList<FishProperties> data = JsonUtility.FromJson<SerializableList<FishProperties>>(jsonString);
        InputBehavior.finishedFishList = data.list;
        Debug.Log("Data loaded from " + filePath);
    }

    void ClearFile()
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("FishData.json cleared.");
        }
    }
}
