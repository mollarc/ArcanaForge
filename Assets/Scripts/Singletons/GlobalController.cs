using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static GlobalController Instance;

    public PlayerStatistics savedStatistics = new PlayerStatistics();

    public int fightsWon;
    public bool hasntSaved = true;

    public WandObject[] wands = new WandObject[2];
    public List<WandComponentSO> gemInventory;
    List<WandComponentSO> startingGemInventory = new List<WandComponentSO>();

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

    private void Start()
    {
        if(Player.playerInstance != null && !hasntSaved)
        {
            Player.playerInstance.SavePlayer();
        }
        startingGemInventory.AddRange(this.gemInventory);
    }

    public void Restart()
    {
        this.hasntSaved = true;
        gemInventory.Clear();
        gemInventory.AddRange(this.startingGemInventory);
        fightsWon = 0;
    }
}
