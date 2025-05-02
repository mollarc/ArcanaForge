using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TypeComponentNS;

public class TypeComponent : WandComponent
{
    public TypeComponentSO typeComponentData;
    public int healValue;
    public int damageValue;
    public int shieldValue;
    public int targets; //Int Values refer to "None,Single,Blast,All" in order starting from 0. Highest value takes priority.
    public List<moves> moveInfos;
    public string FmodEventPath;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.SetUP();
        if(typeComponentData != null)
        {
            LoadComponentData(typeComponentData);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public new void LoadComponentData(TypeComponentSO componentData)
    {
        base.LoadComponentData(componentData);
        healValue = componentData.healValue;
        damageValue = componentData.damageValue;    
        shieldValue = componentData.shieldValue;
        targets = componentData.targets;
        FmodEventPath = componentData.FmodEventPath;
        foreach(moves move in componentData.moveInfos)
        {
            moveInfos.Add(move);
        }
        
    }
}
