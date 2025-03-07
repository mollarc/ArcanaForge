using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyMoves : MonoBehaviour
{
    private int healValue;
    private int damageValue;
    private int shieldValue;
    private int modifierValue;
    private int currentMoveIndex;

    private TempEnemyMove currentMove;

    public List<TempEnemyMove> enemyMoves;
    public List<TempEnemyMove> usedMoves;

    public List<StackableEffectSO> statusEffects;

    public int HealValue { get => healValue; set => healValue = value; }
    public int DamageValue { get => damageValue; set => damageValue = value; }
    public int ShieldValue { get => shieldValue; set => shieldValue = value; }
    public int ModifierValue { get => modifierValue; set => modifierValue = value; }

    public void Start()
    {
        modifierValue = 1;
    }

    public void CalculateValues()
    {
        healValue = currentMove.healValue * modifierValue;
        damageValue = currentMove.damageValue * modifierValue;
        shieldValue = currentMove.shieldValue * modifierValue;
        if (currentMove.statusEffects != null)
        {
            foreach(StackableEffectSO statusEffect in currentMove.statusEffects)
            {
                statusEffects.Add(statusEffect);
            }
        }
    }

    public string LoadMove()
    {
        currentMoveIndex = Random.Range(0, enemyMoves.Count);
        currentMove = enemyMoves[currentMoveIndex];
        CalculateValues();
        if(statusEffects != null)
        {
            foreach(StackableEffectSO effectSO in statusEffects)
            {
                //Show Enemy Intent
            }
        }
        if (enemyMoves[currentMoveIndex].type ==0)
        {
            return damageValue.ToString() + " DMG";
        }
        else if (enemyMoves[currentMoveIndex].type == 1)
        {
            return shieldValue.ToString() + " SHLD";
        }
        else if (enemyMoves[currentMoveIndex].type == 2)
        {
            return healValue.ToString() + " HEAL";
        }
        else
        {
            return null;
        }
    }

    public void UseMove()
    {
        usedMoves.Add(enemyMoves[currentMoveIndex]);
        enemyMoves.Remove(enemyMoves[currentMoveIndex]);
        if (enemyMoves.Count == 0)
        {
            foreach(var _usedMove in usedMoves)
            {
                enemyMoves.Add(_usedMove);
            }
            usedMoves.Clear();
        }
    }
}
