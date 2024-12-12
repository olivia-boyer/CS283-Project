using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyCamera : MonoBehaviour
{
    public int lookSensitivity;
    public int moveSpeed;
    void Start()
    {
         
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0, moveSpeed);
        } else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, -moveSpeed);
        }      
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(moveSpeed, 0, 0);
        } else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-moveSpeed, 0, 0);
        }
        Vector2 mousePos = lookSensitivity * new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));
        Quaternion rotation = transform.rotation;
        Quaternion horizontal = Quaternion.AngleAxis(mousePos.x, Vector3.up);
        Quaternion vertical = Quaternion.AngleAxis(mousePos.y, Vector3.right);
        transform.rotation = rotation * horizontal * vertical;
    }
}

