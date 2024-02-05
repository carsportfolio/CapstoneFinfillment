using UnityEngine;

//This script features basic fish behavior and allows them to be selected. I will be adding the ability to pull up specific fish information w/ a fish class

public class FishBehavior : MonoBehaviour
{
    public float speed = .5f; // how fast he swim
    public float changeDirectionInterval = 10f; // time before change direction
    private Vector2 swimDirection; // current direction
    private Rigidbody2D rb;

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

                if (hitCollider.gameObject == gameObject)
                {
                    Debug.Log("Fish Clicked!"); 
                    Debug.Log("Hi");
                }
            }
            else
            {
                Debug.Log("No collider hit.");
            }
        }
    }

    void ChangeDirection()
    {
        swimDirection = Random.insideUnitCircle.normalized; // generate random direction

        if (swimDirection.x > 0) // figure out if the fish needs to be flipped
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }  
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }   
    }
}
