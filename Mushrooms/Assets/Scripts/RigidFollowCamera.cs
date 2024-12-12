using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidFollowCamera : MonoBehaviour
{
    public Transform target;
    public int zDistance;
    public int yDistance;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        Vector3 tPos = target.position;
        Vector3 tUp = target.up;
        Vector3 tForward = target.forward;
        Vector3 camPos = tPos - tForward * zDistance + tUp * yDistance;
        Vector3 camForward = tPos - camPos;
        transform.position = camPos;
        transform.rotation = Quaternion.LookRotation(camForward);
    }
}
