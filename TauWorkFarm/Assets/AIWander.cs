using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWander : MonoBehaviour
{
    Rigidbody2D rigidbody;
    public float speed;
    public Animator animator;
    public Animator shadow;

    private Vector2 motionVector;
    private Vector2 lastDirection;

    public float rotationspeed;

    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isRotatingUp = false;
    private bool isRotatingDown = false;
    private bool isWalking = false;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isWandering == false)
        {
            StartCoroutine(Wander());
        }
        if (isRotatingRight == true)
        {
            motionVector = new Vector2(1, 0);
        }
        if (isRotatingLeft == true)
        {
            motionVector = new Vector2(-1, 0);
        }
        if (isRotatingUp == true)
        {
            motionVector = new Vector2(0, 1);
        }
        if (isRotatingDown == true)
        {
            motionVector = new Vector2(0, -1);
        }
        AnimateMovement(motionVector);
        if (isWalking == true)
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
        if (isWalking == true)
        {
            rigidbody.velocity = motionVector * speed;
        }
    }

    IEnumerator Wander()
    {
        int rotationTime = Random.Range(1, 3);
        int rotateWait = Random.Range(1, 3);
        int rotateDirection = Random.Range(1, 4);
        int walkWait = Random.Range(1, 3);
        int walkTime = Random.Range(1, 3);
        
        isWandering = true;
        
        yield return new WaitForSeconds(walkWait);
        
        isWalking = true;
        
        yield return new WaitForSeconds(walkTime);
        
        isWalking = false;
        
        yield return new WaitForSeconds(rotateWait);

        if (rotateDirection == 1)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotationTime);
            isRotatingLeft = false;
        }

        if (rotateDirection == 2)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotationTime);
            isRotatingRight = false;
        }

        if (rotateDirection == 3)
        {
            isRotatingUp = true;
            yield return new WaitForSeconds(rotationTime);
            isRotatingUp = false;
        }

        if (rotateDirection == 4)
        {
            isRotatingDown = true;
            yield return new WaitForSeconds(rotationTime);
            isRotatingDown = false;
        }

        isWandering = false;
    }

    void AnimateMovement(Vector2 direction)
    {
        if (animator != null)
        {
            animator.SetBool("isMoving", isWalking);
            if (isWalking)
            {

                animator.SetFloat("horizontal", direction.x);
                animator.SetFloat("vertical", direction.y);
            }
        }
        if (shadow != null)
        {
            shadow.SetBool("isMoving", isWalking);
            if (isWalking)
            {
                shadow.SetFloat("horizontal", direction.x);
                shadow.SetFloat("vertical", direction.y);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isWalking)
        {
            motionVector = new Vector2(-lastDirection.x, -lastDirection.y);
        }
    }
}
