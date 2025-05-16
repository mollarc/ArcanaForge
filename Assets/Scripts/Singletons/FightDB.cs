using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class FightDB : MonoBehaviour
{
    public static FightDB Instance;

    public int currentIndex =-1;

    public List<FightsDataSO> fightList = new List<FightsDataSO>();

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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public FightsDataSO GetSequentialFight()
    {
        currentIndex++;
        return fightList[currentIndex];
    }

    public FightsDataSO PickRandomFight()
    {
        int i = Random.Range(0, fightList.Count);

        return fightList[i];
    }
}
