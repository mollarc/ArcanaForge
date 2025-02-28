using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyMoves : MonoBehaviour
{
    private int healValue;
    private int damageValue;
    private int shieldValue;
    private int currentMoveIndex;

    public List<TempEnemyMove> enemyMoves;
    public List<TempEnemyMove> usedMoves;

    public int HealValue { get => healValue; set => healValue = value; }
    public int DamageValue { get => damageValue; set => damageValue = value; }
    public int ShieldValue { get => shieldValue; set => shieldValue = value; }

    public void Start()
    {
        
    }

    public string LoadMove()
    {
        currentMoveIndex = Random.Range(0, enemyMoves.Count);
        if (enemyMoves[currentMoveIndex].type ==0)
        {
            return enemyMoves[currentMoveIndex].damageValue.ToString() + " DMG";
        }
        else if (enemyMoves[currentMoveIndex].type == 1)
        {
            return enemyMoves[currentMoveIndex].shieldValue.ToString() + " SHLD";
        }
        else if (enemyMoves[currentMoveIndex].type == 2)
        {
            return enemyMoves[currentMoveIndex].healValue.ToString() + " HEAL";
        }
        else
        {
            return null;
        }
    }

    public void UseMove()
    {
        healValue = enemyMoves[currentMoveIndex].healValue;
        damageValue = enemyMoves[currentMoveIndex].damageValue;
        shieldValue = enemyMoves[currentMoveIndex].shieldValue;
        usedMoves.Add(enemyMoves[currentMoveIndex]);
        enemyMoves.Remove(enemyMoves[currentMoveIndex]);
        print(usedMoves);
        if (enemyMoves.Count == 0)
        {
            print("Shuffle");
            foreach(var _usedMove in usedMoves)
            {
                enemyMoves.Add(_usedMove);
            }
            usedMoves.Clear();
        }
    }
}
