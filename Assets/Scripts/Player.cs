using UnityEngine;

public class Player : Unit
{
    public int maxMana;
    public int currentMana;
    public PlayerStatistics currentStatistics = new PlayerStatistics();

    public static Player playerInstance;
    public bool ManaChange(int manaChange, bool casting)
    {
        if (currentMana + manaChange > maxMana)
        {
            currentMana = maxMana;
            return false;
        }
        else if (currentMana + manaChange < 0)
        {
            if (casting)
            {
                return true;
            }
            currentMana = 0;
            return true;
        }
        else
        {
            currentMana += manaChange;
            return false;
        }
    }
    public void SavePlayer()
    {
        currentStatistics.currentHP = currentHP;
        currentStatistics.maxHP = maxHP;
        GlobalController.Instance.savedStatistics = currentStatistics;
    }
}
