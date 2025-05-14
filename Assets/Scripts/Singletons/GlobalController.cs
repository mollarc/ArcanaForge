using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static GlobalController Instance;

    public PlayerStatistics savedStatistics = new PlayerStatistics();

    public WandObject[] wands = new WandObject[2];
    public List<WandComponentSO> gemInventory;

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
