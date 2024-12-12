using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatInitiator : MonoBehaviour
{
    public Camera followCam;
    public Camera battleCam;

    public GameObject playerOne;
    public GameObject playerTwo;
    public Transform respawnPoint;

    public GameObject CombatUI;
    private GameObject currentEnemy;

    public void CombatStart(GameObject enemy) 
    {
        currentEnemy = enemy;
       // Debug.Log("Message Received");
        battleCam.enabled = true;
        followCam.enabled = false;
        currentEnemy.GetComponent<EnemyBehavior>().inCombat = true;
        CombatUI.SetActive(true);
        GetComponent<BattleSystem>().Encounter(currentEnemy.GetComponent<EnemyBehavior>().combatPrefab);
        playerOne.SetActive(false);
        playerTwo.SetActive(false);
         
    }

    public void CombatVictory()
    {
        playerOne.SetActive(true);
        playerTwo.SetActive(true);
        followCam.enabled = true;
        battleCam.enabled = false;
        currentEnemy.SetActive(false);
        CombatUI.SetActive(false);
        //enemy.GetComponent<EnemyBehavior>().inCombat = false;
    }

    public void CombatDefeat()
    {
        playerOne.SetActive(true);
        playerTwo.SetActive(true);
        playerOne.transform.position = respawnPoint.position;
        playerTwo.transform.position = respawnPoint.position + (Vector3.forward * -5);     
        followCam.enabled = true;
        battleCam.enabled = false;
        currentEnemy.GetComponent<EnemyBehavior>().inCombat = false;
        CombatUI.SetActive(false);
    }

    public GameObject getEnemy()
    {
        return currentEnemy;
    }
    
    
}
