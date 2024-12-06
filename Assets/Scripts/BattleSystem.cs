using System;
using System.Collections;
using System.Collections.Generic;
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
    public BattleHUD playerHUD;

    public Transform enemySpawn1;
    public BattleHUD enemyHUD1;

    public Transform enemySpawn2;
    public BattleHUD enemyHUD2;

    public Transform enemySpawn3;
    public BattleHUD enemyHUD3;

    public List<Enemy> enemies;

    Player playerUnit;
    Enemy enemyUnit1;
    Enemy enemyUnit2;
    Enemy enemyUnit3;

    public BattleState state;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    void Update()
    {

    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerSpawn);
        playerUnit = playerGO.GetComponent<Player>();

        GameObject enemyGO = Instantiate(enemyPrefab1, enemySpawn1);
        enemyUnit1 = enemyGO.GetComponent<Enemy>();
        GameObject enemyGO2 = Instantiate(enemyPrefab1, enemySpawn2);
        enemyUnit2 = enemyGO2.GetComponent<Enemy>();
        GameObject enemyGO3 = Instantiate(enemyPrefab1, enemySpawn3);
        enemyUnit3 = enemyGO3.GetComponent<Enemy>();

        enemies.Add(enemyUnit1);
        enemies.Add(enemyUnit2);
        enemies.Add(enemyUnit3);

        playerHUD.SetHUD(playerUnit);
        playerHUD.SetHUDPlayer(playerUnit);
        enemyHUD1.SetHUD(enemyUnit1);
        enemyHUD2.SetHUD(enemyUnit2);
        enemyHUD3.SetHUD(enemyUnit3);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();

    }

    IEnumerator WandCast1()
    {
        state = BattleState.CASTING;

        wand1.CalculateValues();

        if (playerUnit.ManaChange(wand1.ManaCost,true))
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
        playerUnit.ManaChange(3,false);
        playerUnit.ResetShield();
        playerHUD.SetHUD(playerUnit);
        playerHUD.SetMana(playerUnit);
        enemyHUD1.SetHUD(enemyUnit1);
        Debug.Log("Player's Turn!");
    }

    IEnumerator EnemyTurn()
    {
        bool isDead;
        Debug.Log("Enemy Attacks!");
        foreach (var enemy in enemies)
        {
            enemy.ResetShield();
            yield return new WaitForSeconds(1f);

            isDead = playerUnit.TakeDamage(10);

            playerHUD.SetHP(playerUnit.currentHP);

            yield return new WaitForSeconds(1f);

            if (isDead)
            {
                state = BattleState.LOST;
                EndBattle();
            }
        }
        state = BattleState.PLAYERTURN;
        PlayerTurn();
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
