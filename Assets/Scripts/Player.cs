using UnityEngine;

public class Player : Unit
{
    public int maxMana;
    public int currentMana;
    
    public bool ManaChange(int manaChange)
    {
        if (currentMana + manaChange > maxMana)
        {
            currentMana = maxMana;
            return false;
        }
        else if (currentMana + manaChange < 0)
        {
            currentMana = 0;
            return true;
        }
        else
        {
            currentMana += manaChange;
            return false;
        }
    }
}
