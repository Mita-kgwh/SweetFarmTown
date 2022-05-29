using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetFollowPlayer : MonoBehaviour
{
    Rigidbody2D rigidbody;
    public float speed;
    public Animator animator;
    public Animator shadow;

    public Vector2 lastDirection;
    [SerializeField] private Vector2 motionVector;

    Transform target;

    float offsetDis;

    public bool ismoving;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        ismoving = false;
        motionVector = new Vector2(0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget();

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

    private void FollowTarget()
    {
        if (target != null)
        {
            float dis = Vector3.Distance(target.position, transform.position);
            if (dis <= offsetDis) { return; }
            motionVector = (target.position - transform.position).normalized;
        }
    }

    public void SetTarget(Transform transform)
    {
        target = transform;
        offsetDis = Vector3.Distance(target.position, transform.position);
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

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(transform.position, sizeOfFinding);
    //}
}
