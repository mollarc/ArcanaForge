using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using WandComponentNS;

public class RewardDisplay : MonoBehaviour
{
    public List<WandComponent> rewards;
    public GameObject typeComponent;
    public GameObject modifierComponent;
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
        print(data.componentName);
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
            reward.image.enabled = false;
            rewards.Add(reward);
        }
        else if (data.componentType == componentType.Modifier)
        {
            var tempList1 = new List<WandComponentSO>();
            tempList1.Add(data);
            var templist2 = tempList1.Cast<ModifierComponentSO>().ToArray();
            var newModComponent = Instantiate(modifierComponent);
            newModComponent.GetComponent<ModifierComponent>().LoadComponentData(templist2[0]);
            //rewards.Add(newModComponent);
        }
    }
}
