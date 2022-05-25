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
    [SerializeField] private Vector2 motionVector;

    public bool ismoving;

    [SerializeField] private float timer = 0f;
    public float waitingTime;
    public float movingTime;

    public bool hungry;
    bool found;
    Vector3Int troughPosition;

    PetManager manager;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        ismoving = false;
        //timer = waitingTime;
        motionVector = new Vector2(0, 0);
        manager = GetComponent<PetManager>();

    }

    // Update is called once per frame
    void Update()
    {
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

    public void FindFood(Vector3Int _troughPosition)
    {
        troughPosition = _troughPosition;
        motionVector = (troughPosition - transform.position).normalized;
        ismoving = true;
    }

    private void FixedUpdate()
    {
        
        //move the player
        Moving();
        if (hungry) { return; }

        if (!ismoving && timer >= waitingTime)
        {
            timer = 0;
            ismoving = true;
            //Debug.Log("moving");
            // count time we start moving now
            return;
        }
        if (ismoving && timer >= movingTime)
        {
            timer = 0;
            ismoving = false;
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
        Debug.Log("enter");
        motionVector = new Vector2(-lastDirection.x, -lastDirection.y);
        if (collision.transform.CompareTag("Feeding"))
        {
            FoodTrough foodTrough = collision.transform.GetComponent<FoodTrough>();
            if (foodTrough.CheckFood(10))
            {
                foodTrough.EatFood(10);
                hungry = false;  
                manager.Eaten();
                //motionVector = new Vector2(-lastDirection.x, -lastDirection.y);
            }
            
            return;
        }
        //if (ismoving)
        //{
        //    motionVector = new Vector2(-lastDirection.x, -lastDirection.y);
        //}
    }
}
