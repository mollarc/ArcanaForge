using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WandComponentNS;

public class BattleWin : MonoBehaviour
{
    public GameObject typeComponent;
    public GameObject modifierComponent;
    public List<WandComponentSO> reward1 = new List<WandComponentSO>();
    public List<WandComponentSO> reward2 = new List<WandComponentSO>();
    public List<WandComponentSO> reward3 = new List<WandComponentSO>();

    int index1 = 0;
    int index2 = 0;
    int index3 = 0;

    public RewardDisplay display1;
    public RewardDisplay display2;
    public RewardDisplay display3;

    public ItemInspector itemInspector1;
    public ItemInspector itemInspector2;
    public ItemInspector itemInspector3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetRandomRewards()
    {
        gameObject.SetActive(true);
        WandComponentSO component = reward1[index1];
        display1.AddReward(component);
    }

    public void GetReward1()
    {

    }
    public void GetReward2()
    {

    }
    public void GetReward3()
    {

    }
}
