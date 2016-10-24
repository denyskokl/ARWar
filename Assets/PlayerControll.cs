using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PlayerControll : MonoBehaviour
{
    private float speed = 150f;
    private float rotateSpeed = 3f;

    private CharacterController controller;
    private Animator animator;
    private Vector2 startPos;
    private Vector2 direction;
   
    private float speedRotation = 2f;
    private float maxLength = 4f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
#if UNITY_STANDALONE
        InputKeyCatch();
#elif UNITY_ANDROID
        TouchCatch();
#endif
    }

    private void InputKeyCatch()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float y = Input.GetAxis("Vertical") * Time.deltaTime * rotateSpeed;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, y);
            
        if (Input.GetAxis("Vertical") != 0)
        {
            animator.SetBool("IsRun", true);
        }
        else
        {
            animator.SetBool("IsRun", false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shot();
        }
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
