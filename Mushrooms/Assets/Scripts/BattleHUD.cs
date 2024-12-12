using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


//Based on code from this tutorial https://www.youtube.com/watch?v=_1pz_ohupPs
public class BattleHUD : MonoBehaviour
{
    public TMP_Text hp;


    public void SetHP(BattleUnit unit)
    {
        hp.text = "HP: " + unit.currentHp.ToString() +
            " / " + unit.maxHp.ToString();
    }

   
}
