using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    public GameObject template; //the object to be spawned
    public float range; //radius of the spawn sphere
    public int max; //maximum numer of objects
   // private float yValue;
    private Vector3 center;
    private Vector3 aboveGround;

    // Start is called before the first frame update
    void Start()
    {
        aboveGround = new Vector3(0f, 5.0f, 0.0f);
      //  yValue = 12f; //couldn't figure out what I was supposed to do with Collider.Bounds 
        center = transform.position;
        spawnObjects(max);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void spawnObjects(int num)
    {

        /* for (int i = 0; i < num; i++)
         {

             Vector3 objectPos = new Vector3(UnityEngine.Random.Range
                 (-range + center.x,range + center.x), yValue, 
                 UnityEngine.Random.Range(-range + center.z, range + center.z));

             UnityEngine.Object.Instantiate(template, objectPos, 
                 Quaternion.identity, gameObject.transform);
         }*/
        for (int i = 0; i < num; i++)
        {

            Vector3 objectPos;
            RandomPoint(center, range, out objectPos);
            objectPos = objectPos + aboveGround;
            UnityEngine.Object.Instantiate(template, objectPos,
                 Quaternion.identity, gameObject.transform);

        }
    }

    public void respawn(GameObject collected, Vector3 scale) //I had the coin script call this
    {
        /* collected.transform.position = new Vector3(UnityEngine.Random.Range
                     (-range + center.x, range + center.x), yValue,
                     UnityEngine.Random.Range(-range + center.z,
                     range + center.z));
         collected.transform.localScale = scale;
         collected.GetComponent<BoxCollider>().enabled = true;
         collected.SetActive(true);

        */
        Vector3 objectPos;
        RandomPoint(center, range, out objectPos);
        objectPos = objectPos + aboveGround;
        collected.transform.position = objectPos;
        collected.transform.localScale = scale;
        collected.GetComponent<BoxCollider>().enabled = true;
        collected.SetActive(true);

    }

    //randompoint method from unity documentation
    bool RandomPoint(Vector3 origin, float radius, out Vector3 result)
    {
        while (true)
        {
            Vector3 randomPoint = origin + UnityEngine.Random.insideUnitSphere * radius;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        } 
        //result = Vector3.zero;
       // return false;

    }
}
