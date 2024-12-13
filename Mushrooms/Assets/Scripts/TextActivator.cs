using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextActivator : MonoBehaviour
{
    public GameObject activatedText;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(ShowText());
        }
    }

    IEnumerator ShowText()
    {
        activatedText.SetActive(true);
        yield return new WaitForSeconds(2f);
        activatedText.SetActive(false);
    }
}
