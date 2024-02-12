using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script features basic fish behavior and allows them to be selected. I will be adding the ability to pull up specific fish information w/ a fish class

public class FishBehavior : MonoBehaviour
{
    [SerializeField] private GameObject goldfish;
    [SerializeField] private GameObject betafish;
    [SerializeField] private GameObject angelfish;
    private GameObject fishPrefab;
    private GameObject newFish;
    private string parentFishHierarchy;
    private float speed = 5f; // how fast he swim
    private float changeDirectionInterval = 10f; // time before change direction
    private Vector2 swimDirection; // current direction
    private Vector3 minBounds;
    private Vector3 maxBounds;
    private Rigidbody2D rb; // fish's rigidbody which allows me to add movement
    //[SerializeField] private int totalFishCount = 0;
    //________________________________________________________________________________________________
    public FishProperties fishProperties;
    private SceneSystem scene;
    private string fishName;
    private string goal;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("ChangeDirection", 0f, changeDirectionInterval); // basically a method that constantly runs
    }

    void FixedUpdate()
    {
        rb.velocity = swimDirection * speed; // gives it movement

        if (Input.GetMouseButtonDown(0)) // if the screen is tapped
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
            
            if (hitCollider != null) // AND the fish is selected
            {
                Debug.Log("Hit Collider: " + hitCollider.name); // hit fish name

                FishProperties clickedFishProperties = hitCollider.GetComponent<FishProperties>();
                if (clickedFishProperties != null && hitCollider.gameObject == gameObject)
                {
                    Debug.Log("Fish Clicked!"); 
                    Debug.Log("Hi, im " + clickedFishProperties.Name);
                    Debug.Log("My goal is: " + clickedFishProperties.Goal);

                    scene = FindObjectOfType<SceneSystem>();
                    if (scene != null)
                    {
                        scene.PopUpFish();
                    }
                }
            }
            else
            {
                Debug.Log("No collider hit.");
            }
        }
    }

    public void CreationOfFish(int fishType)
    {
        //totalFishCount++;
        scene = FindObjectOfType<SceneSystem>();
        scene.tutorialAddButton.SetActive(false);

        minBounds = new Vector3(20, 20, 1);
        maxBounds = new Vector3(230, 420, 1);
        Vector3 randomPosition = new Vector3( Random.Range(minBounds.x, maxBounds.x), Random.Range(minBounds.y, maxBounds.y), Random.Range(minBounds.z, maxBounds.z) );

        parentFishHierarchy = "parentfish";
        GameObject parent = GameObject.FindWithTag(parentFishHierarchy);

        switch(fishType)
        {
        case 0:
            fishPrefab = goldfish;
            fishName = "goldfishy";
            goal = "tutorial";
            break;
        case 1:
            fishPrefab = betafish;
            fishName = "betafishy";
            goal = "tutorial";
            break;
        case 2:
            fishPrefab = angelfish;
            fishName = "angelfishy";
            goal = "tutorial";
            break;
        case 3:
            fishPrefab = goldfish;
            fishName = "fishy";
            goal = "tutorial";
            break;
        }

        Debug.Log("added " + fishPrefab);
        newFish = Instantiate(fishPrefab, randomPosition, Quaternion.identity, parent.transform);
        newFish.SetActive(false);

        fishProperties = newFish.AddComponent<FishProperties>();

        fishProperties.Name = fishName;
        fishProperties.Goal = goal;
        fishProperties.DailyStreak = 0;
        fishProperties.TotalStreak = 0;
    }

    private void ChangeDirection()
    {
        swimDirection = Random.insideUnitCircle.normalized; // generate random direction

        if (swimDirection.x > 0) // figure out if the fish needs to be flipped
        {
            transform.localScale = new Vector3(-20, 20, 20);
        }  
        else
        {
            transform.localScale = new Vector3(20, 20, 20);
        }   
    }
}
