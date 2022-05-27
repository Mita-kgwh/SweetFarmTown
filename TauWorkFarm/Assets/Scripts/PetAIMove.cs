using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PetAIMove : MonoBehaviour
{
    Rigidbody2D rigidbody;
    public float speed;
    public Animator animator;
    public Animator shadow;

    public Vector2 lastDirection;
    [SerializeField] public Vector2 motionVector;
    public bool ismoving;

    [SerializeField] private float timer = 0f;
    [SerializeField] int eatAmount;
    public float waitingTimeMax;
    public float movingTimeMax;
    float waitingTime;
    float movingTime;


    public bool hungry;
    [SerializeField] bool grabed;
    //Vector3Int troughPosition;
    FoodTrough foodTrough;
    [SerializeField] float sizeOfFinding;
    [SerializeField] float eatDistance = 1f;

    PetManager manager;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        ismoving = false;
        motionVector = new Vector2(0, 0);
        manager = GetComponent<PetManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (grabed)
        {
            return;
        }
        if (foodTrough == null)
        {
            FindFeedTrough();
        }

        if (!hungry)
        {
            timer += Time.deltaTime;
            if (!ismoving && timer >= waitingTime)
            {
                // Time to go my animal
                int move = Random.Range(0, 7);
                //Debug.Log(move);
                switch (move)
                {
                    case 0:
                        motionVector = Vector2.up;
                        break;
                    case 1:
                        motionVector = Vector2.right;
                        break;
                    case 2:
                        motionVector = Vector2.down;
                        break;
                    case 3:
                        motionVector = Vector2.left;
                        break;
                    case 4:
                        motionVector = Vector2.up;
                        break;
                    case 5:
                        motionVector = Vector2.right;
                        break;
                    case 6:
                        motionVector = Vector2.down;
                        break;
                    case 7:
                        motionVector = Vector2.left;
                        break;
                }
            }
        }
        else
        {
            FindFood();
            EatFood();
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

    internal void Grabed(bool value)
    {
        grabed = value;
        rigidbody.velocity = Vector2.zero;
    }

    public void FindFood()
    {
        if (foodTrough == null)
        {
            Freeze();
            return;
        }
        if (!foodTrough.CheckFood(eatAmount))
        {
            Freeze();
            return;
        }
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        Vector3 troughpos = foodTrough.transform.position;
        motionVector = (troughpos - transform.position).normalized;
        ismoving = true;
    }

    private void EatFood()
    {
        if (foodTrough == null)
        {
            return;
        }
        Vector2 thispos = transform.position;
        Vector2 thatpos = foodTrough.transform.position;
        bool canEat = Vector2.Distance(thatpos, thispos) < eatDistance;
        if (canEat)
        {
            foodTrough.EatFood(eatAmount);
            hungry = false;
            manager.Eaten();
            gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }

    public void FindFeedTrough()
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

    private void FixedUpdate()
    {
        if (grabed) { return; }
        Moving();
        if (hungry) { return; }

        if (!ismoving && timer >= waitingTime)
        {
            timer = 0;
            ismoving = true;
            waitingTime = Random.Range(1, waitingTimeMax);
            //Debug.Log("moving");
            // count time we start moving now
            return;
        }
        if (ismoving && timer >= movingTime)
        {
            timer = 0;
            ismoving = false;
            movingTime = Random.Range(1, movingTimeMax);
            motionVector = new Vector2(0, 0);
            //Debug.Log("not moving");
        }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("enter");
        motionVector = new Vector2(-lastDirection.x, -lastDirection.y);
        //if (collision.transform.CompareTag("Feeding"))
        //{
        //    FoodTrough foodTrough = collision.transform.GetComponent<FoodTrough>();
        //    if (foodTrough.CheckFood(eatAmount))
        //    {
        //        foodTrough.EatFood(eatAmount);
        //        hungry = false;  
        //        manager.Eaten();
        //    }
        //    else
        //    {
        //        Freeze();
        //    }        
        //    return;
        //}
    }

    public void Freeze()
    {
        motionVector = Vector2.zero;
        rigidbody.velocity = motionVector;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sizeOfFinding);
    }
}
