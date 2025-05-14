using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyMoves : MonoBehaviour
{
    public int healValue;
    public int damageValue;
    public int shieldValue;
    public float modifierValue;
    public int modifierValueFlat;
    public int currentMoveIndex;

    public TMP_Text moveValue;

    private TempEnemyMove currentMove;
    
    public Sprite moveImage;
    public string moveName;

    public List<TempEnemyMove> enemyMoves;
    public List<TempEnemyMove> usedMoves;

    public List<StackableEffectSO> statusEffects;

    public void Start()
    {
        modifierValue = 1f;
        print(modifierValue);
    }

    public void CalculateValues()
    {
        statusEffects.Clear();
        print(currentMove.damageValue);
        print(Mathf.Round(currentMove.damageValue * modifierValue));
        healValue = (int)Mathf.Round(currentMove.healValue);
        damageValue = (int)Mathf.Round(currentMove.damageValue * modifierValue) + modifierValueFlat;
        shieldValue = (int)Mathf.Round(currentMove.shieldValue);
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
            moveValue.text = damageValue.ToString();
            return stringToReturn += damageValue.ToString() + " DMG";
        }
        else if (enemyMoves[currentMoveIndex].type == 1)
        {
            moveValue.text = shieldValue.ToString();
            return stringToReturn += shieldValue.ToString() + " SHLD";
        }
        else if (enemyMoves[currentMoveIndex].type == 2)
        {
            moveValue.text = healValue.ToString();
            return stringToReturn += healValue.ToString() + " HEAL";
        }
        else
        {
            return null;
        }
    }

    public string RefreshMoveInfo()
    {
        string stringToReturn = "";
        CalculateValues();
        moveImage = currentMove.moveImage;
        moveName = currentMove.moveName;
        if (statusEffects != null)
        {
            foreach (StackableEffectSO effectSO in statusEffects)
            {
                stringToReturn += effectSO.amount + " " + effectSO.effectName + " ";
            }
        }
        if (enemyMoves[currentMoveIndex].type == 0)
        {
            moveValue.text = damageValue.ToString();
            return stringToReturn += damageValue.ToString() + " DMG";
        }
        else if (enemyMoves[currentMoveIndex].type == 1)
        {
            moveValue.text = shieldValue.ToString();
            return stringToReturn += shieldValue.ToString() + " SHLD";
        }
        else if (enemyMoves[currentMoveIndex].type == 2)
        {
            moveValue.text = healValue.ToString();
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
