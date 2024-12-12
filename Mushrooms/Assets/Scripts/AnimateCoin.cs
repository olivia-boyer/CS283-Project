using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using System.Timers;
using UnityEngine;

//does the collection animation and calls spawner to respawn
public class AnimateCoin : MonoBehaviour
{

    public float duration;
    public int rotation;
    private Vector3 ogScale;
    public int up;
    // Start is called before the first frame update
    void Start()
    {
        ogScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CoinAnimation()
    {
       
        gameObject.GetComponent<BoxCollider>().enabled = false;
        StartCoroutine(Anim());
    }

    IEnumerator Anim()
    {
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
           
            transform.Rotate(0, rotation * Time.deltaTime, 0);
            transform.localScale = Vector3.Lerp(ogScale, Vector3.zero,
                timer / duration);
            transform.Translate(0, up * Time.deltaTime, 0);
            yield return null;
        }
        UnityEngine.Debug.Log("here");
        gameObject.SetActive(false);
        transform.parent.gameObject.GetComponent<Spawner>().respawn //calling spawner
            (gameObject, ogScale);
    }
}
