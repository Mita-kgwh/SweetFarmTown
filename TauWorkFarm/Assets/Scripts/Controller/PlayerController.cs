using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] ParticleSystem dust;
    [SerializeField] GameObject dustObject;
    [SerializeField] float speed;
    [SerializeField] float runspeed;
    public Animator animator;

    public Vector2 lastDirection;
    private Vector2 motionVector;

    public bool ismoving;
    public bool running;

    public Joystick joystick;

    private void Awake()
    {
        if (rigidbody == null) { rigidbody = GetComponent<Rigidbody2D>(); }
        if (animator == null) { animator = GetComponent<Animator>(); }
    }

    void Start()
    {
        if (motionVector == null) { motionVector = new Vector2(); }
        dustObject.SetActive(false);
        lastDirection += Vector2.down;
        //joystick.gameObject.SetActive(false);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            running = true;
            if (ismoving)
            {
                //dust.Play();
                dustObject.SetActive(true);
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            running = false;
            dustObject.SetActive(false);
        }

        //float horizontal = Input.GetAxisRaw("Horizontal");
        //float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = 0; 
        float vertical = 0;

        if (joystick.Horizontal >= .2f)
        {
            horizontal = 1;
        }
        else if (joystick.Horizontal <= -.2f)
        {
            horizontal = -1;
        }
        //else
        //{
        //    horizontal = 0;
        //}

        if (joystick.Vertical >= .2f)
        {
            vertical = 1;
        }
        else if (joystick.Vertical <= -.2f)
        {
            vertical = -1;
        }
        //else
        //{
        //    vertical = 0;
        //}

        if (Mathf.Abs(joystick.Horizontal) > Mathf.Abs(joystick.Vertical))
        {
            vertical = 0;
        }
        else
        {
            horizontal = 0;
        }


        motionVector.x = horizontal;
        motionVector.y = vertical;


        ismoving = horizontal != 0 || vertical != 0;

        AnimateMovement(motionVector);
        if (ismoving)
        {
            //timer -= Time.deltaTime;
            lastDirection = new Vector2(horizontal,vertical).normalized;
            animator.SetFloat("lasthorizontal", horizontal);
            animator.SetFloat("lastvertical", vertical);
            //if (timer <= 0)
            //{
            //    running = true;
            //}
        }
        //else
        //{
        //    timer = COUNT_DOWN_TO_RUN;
        //    running = false;
        //}

        
    }

    private void FixedUpdate()
    {
        //move the player
        Move();
        //transform.position += direction * speed * Time.deltaTime;
    }

    private void Move()
    {
        rigidbody.velocity = motionVector * (running == true? runspeed : speed);
    }

    void AnimateMovement(Vector2 motionVector)
    {
        if (animator != null)
        {
            animator.SetBool("IsMoving", ismoving);
            if (running)
            {
                //Debug.Log("im running");
            }
            if (ismoving)
            {
                //Debug.Log("im movining");
            }
            if (ismoving && running) dust.Play();
            if (ismoving)
            {           
                animator.SetFloat("horizontal", motionVector.x);
                animator.SetFloat("vertical", motionVector.y);
            }
        }
        
    }

    private void OnDisable()
    {
        rigidbody.velocity = Vector2.zero;
        motionVector = Vector2.zero;
    }
}
