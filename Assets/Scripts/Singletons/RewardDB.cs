using System.Collections.Generic;
using UnityEngine;

public class RewardDB : MonoBehaviour
{
    public static RewardDB Instance;

    int currentIndex =-1;

    public List<RewardSO> rewards;
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public RewardSO GetSequentialReward()
    {
        if (currentIndex == 0)
        {
            currentIndex++;
            return rewards[currentIndex];
        }
        currentIndex++;
        return rewards[currentIndex];
    }
}
