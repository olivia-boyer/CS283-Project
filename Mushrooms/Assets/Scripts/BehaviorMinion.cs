using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BTAI;
using System;


public class BehaviorMinion : MonoBehaviour
{
    private Root rootNode = BT.Root();
    public Transform target;
    public float atkRad; //attack radius
    public float viewRad;
    public int homeCutoff;
    private bool seen;
    private Animator anim;
    private Vector3 area = new(-80, 12, 560);
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Triggered());
        anim = GetComponent<Animator>();
        seen = false;
        BTNode flee = BT.RunCoroutine(Flee);
        BTNode attack = BT.RunCoroutine(Attack);
        BTNode follow = BT.RunCoroutine(Follow);
        Selector selector = BT.Selector();
        ConditionalBranch view = BT.If(() => { return seen; });
        selector.OpenBranch(flee);
        selector.OpenBranch(attack);
        selector.OpenBranch(follow);
        view.OpenBranch(selector);
        rootNode.OpenBranch(view);

    }

    // Update is called once per frame
    void Update()
    {
        rootNode.Tick();
    }

    
    IEnumerator<BTState> Attack()
    {
        
        if (Vector3.Distance(this.transform.position,target.position) <= atkRad)
        {
            agent.ResetPath();
            transform.LookAt(target, Vector3.up);
            anim.SetBool("attacking", true);
            Debug.Log("Attacking");
            Debug.Log(Vector3.Distance(this.transform.position, target.position).ToString());
            yield return BTState.Success;
        }
        
        anim.SetBool("attacking", false);
        yield return BTState.Failure;
        //if in range play attack animation
    }

    IEnumerator<BTState> Flee()  
    {

        if (target.position.z < homeCutoff)
        {
            Debug.Log("Fleeing");
            Vector3 goTo;
            if (RandomPoint(area, 100, out goTo))
            {  Debug.Log(goTo.ToString());
            agent.SetDestination(goTo);
            }
            else
            {
                goTo = area;
                agent.SetDestination(goTo);
            }
            seen = false;
            StartCoroutine(Triggered());
            StartCoroutine(CheckDistance(goTo));
            
            yield return BTState.Success;

        }
        yield return BTState.Failure;
        //calculate vector direction to player, move in opposite direction
    }

    IEnumerator<BTState> Follow()
    {
        //move to player transform
        
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if (target.position.z < homeCutoff)
        { 
            yield return BTState.Failure;
        }
        Debug.Log("Following");
        if (Vector3.Distance(this.transform.position, target.position) > atkRad)
        {
            agent.SetDestination(target.position);
            anim.SetBool("moving", true);
            yield return BTState.Success;
        } 
        
            agent.ResetPath();
            anim.SetBool("moving", false);
            yield return BTState.Failure;
        
        /*
         * while (Vector3.Distance(this.transform.position,target.position) >= atkRad)
        {
            agent.SetDestination(target.position);
            anim.SetBool("moving", true);
            yield return BTState.Continue;
        }
        agent.ResetPath();
        anim.SetBool("moving", false);
        */
    }

    IEnumerator Triggered()
    {
        while(!seen)
        {
           //anim.SetBool("moving", false);
            if (Vector3.Distance(this.transform.position, target.position) <= viewRad)
            {
                seen = true;
            }
            yield return null;
        }
       
    }

    private bool getSeen()
    {
        Debug.Log(seen.ToString());
        return seen;
    }

    //randompoint method from unity documentation
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
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

    IEnumerator CheckDistance(Vector3 point)
    {
        while(Vector3.Distance(transform.position, point) >= 5)
            {
                yield return null;
            }
        anim.SetBool("moving", false);
        agent.ResetPath();
        yield return null;
    }
}
