using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : MonoBehaviour
{
    public float range = 20.0f;
    public float speed;
    private NavMeshAgent agent;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent.SetDestination(transform.position);
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 point;
        
        if (agent.remainingDistance < 1)
        {
            animator.SetBool("isMoving", false);
            if (RandomPoint(transform.position, range, out point))
            {        
                agent.SetDestination(point);
                animator.SetBool("isMoving", true);
            }
        }
        animator.SetBool("isMoving", true);
    }

    //randompoint method from unity documentation
    bool RandomPoint(Vector3 center, float range, out Vector3 result) 
    {
        for (int i = 0; i< 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;

     }


}
