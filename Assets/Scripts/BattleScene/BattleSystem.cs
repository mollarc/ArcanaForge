using FMOD.Studio;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST, CASTING , ENDING}

public class BattleSystem : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject enemyPrefab1;

    public InventoryController inventoryController;
    public BattleWin winScreen;

    public WandObject wand1;
    public WandObject wand2;

    public FightsDataSO fightData;

    public Transform playerSpawn;
    public BattleHUD playerHUD;

    public List<Enemy> enemies = new List<Enemy>();
    public List<Transform> enemySpawns = new List<Transform>();
    public List<BattleHUD> enemyHUDS = new List<BattleHUD>();

    public TMP_Text castingText;

    public CinemachineCameraLogic cinemachineScript;

    Player playerUnit;

    bool isTarget = false;

    public static BattleState state;

    public Animator playerAnimator;


    private void Awake()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerSpawn);
        playerUnit = playerGO.GetComponent<Player>();
        playerHUD = playerGO.GetComponentInChildren<BattleHUD>();
        playerAnimator = playerGO.GetComponentInChildren<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = BattleState.START;
        GameObject currentManaUI = GameObject.FindGameObjectWithTag("CurrentManaUI");
        playerHUD.currentManaText = currentManaUI.GetComponentInChildren<TMP_Text>();
        GameObject maxManaUI = GameObject.FindGameObjectWithTag("MaxManaUI");
        playerHUD.maxManaText = maxManaUI.GetComponentInChildren<TMP_Text>();
        StartCoroutine(SetupBattle());
    }

    void Update()
    {

    }

    IEnumerator SetupBattle()
    {
        StartCoroutine(PlayerStartCutscene());
        fightData = FightDB.Instance.GetSequentialFight();
        for (int i = 0; i < fightData.enemyDataList.Count; i++)
        {
            GameObject enemyGo = Instantiate(fightData.enemyDataList[i], enemySpawns[i]);
            enemies.Add(enemyGo.GetComponent<Enemy>());
            enemyHUDS.Add(enemies[i].GetComponent<BattleHUD>());
        }

        playerUnit.SetHUD();
        playerHUD.SetHUDPlayer(playerUnit);

        foreach (Enemy enemy in enemies)
        {
            enemy.SetHUD();
        }

        yield return new WaitForSeconds(1.5f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();

    }

    IEnumerator WandCast1()
    {
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
        if (wand1.targets != 0)
        {
            yield return TargetEnemies();
            if (!isTarget)
            {
                print("Breaking2!");
                playerUnit.ManaChange(wand1.ManaCost * -1, false);
                state = BattleState.PLAYERTURN;
                yield break;
            }
        }
        playerHUD.SetMana(playerUnit);
        wand1.CheckMana();
        playerAnimator.SetBool("Attacked", true);

        //Logic for hitting enemies
        StartCoroutine(PlayWandSound(wand1));
        foreach (Enemy enemy in enemies)
        {
            if (enemy.target)
            {
                enemy.TargetDeselect();
                enemy.AttackedAnim();
                enemy.TakeDamage(wand1.DamageValue, true);
                if (wand1.cloneStatusEffects != null)
                {
                    foreach (StackableEffectSO stackEffectData in wand1.cloneStatusEffects)
                    {
                        enemy.AddStatus(stackEffectData);
                    }
                    enemy.battleHUD.DisplayStatus(enemy.stackableEffects);
                    enemy.RefreshMove();
                }
            }
        }

        playerUnit.HealDamage(wand1.HealValue);
        playerHUD.SetHP(playerUnit.currentHP);

        playerUnit.GainShield(wand1.ShieldValue);
        playerHUD.SetShield(playerUnit);

        foreach (Enemy _enemy in enemies)
        {
            _enemy.SetHUD();
        }

        Debug.Log("Casted Wand!");

        wand1.MarkUsed();

        yield return new WaitForSeconds(0.5f);

        playerAnimator.SetBool("Attacked", false);

        CheckEnemys();
        state = BattleState.PLAYERTURN;
    }

    IEnumerator PlayWandSound(WandObject wand)
    {
        foreach (string eventpath in wand.FmodSoundPaths)
        {
            EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(eventpath);
            if (eventInstance.isValid())
            {
                eventInstance.start();
                yield return new WaitForSeconds(0.5f);
            }
        }
        yield break;
    }

    IEnumerator EndTurn()
    {
        cinemachineScript.BattleEndTurnCamera();
        playerUnit.EndTurnEffects();
        playerHUD.SetHP(playerUnit.currentHP);
        playerHUD.SetShield(playerUnit);
        playerHUD.DisplayStatus(playerUnit.stackableEffects);
        foreach (var enemy in enemies)
        {
            enemy.EndTurnEffects();
            enemy.SetHUD();
            enemy.battleHUD.DisplayStatus(enemy.stackableEffects);
        }
        inventoryController.EndTurn();
        wand1.EndTurn();
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(EnemyTurn());
        CheckEnemys();
    }

    void PlayerTurn()
    {
        state = BattleState.PLAYERTURN;
        cinemachineScript.BattlePlayerTurnCamera();
        playerUnit.ManaChange(3, false);
        wand1.CheckMana();
        playerUnit.ResetShield();
        playerUnit.SetHUD();
        playerHUD.SetMana(playerUnit);
        foreach (var enemy in enemies)
        {
            enemy.SetMove();
        }
        Debug.Log("Player's Turn!");
    }

    IEnumerator EnemyTurn()
    {
        state = BattleState.ENEMYTURN;
        Debug.Log("Enemy Attacks!");
        foreach (var enemy in enemies)
        {
            if (enemy.gameObject.activeSelf)
            {
                enemy.ResetShield();
                yield return new WaitForSeconds(1f);

                enemy.enemyMoves.UseMove();

                enemy.HealDamage(enemy.enemyMoves.healValue);
                enemy.GainShield(enemy.enemyMoves.shieldValue);
                enemy.SetHUD();
                enemy.AttackAnim();

                playerUnit.TakeDamage(enemy.enemyMoves.damageValue, true);
                playerHUD.SetHP(playerUnit.currentHP);
                playerHUD.SetShield(playerUnit);

                if (enemy.enemyMoves.statusEffects != null)
                {
                    foreach (StackableEffectSO stackEffectData in enemy.enemyMoves.statusEffects)
                    {
                        print(stackEffectData.effectName);
                        playerUnit.AddStatus(stackEffectData);
                    }
                }
                playerHUD.DisplayStatus(playerUnit.stackableEffects);
                yield return new WaitForSeconds(1f);
            }
            if (playerUnit.isDead)

            {
                state = BattleState.LOST;
                EndBattle();
            }
        }
        PlayerTurn();
    }

    private void CheckEnemys()
    {
        foreach (var enemy in enemies)
        {
            if (enemy.isDead)
            {
                enemy.gameObject.SetActive(false);
                enemy.battleHUD.gameObject.SetActive(false);
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

    private void EndBattle()
    {
        StopAllCoroutines();
        if (state == BattleState.WON)
        {
            Debug.Log("You Won!");
            winScreen.GetRandomRewards();
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
        state = BattleState.CASTING;
        StartCoroutine(WandCast1());
    }

    public void OnEndTurnButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        state = BattleState.ENDING;
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
        //if (enemies.Count == 1)
        //{
        //    enemies[0].TargetSelect();
        //    isTarget = true;
        //    yield break;
        //}
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

    IEnumerator PlayerStartCutscene()
    {
        playerAnimator.SetBool("Dashing", true);
        float elapsedTime = 0;
        float animDuration = 1.5f;

        Vector3 playerLeft = playerSpawn.position - new Vector3(10, 0, 0);

        while (elapsedTime <= animDuration)
        {
            playerUnit.gameObject.transform.position = Vector3.Lerp(playerLeft, playerSpawn.transform.position, elapsedTime / animDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        playerUnit.gameObject.transform.position = playerSpawn.transform.position;
        playerAnimator.SetBool("Dashing", false);

    }
}
