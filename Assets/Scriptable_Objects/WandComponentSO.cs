using System.Collections.Generic;
using UnityEngine;

public class WandComponentSO : ScriptableObject
{
    public string componentName;
    public string componentType;
    public int manaCost;
    public int cooldown;
    public Sprite gemImage;
    public string moveInfo; // Format should be " X Damage" and X is replaced by value in calculation
    public List<Sprite> sprites;
}
