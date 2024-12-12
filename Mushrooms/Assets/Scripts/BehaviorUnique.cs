using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BTAI;
using System;

public class BehaviorUnique : MonoBehaviour
{
    private Root rootNode = BT.Root();
    public Transform target;
    public float atkRad;
    public float viewRad;
    public int homeCutoff;
    private Animator anim;
    private bool activated;
    public Transform wanderOrigin;
    private NavMeshAgent agent;
    private GameObject[] enemies;
    public float range;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Activate());
        anim = GetComponent<Animator>();
        activated = false;
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        BTNode follow = BT.RunCoroutine(Follow);
        BTNode wander = BT.RunCoroutine(Wander);
        BTNode attack = BT.RunCoroutine(Attack);
        Selector sidekick = BT.Selector();
        Selector checkActivated = BT.Selector();
        ConditionalBranch notActivated = BT.If(() => { return !activated; });
        ConditionalBranch isActivated = BT.If(() => { return activated; });
        isActivated.OpenBranch(attack);
        isActivated.OpenBranch(follow);
        notActivated.OpenBranch(wander);
        checkActivated.OpenBranch(isActivated);
        checkActivated.OpenBranch(notActivated);
        rootNode.OpenBranch(checkActivated);
     }

    // Update is called once per frame
    void Update()
    {
        rootNode.Tick();
    }

    IEnumerator<BTState> Attack() //checks for nearby enemies, approaches them, and plays attack animation
    {
        Transform enemyLoc;
        if (EnemyInRange(out enemyLoc) &&
            Vector3.Distance(transform.position, target.position) <= viewRad*5)
        {
            transform.LookAt(enemyLoc, Vector3.up);
            if (Vector3.Distance(enemyLoc.position, transform.position) > atkRad)
            {
                agent.SetDestination(enemyLoc.position);
                anim.SetBool("moving", true);
            }
            else
            {
                anim.SetBool("attack", true);
            }
            agent.ResetPath();
            yield return BTState.Success;
        }
        anim.SetBool("attack", false);
        yield return BTState.Failure;
    }



    IEnumerator<BTState> Follow() //follows player character
    {
       Vector3 personalSpace = new Vector3(atkRad - 2, 0, atkRad - 2);
       if (Vector3.Distance(transform.position, target.position) >= atkRad)
        {
            agent.SetDestination(target.position - personalSpace);
            anim.SetBool("moving", true);
            yield return BTState.Success;
        }
        agent.ResetPath();
        anim.SetBool("moving", false);
        yield return BTState.Failure;
    }



    IEnumerator<BTState> Wander() //wanders around safe area before being approached
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        Vector3 dest;
        if (agent.remainingDistance < 1.0f)
        {
            if (RandomPoint(
               wanderOrigin.position, range, out dest))
            {
                agent.SetDestination(dest);
            }
        }
        // wait for agent to reach destination
        yield return BTState.Success;
    }



    IEnumerator Activate() //checks if player character has approached
    {
        while (!activated)
        {
            //anim.SetBool("moving", false);
            if (Vector3.Distance(this.transform.position, target.position) <= viewRad)
            {
                activated = true;
            }
            yield return null;
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

    bool EnemyInRange(out Transform enemyLoc)
    {
        foreach(GameObject enemy in enemies)
        {
            if (Vector3.Distance(enemy.transform.position, transform.position) < viewRad)
            {
                enemyLoc = enemy.transform;
                return true;
            }
        }
        enemyLoc = null;
        return false;
    }
}
