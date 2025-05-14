using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using WandComponentNS;

public class RewardDisplay : MonoBehaviour
{
    public List<WandComponent> rewards;
    public GameObject typeComponent;
    public GameObject modifierComponent;

    int index = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddReward(WandComponentSO data)
    {
        if (data.componentType == componentType.Type)
        {
            var tempList1 = new List<WandComponentSO>
            {
                data
            };
            var templist2 = tempList1.Cast<TypeComponentSO>().ToArray();
            var rewardGO = Instantiate(typeComponent, gameObject.transform);
            TypeComponent reward = rewardGO.GetComponent<TypeComponent>();
            reward.LoadComponentData(templist2[0]);
            reward.tooltip.itemInspector = gameObject.GetComponent<ItemInspector>();
            reward.UpdateTooltip();
            reward.tooltip.itemInspector.LoadTooltipData(reward.tooltip);
            Destroy(rewardGO.GetComponent<ImageFlipAnimation>());
            Destroy(rewardGO.GetComponent<ItemDragHandler>());
            Destroy(rewardGO.GetComponent<Image>());
            rewardGO.transform.localScale = Vector3.zero;
            rewards.Add(reward);
        }
        else if (data.componentType == componentType.Modifier)
        {
            var tempList1 = new List<WandComponentSO>
            {
                data
            };
            var templist2 = tempList1.Cast<ModifierComponentSO>().ToArray();
            var rewardGO = Instantiate(modifierComponent, gameObject.transform);
            ModifierComponent reward = rewardGO.GetComponent<ModifierComponent>();
            reward.LoadComponentData(templist2[0]);
            reward.tooltip.itemInspector = gameObject.GetComponent<ItemInspector>();
            reward.UpdateTooltip();
            reward.tooltip.itemInspector.LoadTooltipData(reward.tooltip);
            Destroy(rewardGO.GetComponent<ImageFlipAnimation>());
            Destroy(rewardGO.GetComponent<ItemDragHandler>());
            Destroy(rewardGO.GetComponent<Image>());
            rewardGO.transform.localScale = Vector3.zero;
            rewards.Add(reward);
        }
        DisplayReward();
    }

    private void DisplayReward()
    {
        rewards[index].tooltip.itemInspector.LoadTooltipData(rewards[index].tooltip);
    }
    public void CycleLeft()
    {
        if (index == 0)
        {
            index = rewards.Count - 1;
        }
        else
        {
            index--;
        }
        DisplayReward();
    }

    public void CycleRight()
    {
        if (index == rewards.Count-1)
        {
            index = 0;
        }
        else
        {
            index++;
        }
        DisplayReward();
    }
}
