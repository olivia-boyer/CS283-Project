using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BTAI;
using System;

public class EnemyBehavior : MonoBehaviour
{
    private Root rootNode = BT.Root();
    public Transform target;
    public float atkRad;
    public float viewRad;
    private Animator anim;
    private bool activated;
    public Transform wanderOrigin;
    private NavMeshAgent agent;
    public float range;
    public Boolean patrolling;
    public bool inCombat;
    public Transform ptpOne; //patrol endpoint one
    public Transform ptpTwo; //patrol endpoint two
    private bool loc; //using boolean to choose destinations since only 2
    public GameObject combatManager;
    public GameObject combatPrefab;
 
   
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();     
        anim = GetComponent<Animator>();     
        BTNode patrol = BT.RunCoroutine(Patrol);
        BTNode attack = BT.RunCoroutine(Attack);
        BTNode follow = BT.RunCoroutine(Follow);
        BTNode wander = BT.RunCoroutine(Wander);
        Selector isPatrolling = BT.Selector();
        Selector isAtk = BT.Selector();
        Selector playerSeen = BT.Selector();
        ConditionalBranch isSeen = BT.If(() => {
            return Vector3.Distance(
            target.position, this.transform.position)
            <= viewRad;});
        ConditionalBranch unSeen = BT.If(() => {
            return Vector3.Distance(
            target.position, this.transform.position)
            > viewRad;});
        rootNode.OpenBranch(playerSeen);
        playerSeen.OpenBranch(isSeen);
        unSeen.OpenBranch(isPatrolling);
        isPatrolling.OpenBranch(wander);
        isPatrolling.OpenBranch(patrol);
        playerSeen.OpenBranch(unSeen);
        isSeen.OpenBranch(isAtk);
        isAtk.OpenBranch(attack);
        isAtk.OpenBranch(follow);
        loc = true;
        if (!patrolling)
        {
            agent.SetDestination(transform.position);
        }
        

    }


    void Update()
    {
        rootNode.Tick();
    }

    IEnumerator<BTState> Patrol()
    {
        if (patrolling)
        {
            Debug.Log("patrolling");
            if(agent.remainingDistance < 1)
            {
                loc = !loc;
            }
            
            if (loc)
            {
                agent.SetDestination(ptpTwo.position);
            } else
            {
                agent.SetDestination(ptpOne.position);
            }
            
            yield return BTState.Success;

        } else
        {
            yield return BTState.Failure;
        }
    }


    IEnumerator<BTState> Wander()
    {
        if (patrolling)
        {
            yield return BTState.Failure;
        } else
        {
            Debug.Log("wandering");
            //NavMeshAgent agent = GetComponent<NavMeshAgent>();
            Vector3 dest;
            if (agent.remainingDistance < 1.0f)
            {
                if (RandomPoint(
                   wanderOrigin.position, range, out dest))
                {
                    agent.SetDestination(dest);
                }
            }
            agent.ResetPath();
            // wait for agent to reach destination
            yield return BTState.Success;
        }
    } 

    IEnumerator<BTState> Follow()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();           
        if (Vector3.Distance(this.transform.position, target.position) > atkRad)
        {
            Debug.Log("Following");
            agent.SetDestination(target.position);
            anim.SetBool("moving", true);
            yield return BTState.Success;
        }

        agent.ResetPath();
        anim.SetBool("moving", false);
        yield return BTState.Failure;

    }

    IEnumerator<BTState> Attack()
    {
        if (Vector3.Distance(this.transform.position, target.position) <= atkRad)
        {
            Debug.Log("Attacking");
            inCombat = true;
            combatManager.GetComponent<CombatInitiator>().CombatStart(this.gameObject);
            while (inCombat)
            {
                yield return BTState.Continue;
            }
        }
        else
        {
            yield return BTState.Failure;
        }
    }

    bool RandomPoint(Vector3 center, float radius, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * radius;
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
