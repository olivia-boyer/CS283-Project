using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotionController : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)
            || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            animator.SetBool("moving", true);
        } else
        {
            animator.SetBool("moving", false);
        }
        
        if (gameObject.GetComponent<CharacterController>().isGrounded)
        {
            animator.SetBool("jumping", false);
        } else
        {
            animator.SetBool("jumping", true);
        }
       // Debug.Log(animator.GetBool("jumping"));
    }
}
