using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST, CASTING }

public class BattleSystem : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject enemyPrefab1;

    public WandObject wand1;
    public WandObject wand2;

    public Transform playerSpawn;
    public Transform enemySpawn1;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD1;

    Player playerUnit;
    Enemy enemyUnit1;

    public BattleState state;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerSpawn);
        playerUnit = playerGO.GetComponent<Player>();

        GameObject enemyGO = Instantiate(enemyPrefab1, enemySpawn1);
        enemyUnit1 = enemyGO.GetComponent<Enemy>();

        playerHUD.SetHUD(playerUnit);
        playerHUD.SetHUDPlayer(playerUnit);
        enemyHUD1.SetHUD(enemyUnit1);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();

    }

    IEnumerator WandCast1()
    {
        state = BattleState.CASTING;

        wand1.CalculateValues();

        if (playerUnit.ManaChange(wand1.ManaCost))
        {
            Debug.Log("Breaking!");
            //playerUnit.ManaChange(wand1.ManaCost * -1);
            state = BattleState.PLAYERTURN;
            yield break;
        }
        playerHUD.SetMana(playerUnit);

        bool isDead = enemyUnit1.TakeDamage(wand1.DamageValue);

        playerUnit.HealDamage(wand1.HealValue);
        playerHUD.SetHP(playerUnit.currentHP);

        playerUnit.GainShield(wand1.ShieldValue);

        enemyHUD1.SetHP(enemyUnit1.currentHP);
        Debug.Log("Casted Wand!");

        yield return new WaitForSeconds(0.5f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
        }
    }

    IEnumerator EndTurn()
    {
        yield return new WaitForSeconds(0.5f);
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    void PlayerTurn()
    {
        playerUnit.ManaChange(3);
        playerUnit.ResetShield();
        playerHUD.SetHUD(playerUnit);
        playerHUD.SetMana(playerUnit);
        enemyHUD1.SetHUD(enemyUnit1);
        Debug.Log("Player's Turn!");
    }

    IEnumerator EnemyTurn()
    {
        Debug.Log("Enemy Attacks!");
        enemyUnit1.ResetShield();

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(10);

        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    private void EndBattle()
    {
        if (state == BattleState.WON)
        {
            Debug.Log("You Won!");
        }
        else if (state == BattleState.LOST)
        {
            Debug.Log("You Lost!");
        }
    }

    public void OnCastButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(WandCast1());
    }

    public void OnEndTurnButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(EndTurn());
    }
}
