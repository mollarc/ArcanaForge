using System.Collections.Generic;
using UnityEngine;

public class EnemyMoves : MonoBehaviour
{
    private int healValue;
    private int damageValue;
    private int shieldValue;
    private int modifierValue;
    private int currentMoveIndex;

    private TempEnemyMove currentMove;
    
    public Sprite moveImage;
    public string moveName;

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
        statusEffects.Clear();
        healValue = currentMove.healValue * modifierValue;
        damageValue = currentMove.damageValue * modifierValue;
        shieldValue = currentMove.shieldValue * modifierValue;
        if (currentMove.statusEffects != null)
        {
            foreach(StackableEffectSO statusEffectData in currentMove.statusEffects)
            {
                StackableEffectSO stackableClone = Instantiate(statusEffectData);
                statusEffects.Add(stackableClone);
            }
        }
    }

    public string LoadMove()
    {
        string stringToReturn = "";
        currentMoveIndex = Random.Range(0, enemyMoves.Count);
        currentMove = enemyMoves[currentMoveIndex];
        CalculateValues();
        moveImage = currentMove.moveImage;
        moveName = currentMove.moveName;
        if(statusEffects != null)
        {
            foreach(StackableEffectSO effectSO in statusEffects)
            {
                stringToReturn += effectSO.amount + " " + effectSO.effectName + " ";
            }
        }
        if (enemyMoves[currentMoveIndex].type ==0)
        {
            return stringToReturn += damageValue.ToString() + " DMG";
        }
        else if (enemyMoves[currentMoveIndex].type == 1)
        {
            return stringToReturn += shieldValue.ToString() + " SHLD";
        }
        else if (enemyMoves[currentMoveIndex].type == 2)
        {
            return stringToReturn += healValue.ToString() + " HEAL";
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
