using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Transform[] stops;
    public float speed;
    private int currentStop;
    private Transform start;
    private Transform end;
    public float duration = 3.0F;
    public GameObject stopParent;
    //private float timer = 0;
    public bool Castel;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))  
        {
            if (currentStop >= stops.Length)
            {
                currentStop = 0;       
                end = stops[0];
            }
            StartCoroutine(CubicBezier());
        }
        if (Input.GetKey(KeyCode.V))
        {
          //  OnDrawGizmos();
        }
     }

    IEnumerator CubicBezier() //basic poly version
    {
        while (currentStop < stops.Length)
        {

            for (float timer = 0; timer < duration; timer += Time.deltaTime)
            {
                Debug.Log(timer);
                Vector3 posControlOne;
                Vector3 posControlTwo;
                if (currentStop == 0)
                {
                    posControlOne = start.position + (end.position - start.position) / 6;
                  
                } 
                else
                {
                    Debug.Log(currentStop);
                    posControlOne = start.position + (end.position - stops[currentStop - 1].position) / 6;
                }
                if (currentStop == stops.Length - 1)
                {
                    posControlTwo = end.position - (end.position - start.position) / 6;
                }
                else
                {
                    Debug.Log(currentStop);
                    posControlTwo = end.position - (stops[currentStop + 1].position - start.position) / 6;
                }
                if (Castel)
                {
                   Vector3 L11 = Vector3.Lerp(start.position, posControlOne, timer / duration);
                    Vector3 L12 = Vector3.Lerp(posControlOne, posControlTwo, timer / duration);
                    Vector3 L13 = Vector3.Lerp(posControlTwo, end.position, timer / duration);
                    Vector3 L21 = Vector3.Lerp(L11, L12, timer / duration);
                    Vector3 L31 = Vector3.Lerp(L12, L13, timer / duration);
                    this.transform.position = Vector3.Lerp(L21, L31, timer / duration);

                   
                }
                else
                {
                    this.transform.position = Mathf.Pow(1 - timer / duration, 3) * start.position +
                        3 * timer / duration * Mathf.Pow(1 - timer / duration, 2) * posControlOne +
                        3 * Mathf.Pow(timer / duration, 2) * (1 - timer / duration) * posControlTwo +
                        Mathf.Pow(timer / duration, 3) * end.position;


                }
                
                yield return null;
            }
            currentStop += 1;
            start = end;
            if (currentStop < stops.Length)
            {
                end = stops[currentStop];
            }
            yield return null;
        }
    }

    /*private void OnDrawGizmos()
    {
        Vector3[] curve = new Vector3[stops.Length];
        for (int i = 0; i < stops.Length; i++)
        {
            curve[i] = stops[i].position;
        }
        Gizmos.color = Color.blue;
        Gizmos.DrawLineStrip(curve, false);
        foreach (Transform p in stops)
        {
            Gizmos.DrawSphere(p.position, 0.2f);
        }
    }*/
}
