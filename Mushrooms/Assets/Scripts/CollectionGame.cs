using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CollectionGame : MonoBehaviour
{
    public TMP_Text countDisplay;
    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Coins")
        {
            count += 1;
            countDisplay.text = count.ToString();
            GameObject coin = collision.gameObject;
            coin.GetComponent<AnimateCoin>().CoinAnimation();
            
        }
    }
}
