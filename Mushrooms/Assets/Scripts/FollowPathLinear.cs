using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPathLinear : MonoBehaviour
{
    private Transform[] stops;
    //public float speed;
    private int currentStop;//index of location traveling to in array
    private Transform start;
    private Transform end;
    public float duration = 3.0F;
    public GameObject stopParent;
    private float timer = 0;


    // Start is called before the first frame update
    void Start()
    {
        stops = new Transform[stopParent.transform.childCount];
        for (int i = 0; i < stops.Length; i++)
        {
            stops[i] = stopParent.transform.GetChild(i);
        }
        start = this.transform;
        end = stops[0];
        currentStop = 0;
       //StartCoroutine(DoLerp());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))  
        {
            if (currentStop >= stops.Length)
            {
                currentStop = 0;
                timer = 0;
                start = this.transform;
                end = stops[0];
            }
            StartCoroutine(DoLerp());
        }
    }

    IEnumerator DoLerp()
    {
        while (currentStop < stops.Length)
        {
           
            for (; timer < duration; timer += Time.deltaTime)  //first lerp weirdly fast(?)
            {
                Debug.Log(timer);
                float u = timer / duration;
                transform.position = Vector3.Lerp(start.position, end.position, u);
                transform.rotation = Quaternion.Slerp(start.rotation,
                    end.rotation, u);
                yield return null;
            }
            currentStop += 1;
            start = end;
            if (currentStop < stops.Length)
            {
                end = stops[currentStop];
                timer = 0;
            }
            yield return null;
        }
    }
}

