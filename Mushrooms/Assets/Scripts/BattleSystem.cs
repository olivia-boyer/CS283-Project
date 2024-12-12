using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

//Based on code from this tutorial https://www.youtube.com/watch?v=_1pz_ohupPs
public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class BattleSystem : MonoBehaviour
{
    public GameObject playerOne;
    public GameObject playerTwo;
    private GameObject enemyPrefab;

    public Transform enemyLoc;

    public BattleState state;

    private BattleUnit playerOneUnit;
    private BattleUnit playerTwoUnit;
    private BattleUnit enemyUnit;

    public TMP_Text dialogueText;

    public BattleHUD playerOneHUD;
    public BattleHUD playerTwoHUD;
    public BattleHUD enemyHUD;

    private int turnNum = 0;

    public GameObject playerOneButtons;
    public GameObject playerTwoButtons;

    public float waitTime;
    private bool isHealing;
    private bool selectingTarget;

   
    public void Encounter(GameObject enemyType)
    {
        enemyPrefab = Instantiate(enemyType, enemyLoc);
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }


    IEnumerator SetupBattle()
    {
       // GameObject enemyGo = Instantiate(enemyPrefab, enemyLoc);
        enemyUnit = enemyPrefab.GetComponent<BattleUnit>();
        enemyUnit.Heal(enemyUnit.maxHp);

        playerOneUnit = playerOne.GetComponent<BattleUnit>();
        playerTwoUnit = playerTwo.GetComponent<BattleUnit>();

        dialogueText.text = "You've encountered a wild " + enemyUnit.unitName;

        playerOneHUD.SetHP(playerOneUnit);
        playerTwoHUD.SetHP(playerTwoUnit);
        enemyHUD.SetHP(enemyUnit);

        yield return new WaitForSeconds(waitTime);

        state = BattleState.PLAYERTURN;
        turnNum = 1;
        StartCoroutine(PlayerOneTurn());
        
    }

    IEnumerator PlayerOneTurn()
    {
        playerOneButtons.SetActive(true); 
        dialogueText.text = "Choose Sian's action";
        Debug.Log(turnNum.ToString());
        while (turnNum == 1)
        {
            yield return null;
        }

        playerOneButtons.SetActive(false); //need to disappear faster

        yield return new WaitForSeconds(waitTime);

        StartCoroutine(PlayerTwoTurn());

    }

    IEnumerator PlayerTwoTurn()
    {
        playerTwoButtons.SetActive(true);
        dialogueText.text = "Choose Amanita's action";
        while (turnNum == 2)
        {
            yield return null;
        }

        playerTwoButtons.SetActive(false);

        yield return new WaitForSeconds(waitTime);

        StartCoroutine(EnemyTurn()); 
    }


    public void OnAttackButton(BattleUnit player)
    {       
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }    
       PlayerAttack(player);
    }

    public void OnHealButton(BattleUnit healer)
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        isHealing = true;
        selectingTarget = true;
        dialogueText.text = "Click the name of who you want to heal.";
    }

    public void OnAtkBuffButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        isHealing = false;
        selectingTarget = true;
        dialogueText.text = "Click the name of whose attack you want to boost.";

    }

    public void OnDefendButton(BattleUnit player)
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        player.BuffDefense();
        dialogueText.text = player.unitName + " put up their guard";
        turnNum = (turnNum + 1) % 3;
    }

    void  PlayerAttack(BattleUnit player) 
    {
        Debug.Log("attacking");
        bool isDead = enemyUnit.TakeDamage(player.Damage());


        dialogueText.text = "the attack was successful";

        enemyHUD.SetHP(enemyUnit);

        //player.buttons.SetActive(false);

        if (isDead)
        {
            state = BattleState.WON;
            StartCoroutine(endBattle());
        }  else
        {
            player.attackBuff = 0;
            turnNum = (turnNum + 1) % 3;
        }
    }

    public void SelectTarget(BattleUnit target)
    {
        if (selectingTarget)
        {
            selectingTarget = false;
            Debug.Log(isHealing.ToString());
            if (isHealing)
            {
                Heal(target);
            } else
            {
                AtkBuff(target);
            }
        }
    }
    

    void Heal(BattleUnit target)
    {
        
        target.Heal(playerTwoUnit.Damage());
        playerOneHUD.SetHP(playerOneUnit);
        playerTwoHUD.SetHP(playerTwoUnit);

        dialogueText.text = target.unitName + " has been healed!";

        turnNum = (turnNum + 1) % 3;    
    }

   

    void AtkBuff(BattleUnit target)
    {
        target.BuffAtk(playerOneUnit.Damage());

        dialogueText.text = target.unitName + " has had their attack increased!";

        turnNum = (turnNum + 1) % 3;
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + " attacked!";

        enemyPrefab.GetComponent<Animator>().SetTrigger("Attack");
        bool isDead;

        if (UnityEngine.Random.Range(1,3) == 2)
        {
            isDead = playerOneUnit.TakeDamage(enemyUnit.Damage());
            playerOneHUD.SetHP(playerOneUnit);
        } else
        {
            isDead = playerTwoUnit.TakeDamage(enemyUnit.Damage());
            playerTwoHUD.SetHP(playerTwoUnit);
        }
    
        yield return new WaitForSeconds(waitTime);

        if (isDead)
        {
            StartCoroutine(endBattle());
            state = BattleState.LOST;
        }
        else
        {
            state = BattleState.PLAYERTURN;
            turnNum = (turnNum + 1) % 3;
            StartCoroutine(PlayerOneTurn());
           
        }
    }

    IEnumerator endBattle()
    {
        yield return new WaitForSeconds(waitTime);
        if (state == BattleState.WON)
        {
            dialogueText.text = "Congrats! You Won!";
            yield return new WaitForSeconds(waitTime);
            GetComponent<CombatInitiator>().CombatVictory();
            Destroy(enemyPrefab);
            playerOneButtons.SetActive(false);
            playerTwoButtons.SetActive(false);
            StopAllCoroutines();

        } else if (state == BattleState.LOST)
        {
            dialogueText.text = "Game Over";
            yield return new WaitForSeconds(waitTime);
            playerOneUnit.Heal(playerOneUnit.maxHp);
            playerTwoUnit.Heal(playerTwoUnit.maxHp);
            GetComponent<CombatInitiator>().CombatDefeat();
            Destroy(enemyPrefab);
            playerTwoButtons.SetActive(false);
            playerOneButtons.SetActive(false);
            StopAllCoroutines();
        }

    }

  
}
