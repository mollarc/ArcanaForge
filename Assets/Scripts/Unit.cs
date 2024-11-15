using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public string unitName;
    public int maxHP;
    public int currentHP;
    public int currentShield;

    public bool TakeDamage(int dmg)
    {
        dmg -= currentShield;
        currentHP -= dmg;

        if (currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void HealDamage(int healing)
    {
        if (maxHP - currentHP <= healing)
        {
            currentHP = maxHP;
        }
        else
        {
            currentHP += healing;
        }
    }

    public void GainShield(int shield)
    {
        currentShield += shield;
    }

    public void ResetShield()
    {
        currentShield = 0;
    }
}
