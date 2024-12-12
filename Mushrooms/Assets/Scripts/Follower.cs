using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BTAI;
using System;

public class Follower : MonoBehaviour
{
    public Transform target;
    public int personalSpace;
    private Animator anim;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
       //StartCoroutine(Follow());
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, target.position) > personalSpace)
        {
            agent.SetDestination(target.position);
            anim.SetBool("moving", true);
           
        }
        else
        {
           
            agent.ResetPath();
            anim.SetBool("moving", false);
            
        }
       
    }

  /*  IEnumerator Follow()
    {
        while (true)
        {
           
            if (Vector3.Distance(this.transform.position, target.position) > personalSpace)
            {
                agent.SetDestination(target.position);
                anim.SetBool("moving", true);
                yield return null;
            }
            else
            {
                Debug.Log("should stop");
                agent.ResetPath();
                anim.SetBool("moving", false);
                yield return null;
            }
        }
    }*/
}
