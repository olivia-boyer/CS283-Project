using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringFollowCamera : MonoBehaviour
{
    public Transform target;
    public int zDist;
    public int yDist;
    public float dampConstant;
    public float springConstant;
    private Vector3 velocity;
    private Vector3 actualPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        Vector3 idealEye = target.position - target.forward * zDist + target.up * yDist;
        Vector3 camForward = target.position - actualPosition;
        Vector3 displacement = actualPosition - idealEye;
        Vector3 springAccel = (-springConstant * displacement) - (dampConstant * velocity);
        velocity += springAccel * Time.deltaTime;
        actualPosition += velocity * Time.deltaTime;
        transform.position = actualPosition;
        transform.rotation = Quaternion.LookRotation(camForward);

    }
}
