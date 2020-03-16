using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_Manager : MonoBehaviour
{

   Animator animator;
   public float v; 
   public float sprint;
   //int jumpHash = Animator.StringToHash("Jump");
   //int runStateHash = Animator.StringToHash("Base Lauer.Run");


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        v = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            sprint = 1;
        }
        else
        {
            sprint = 0;
        }

        if(Input.GetKey(KeyCode.Space))
        {
           animator.SetTrigger("Jump");
        }

    }

    void FixedUpdate()
    {
        animator.SetFloat("Speed", v);
        animator.SetFloat("Sprint", sprint);
    }

}
