using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
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

    public TMP_Text castingText;

    Player playerUnit;
    Enemy enemyUnit1;
    Enemy enemyUnit2;
    Enemy enemyUnit3;

    bool isTarget = false;

    public static BattleState state;

    public Animator playerAnimator;


    private void Awake()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerSpawn);
        playerUnit = playerGO.GetComponent<Player>();
        playerAnimator = playerGO.GetComponentInChildren<Animator>();
    }
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
        

        GameObject enemyGO = Instantiate(enemyPrefab1, enemySpawn1);
        enemyUnit1 = enemyGO.GetComponent<Enemy>();
        GameObject enemyGO2 = Instantiate(enemyPrefab1, enemySpawn2);
        enemyUnit2 = enemyGO2.GetComponent<Enemy>();
        //GameObject enemyGO3 = Instantiate(enemyPrefab1, enemySpawn3);
        //enemyUnit3 = enemyGO3.GetComponent<Enemy>();

        enemies.Add(enemyUnit1);
        enemies.Add(enemyUnit2);
        //enemies.Add(enemyUnit3);

        enemyUnit1.enemyHUD = enemyHUD1;
        enemyUnit2.enemyHUD = enemyHUD2;
        //enemyUnit3.enemyHUD = enemyHUD3;

        playerHUD.SetHUD(playerUnit);
        playerHUD.SetHUDPlayer(playerUnit);
        enemyUnit1.enemyHUD.SetHUD(enemyUnit1);
        enemyUnit2.enemyHUD.SetHUD(enemyUnit2);
        //enemyUnit3.enemyHUD.SetHUD(enemyUnit3);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();

    }

    IEnumerator WandCast1()
    {
        state = BattleState.CASTING;

        wand1.CalculateValues();

        if (playerUnit.ManaChange(wand1.ManaCost, true))
        {
            Debug.Log("Breaking!");
            castingText.text = "Not Enough Mana!";
            castingText.enabled = true;
            state = BattleState.PLAYERTURN;
            yield return new WaitForSeconds(1f);
            castingText.enabled = false;
            yield break;

        }
        else
        {
            castingText.text = "Select an Enemy!";
        }

        yield return TargetEnemies();

        if (!isTarget)
        {
            print("Breaking2!");
            playerUnit.ManaChange(wand1.ManaCost * -1, false);
            state = BattleState.PLAYERTURN;
            yield break;
        }

        playerHUD.SetMana(playerUnit);

        playerAnimator.SetBool("Attacked", true);

        foreach (Enemy enemy in enemies)
        {
            if (enemy.target)
            {
                bool isDead = enemy.TakeDamage(wand1.DamageValue);
            }
        }

        playerUnit.HealDamage(wand1.HealValue);
        playerHUD.SetHP(playerUnit.currentHP);

        playerUnit.GainShield(wand1.ShieldValue);
        playerHUD.SetShield(playerUnit);

        enemyHUD1.SetHP(enemyUnit1.currentHP);
        enemyHUD2.SetHP(enemyUnit2.currentHP);

        enemyHUD1.SetShield(enemyUnit1);
        enemyHUD2.SetShield(enemyUnit2);
        //enemyHUD3.SetHP(enemyUnit3.currentHP);
        Debug.Log("Casted Wand!");

        yield return new WaitForSeconds(0.5f);

        playerAnimator.SetBool("Attacked", false);

        foreach (var enemy in enemies)
        {
            if (enemy.isDead)
            {
                enemy.gameObject.SetActive(false);
                enemy.enemyHUD.gameObject.SetActive(false);
            }
            else
            {
                state = BattleState.PLAYERTURN;
            }
        }
        foreach (var enemy in enemies)
        {
            int count = 0;
            if (!enemy.isDead)
            {

            }
            else
            {
                count += 1;
            }
            if (count == enemies.Count)
            {
                state = BattleState.WON;
                EndBattle();
            }
        }
    }

    IEnumerator EndTurn()
    {
        playerUnit.EndTurnEffects();
        playerHUD.SetHP(playerUnit.currentHP);
        playerHUD.SetShield(playerUnit);
        foreach (var enemy in enemies)
        {
            enemy.EndTurnEffects();
            enemy.enemyHUD.SetHP(enemy.currentHP);
        }
        state = BattleState.ENEMYTURN;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(EnemyTurn());
    }

    void PlayerTurn()
    {
        playerUnit.ManaChange(3, false);
        playerUnit.ResetShield();
        playerHUD.SetHUD(playerUnit);
        playerHUD.SetMana(playerUnit);
        playerHUD.SetShield(playerUnit);
        foreach (var enemy in enemies)
        {
            enemy.enemyHUD.SetMove(enemy.enemyMoves.LoadMove());
        }
        Debug.Log("Player's Turn!");
    }

    IEnumerator EnemyTurn()
    {
        bool isDead;
        Debug.Log("Enemy Attacks!");
        foreach (var enemy in enemies)
        {
            if (enemy.gameObject.activeSelf)
            {
                enemy.ResetShield();
                yield return new WaitForSeconds(1f);

                enemy.enemyMoves.UseMove();

                

                enemy.HealDamage(enemy.enemyMoves.HealValue);
                enemy.enemyHUD.SetHP(enemy.currentHP);
                enemy.GainShield(enemy.enemyMoves.ShieldValue);
                enemy.enemyHUD.SetShield(enemy);
                enemy.AttackAnim();

                if (enemy.enemyMoves.statusEffects != null)
                {
                    print("Battle Status Not Null");
                    foreach (StackableEffectSO stackEffectData in enemy.enemyMoves.statusEffects)
                    {
                        playerUnit.AddStatus(stackEffectData);
                    }
                }
                playerUnit.TakeDamage(enemy.enemyMoves.DamageValue);
                playerHUD.SetHP(playerUnit.currentHP);
                playerHUD.SetShield(playerUnit);

                yield return new WaitForSeconds(1f);
            }
            if (playerUnit.isDead)
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

    public void MarkTargets(Enemy _enemy)
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.TargetDeselect();
        }
        _enemy.TargetSelect();
    }

    IEnumerator TargetEnemies()
    {
        print("targeting");   
        if (enemies.Count == 1)
        {
            enemies[0].TargetSelect();
            isTarget = true;
            yield break;
        }
        castingText.enabled = true;
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        var rayhit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()));

        

        if (!rayhit.collider)
        {
            isTarget = false;
            castingText.text = "Select an Enemy!";
            castingText.enabled = false;
            print("breaking3");
            yield break;
        }

        if (rayhit.collider.gameObject.tag == "Enemy")
        {
            print("enemyTargeted");
            Enemy enemy = (Enemy)rayhit.collider.gameObject.GetComponent<Enemy>();
            MarkTargets(enemy);
            castingText.enabled = false;
            isTarget = true;
        }
        Debug.Log(rayhit.collider.gameObject.name);
        yield break;
    }
}
