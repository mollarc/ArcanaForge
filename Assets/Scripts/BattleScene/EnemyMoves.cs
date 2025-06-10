using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyMoves : MonoBehaviour
{
    public int castAmount;
    public int healValue;
    public int damageValue;
    public int shieldValue;
    public float modifierValue;
    public int modifierValueFlat;
    public int currentMoveIndex;

    public TMP_Text moveValue;

    public TempEnemyMove currentMove;

    public Sprite moveImage;
    public string moveName;

    public List<TempEnemyMove> enemyMoves;
    public List<TempEnemyMove> usedMoves;

    public List<StackableEffectSO> cloneDebuffEffects;
    public List<StackableEffectSO> cloneBuffEffects;

    public void Start()
    {
        modifierValue = 1f;
    }

    public void CalculateValues()
    {
        cloneDebuffEffects.Clear();
        cloneBuffEffects.Clear();
        healValue = 0;
        damageValue = 0;
        shieldValue = 0;
        Player.playerInstance.GetModifiers();
        castAmount = currentMove.castAmount;
        healValue = (int)Mathf.Round(currentMove.healValue);
        if (currentMove.damageValue > 0)
        {
            damageValue = (int)Mathf.Round(((currentMove.damageValue * modifierValue) + modifierValueFlat) * Player.playerInstance.dmgIncomingPercent);
        }
        shieldValue = (int)Mathf.Round(currentMove.shieldValue);
        if (currentMove.statusEffects != null)
        {
            foreach (StackableEffectSO statusEffectData in currentMove.statusEffects)
            {
                StackableEffectSO stackableClone = Instantiate(statusEffectData);
                if (stackableClone.type == 0)
                {
                    cloneDebuffEffects.Add(stackableClone);
                }
                else if (stackableClone.type == 1)
                {
                    cloneBuffEffects.Add(stackableClone);
                }
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
        if (cloneDebuffEffects != null)
        {
            foreach (StackableEffectSO effectSO in cloneDebuffEffects)
            {
                stringToReturn += effectSO.amount + " " + effectSO.effectData.effectName + " ";
            }
        }
        if (cloneBuffEffects != null)
        {
            foreach (StackableEffectSO effectSO in cloneBuffEffects)
            {
                stringToReturn += effectSO.amount + " " + effectSO.effectData.effectName + " ";
            }
        }
        if (enemyMoves[currentMoveIndex].type == 0)
        {
            moveValue.text = damageValue.ToString();
            stringToReturn += damageValue.ToString() + " Damage";
        }
        else if (enemyMoves[currentMoveIndex].type == 1)
        {
            moveValue.text = shieldValue.ToString();
            stringToReturn += shieldValue.ToString() + " Shield";
        }
        else if (enemyMoves[currentMoveIndex].type == 2)
        {
            moveValue.text = healValue.ToString();
            stringToReturn += healValue.ToString() + " Heal";
        }
        else if (enemyMoves[currentMoveIndex].type == 3)
        {
            moveValue.text = damageValue.ToString();
            stringToReturn += damageValue.ToString() + " Damage ";
            stringToReturn += shieldValue.ToString() + " Shield";
        }
        else
        {
            moveValue.text = "";
        }
        if (castAmount > 1)
        {
            moveValue.text += "x" + castAmount;
            stringToReturn += " " + castAmount + " times";
        }
        return stringToReturn;
    }

    public string RefreshMoveInfo()
    {
        string stringToReturn = "";
        CalculateValues();
        moveImage = currentMove.moveImage;
        moveName = currentMove.moveName;
        if (cloneDebuffEffects != null)
        {
            foreach (StackableEffectSO effectSO in cloneDebuffEffects)
            {
                stringToReturn += effectSO.amount + " " + effectSO.effectData.effectName + " ";
            }
        }
        if (cloneBuffEffects != null)
        {
            foreach (StackableEffectSO effectSO in cloneBuffEffects)
            {
                stringToReturn += effectSO.amount + " " + effectSO.effectData.effectName + " ";
            }
        }
        if (enemyMoves[currentMoveIndex].type == 0)
        {
            moveValue.text = damageValue.ToString();
            stringToReturn += damageValue.ToString() + " Damage";
        }
        else if (enemyMoves[currentMoveIndex].type == 1)
        {
            moveValue.text = shieldValue.ToString();
            stringToReturn += shieldValue.ToString() + " Shield";
        }
        else if (enemyMoves[currentMoveIndex].type == 2)
        {
            moveValue.text = healValue.ToString();
            stringToReturn += healValue.ToString() + " Heal";
        }
        else if (enemyMoves[currentMoveIndex].type == 3)
        {
            moveValue.text = damageValue.ToString();
            stringToReturn += damageValue.ToString() + " Damage ";
            stringToReturn += shieldValue.ToString() + " Shield";
        }
        else
        {
            moveValue.text = "";
        }
        if (castAmount > 1)
        {
            moveValue.text += "x" + castAmount;
            stringToReturn += " " + castAmount + " times";
        }
        return stringToReturn;
    }

    public void UseMove()
    {
        usedMoves.Add(enemyMoves[currentMoveIndex]);
        enemyMoves.Remove(enemyMoves[currentMoveIndex]);
        if (enemyMoves.Count == 0)
        {
            foreach (var _usedMove in usedMoves)
            {
                enemyMoves.Add(_usedMove);
            }
            usedMoves.Clear();
        }
    }
}
