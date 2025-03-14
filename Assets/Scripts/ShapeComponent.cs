using UnityEngine;

public class ShapeComponent : WandComponent
{
    public ShapeComponentSO shapeComponentData;
    public int numberOfCasts;
    public int targets; //Int Values refer to "None,Single,Blast,All" in order starting from 0. Highest value takes priority.
    public bool isRandom;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.SetUP();
        componentName = shapeComponentData.name;
        componentType = shapeComponentData.componentType;
        manaCost = shapeComponentData.manaCost;
        cooldown = shapeComponentData.cooldown;
        numberOfCasts = shapeComponentData.numberOfCasts;
        isRandom = shapeComponentData.isRandom;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
