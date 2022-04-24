using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Animator animator;

    private Vector3 direction;
    public Vector2 lastDirection;

    public bool ismoving;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        direction = new Vector3(horizontal, vertical);

        ismoving = horizontal != 0 || vertical != 0;

        AnimateMovement(direction);
        if (ismoving)
        {
            lastDirection = new Vector2(horizontal,vertical).normalized;
            animator.SetFloat("lasthorizontal", horizontal);
            animator.SetFloat("lastvertical", vertical);
        }

        
    }

    private void FixedUpdate()
    {
        //move the player
        transform.position += direction * speed * Time.deltaTime;
    }

    void AnimateMovement(Vector3 direction)
    {
        if (animator != null)
        {
            animator.SetBool("IsMoving", ismoving);
            if (ismoving)
            {           
                animator.SetFloat("horizontal", direction.x);
                animator.SetFloat("vertical", direction.y);
            }
        }
        
    }
}
