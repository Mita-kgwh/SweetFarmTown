using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidbody;
    public float speed;
    public Animator animator;

    //private Vector3 direction;
    public Vector2 lastDirection;
    private Vector2 motionVector;

    public bool ismoving;

    private void Awake()
    {
        if (rigidbody == null) { rigidbody = GetComponent<Rigidbody2D>(); }
        if (animator == null) { animator = GetComponent<Animator>(); }
    }

    void Start()
    {
        if (motionVector == null) { motionVector = new Vector2(); }
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");


        motionVector.x = horizontal;
        motionVector.y = vertical;


        ismoving = horizontal != 0 || vertical != 0;

        AnimateMovement(motionVector);
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
        Move();
        //transform.position += direction * speed * Time.deltaTime;
    }

    private void Move()
    {
        rigidbody.velocity = motionVector * speed;
    }

    void AnimateMovement(Vector2 motionVector)
    {
        if (animator != null)
        {
            animator.SetBool("IsMoving", ismoving);
            if (ismoving)
            {           
                animator.SetFloat("horizontal", motionVector.x);
                animator.SetFloat("vertical", motionVector.y);
            }
        }
        
    }
}
