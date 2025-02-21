using System.Collections.Generic;
using UnityEngine;

public class EnemyMoves : MonoBehaviour
{
    private int healValue;
    private int damageValue;
    private int shieldValue;

    public List<WandComponent> enemyMoves;
    public List<WandComponent> usedMoves;

    public int HealValue { get => healValue; set => healValue = value; }
    public int DamageValue { get => damageValue; set => damageValue = value; }
    public int ShieldValue { get => shieldValue; set => shieldValue = value; }

    public void UseMove()
    {
        int move = Random.Range(0, enemyMoves.Count);
        //healValue = enemyMoves[move].healValue;
        //damageValue = enemyMoves[move].damageValue;
        //shieldValue = enemyMoves[move].shieldValue;
        usedMoves.Add(enemyMoves[move]);
        enemyMoves.Remove(enemyMoves[move]);
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
