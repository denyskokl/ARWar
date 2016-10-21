using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PlayerControll : MonoBehaviour
{
    public float speed = 3f;
    private CharacterController controller;
    private Animator animator;
    private Vector2 startPos;
    private Vector2 direction;
   
    private float rotateSpeed = 3f;
    private float speedRotation = 2f;
    private float maxLength = 4f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //#if UNITY_EDITOR
        //transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
        //Vector3 forward = transform.TransformDirection(Vector3.forward);
        //float curSpeed = speed * Input.GetAxis("Vertical");
        //controller.SimpleMove(forward * curSpeed);
        //#elif UNITY_ANDROID
        TouchCatch();
        //#endif

        //if (Input.GetMouseButtonDown(0))
        //{
        //    Debug.Log("Mouse down");
        //    Shot();
        //}
    }

   private void TouchCatch()
    {
        // Track a single touch as a direction control.
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Handle finger movements based on touch phase.
            switch (touch.phase)
            {
                // Record initial touch position.
                case TouchPhase.Began:
                    startPos = touch.position;
                    break;

                // Determine direction by comparing the current touch position with the initial one.
                case TouchPhase.Moved:
                    direction = startPos - touch.position;
                    break;

                // Report that a direction has been chosen when the finger is lifted.
                case TouchPhase.Ended:
                    if (direction.magnitude < 10f)
                    {
                        Shot();
                    }
                    else
                    {
                        animator.SetBool("IsRun", false);
                    }
                    direction = Vector2.zero;
                    break;
            }

            if (direction.magnitude > 10f)
            {
                Move(direction.normalized);
                animator.SetBool("IsRun", true);
            }
        }
    }

    private void Move(Vector2 direction)
    {
        Vector3 tmpPosition = new Vector3(direction.x * speed * Time.deltaTime, transform.position.y, direction.y * speed * Time.deltaTime);
        transform.position += tmpPosition;

        var targetRotating = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotating, Time.time * speedRotation);
    }

    private void Shot()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Shot"))
        {
            animator.SetTrigger("Shot");
        }
    }
}
