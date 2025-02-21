using UnityEngine;

[CreateAssetMenu(fileName = "ShapeComponent", menuName = "Scriptable Objects/ShapeComponent")]
public class ShapeComponent : ScriptableObject
{
    public string componentName;
    public int numberOfCasts;
    public int targets; //Int Values refer to "None,Single,Blast,All" in order starting from 0. Highest value takes priority.
    public bool isRandom;
}
