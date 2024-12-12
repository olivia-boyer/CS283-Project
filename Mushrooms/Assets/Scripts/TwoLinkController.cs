using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Two : MonoBehaviour
{
    public Transform target;
    public Transform foot;
    private Transform elbow;
    private Transform shoulder;
    // Start is called before the first frame update
    void Start()
    {
        elbow = foot.parent;
        shoulder = elbow.parent;
    }

    // Update is called once per frame
    void Update()
    {

        Debug.DrawLine(shoulder.position, target.position, Color.red);
        Vector3 r = target.position - shoulder.position;
        Vector3 lOne = shoulder.position - elbow.position;
        Vector3 lTwo = elbow.position - foot.position;
        float magR = Vector3.Magnitude(r);
        float magLOne = Vector3.Magnitude(lOne);
        float magLTwo = Vector3.Magnitude(lTwo);
        float theta = Mathf.Rad2Deg*Mathf.Acos((Mathf.Pow(magR, 2) - Mathf.Pow(magLOne, 2) 
            - Mathf.Pow(magLTwo, 2)) / (-2 * magLOne * magLTwo));
        Vector3 eAxis = Vector3.Cross(lOne, lTwo)/Vector3.Magnitude(Vector3.Cross(lOne,lTwo));
        elbow.rotation = Quaternion.AngleAxis(180+theta, eAxis);
        Vector3 e = target.position - foot.position;
        Vector3 axis = Vector3.Cross(r, e) / Vector3.Magnitude(Vector3.Cross(r, e));
        float angle = Mathf.Rad2Deg * Mathf.Atan2(Vector3.Magnitude(Vector3.Cross(r, e)),
            Vector3.Dot(r, r) + Vector3.Dot(r, e));
      shoulder.rotation = shoulder.rotation * Quaternion.AngleAxis(angle, axis);
        Debug.Log((shoulder.position - foot.position) - (shoulder.position - target.position));
        
    }
}
