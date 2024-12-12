using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tour : MonoBehaviour
{
    public float speed;
    private Transform[] poi;
    private int tourSpot = 0;
    public GameObject cam;
    private int prev;
    private Vector3 tourPos;
    private Quaternion tourRot;
    private float distance;
    private float timeElapsed;

    void Start()
    {
        poi = new Transform[3];
        GameObject poilist = GameObject.Find("poi");//
        for (int i = 0; i < poilist.transform.childCount; i++)
        {
            poi[i] = poilist.transform.GetChild(i);
        }
        cam.transform.position = poi[0].position;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.N))
        {
           tourPos = cam.transform.position;
           tourRot = cam.transform.rotation;
            prev = tourSpot;
            tourSpot += 1;
            if (tourSpot > 2)
            {
                tourSpot = 0;
            }
            distance = Vector3.Distance(poi[tourSpot].position, tourPos);
            timeElapsed = 0;
            
        }
        float time = distance / (speed);
        timeElapsed += Time.deltaTime;
        float u = Mathf.Clamp(timeElapsed,0, time) / time;
        Debug.Log(u);
        if (u < 1) {
            cam.transform.position = Vector3.Lerp(tourPos, poi[tourSpot].position, u);
            cam.transform.rotation = Quaternion.Slerp(tourRot, poi[tourSpot].rotation, u);
        }
    }
}
