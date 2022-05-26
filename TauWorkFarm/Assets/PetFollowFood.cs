using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetFollowFood : MonoBehaviour
{
    Rigidbody2D rigidbody;
    public float speed;
    public Animator animator;
    public Animator shadow;

    public Vector2 lastDirection;
    [SerializeField] private Vector2 motionVector;

    public bool ismoving;

    [SerializeField] int eatAmount;
    [SerializeField] float sizeOfFinding;
    [SerializeField] float eatDistance;
    FoodTrough foodTrough;

    [SerializeField] ParticleSystem heartEmotion;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        ismoving = false;
        motionVector = new Vector2(0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (foodTrough != null)
        {
            if (foodTrough.CheckFood(eatAmount))
            {
                Vector2 thispos = transform.position;
                Vector2 thatpos = foodTrough.transform.position;
                bool canEat = Vector2.Distance(thatpos, thispos) < eatDistance;
                if (canEat)
                {
                    foodTrough.EatFood(eatAmount);
                    heartEmotion.Play();
                    Debug.Log("Eat");
                    motionVector = Vector2.zero;
                    foodTrough = null;
                    ismoving = false;
                }
                else
                {
                    motionVector = (foodTrough.transform.position - transform.position).normalized;
                    ismoving = true;
                }
            }
            
        }
        else
        {
            FindFeedTrough();
        }

        AnimateMovement(motionVector);
        if (ismoving)
        {
            lastDirection = new Vector2(motionVector.x, motionVector.y).normalized;
            animator.SetFloat("lasthorizontal", motionVector.x);
            animator.SetFloat("lastvertical", motionVector.y);
            if (shadow != null)
            {
                shadow.SetFloat("lasthorizontal", motionVector.x);
                shadow.SetFloat("lastvertical", motionVector.y);
            }

        }
    }

    private void FixedUpdate()
    {
        Moving();
    }
    private void Moving()
    {
        rigidbody.velocity = motionVector * speed;
    }

    void AnimateMovement(Vector2 direction)
    {
        if (animator != null)
        {
            animator.SetBool("isMoving", ismoving);
            if (ismoving)
            {

                animator.SetFloat("horizontal", direction.x);
                animator.SetFloat("vertical", direction.y);
            }
        }
        if (shadow != null)
        {
            shadow.SetBool("isMoving", ismoving);
            if (ismoving)
            {

                shadow.SetFloat("horizontal", direction.x);
                shadow.SetFloat("vertical", direction.y);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(transform.tag + " enter trigger " + collision.tag);
        if (collision.transform.CompareTag("Feeding"))
        {
            if (foodTrough == null)
            {
                foodTrough = collision.transform.GetComponent<FoodTrough>();
            }
            Vector2 thispos = transform.position;
            Vector2 thatpos = collision.transform.position;
            Debug.Log(thispos + " " + thatpos);
            bool canEat = Vector2.Distance(thatpos, thispos) < eatDistance;
            if (foodTrough.CheckFood(eatAmount) && canEat)
            {
                foodTrough.EatFood(eatAmount);
                heartEmotion.Play();
                Debug.Log("Eat");
                motionVector = Vector2.zero;
                foodTrough = null;
                ismoving = false;
            }
            return;
        }
        //motionVector = Vector2.zero;
        //foodTrough = null;
    }

    private void FindFeedTrough()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, sizeOfFinding);
        foreach (Collider2D c in colliders)
        {
            FoodTrough hit = c.GetComponent<FoodTrough>();
            if (hit != null)
            {
                foodTrough = hit;
                return;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sizeOfFinding);
    }
}
