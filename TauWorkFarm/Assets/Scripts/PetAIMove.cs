using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PetAIMove : MonoBehaviour
{
    Rigidbody2D rigidbody;
    public float speed;
    public Animator animator;

    public Vector2 lastDirection;
    [SerializeField] private Vector2 motionVector;

    public bool ismoving;

    private float timer = 0f;
    public float standingTime;
    public float movingTime;
    //public float randomRange;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        ismoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (!ismoving && timer >= standingTime)
        {
            // Time to go my animal
            int horizontal = UnityEngine.Random.Range(-1, 1);
            int vertical = UnityEngine.Random.Range(-1, 1);

            motionVector = new Vector2(
                horizontal,
                vertical
                );
        }
        AnimateMovement(motionVector);
        if (ismoving)
        {
            lastDirection = new Vector2(motionVector.x, motionVector.y).normalized;
            animator.SetFloat("lasthorizontal", motionVector.x);
            animator.SetFloat("lastvertical", motionVector.y);
        }
    }


    private void FixedUpdate()
    {
        //move the player
        Moving();
        if (!ismoving && timer >= standingTime)
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

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ismoving)
        {
            motionVector = new Vector2(-lastDirection.x, -lastDirection.y);
        }
    }
}
