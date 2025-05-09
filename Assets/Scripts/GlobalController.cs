using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static GlobalController Instance;

    public PlayerStatistics savedStatistics = new PlayerStatistics();

    public WandObject wand1;
    public WandObject wand2;

    public List<WandComponent> gemInventory;

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
}
