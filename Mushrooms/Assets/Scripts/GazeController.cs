using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GazeController : MonoBehaviour
{
    public Transform target;
    public Transform pointer; //the part of head to face target
    private Vector3 axis;
    private float angle;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     /*   Vector3 r = target.position - pointer.parent.position;
        Vector3 e = target.position - pointer.position;
        axis = Vector3.Cross(r, e) / Vector3.Magnitude(Vector3.Cross(r, e));
        angle = Mathf.Rad2Deg*Mathf.Atan2(Vector3.Magnitude(Vector3.Cross(r, e)), Vector3.Dot(r, r) + Vector3.Dot(r, e));
       /* Quaternion temp = Quaternion.LookRotation(r, Vector3.up);
        Quaternion fix = new Quaternion(90, -90, 0, 1);
        temp = fix * temp;
        pointer.parent.rotation = temp;*/
      /*pointer.parent.rotation = pointer.parent.rotation * Quaternion.AngleAxis(angle, axis);
        Debug.DrawLine(target.position, pointer.parent.position, Color.red);
        */
    }
}
