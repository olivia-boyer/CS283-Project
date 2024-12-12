using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit : MonoBehaviour
{
    //Based on code from this tutorial https://www.youtube.com/watch?v=_1pz_ohupPs
    public string unitName;
    public int attack;
    public int maxHp;
    public int currentHp;
    public int attackBuff = 0;
    public int defense;

   // public GameObject buttons;

    public bool TakeDamage(int damage)
    {
        currentHp = Mathf.Clamp(currentHp - damage + defense, 0, maxHp);
        defense = 0;
        if(currentHp <= 0)
        {
            return true;
        } else
        {
            return false;
        }
        
    }

    public void Heal(int healPower)
    {
        currentHp = Mathf.Clamp(currentHp + healPower, 0, maxHp);
    }

    public int Damage()
    {
        return Random.Range((attack + attackBuff) * 3 / 4, (attack + attackBuff) * 4 / 3);
    }

    public float HPPercent()
    {
        return (currentHp / maxHp);
    }

    public void BuffAtk(int amount)
    {
        attackBuff += amount;
    }

    public void BuffDefense()
    {
        defense += (attack * 3 / 4);
    }
}
