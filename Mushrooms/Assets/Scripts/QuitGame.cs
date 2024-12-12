using UnityEngine;
using System.Collections;

// Quits the player when the user hits escape
//literally copied and pasted from unity documentation


public class QuitGame : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
