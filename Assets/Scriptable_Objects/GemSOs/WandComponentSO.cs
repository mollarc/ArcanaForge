using System.Collections.Generic;
using UnityEngine;
using WandComponentNS;

public class WandComponentSO : ScriptableObject
{
    public string componentName;
    public componentType componentType;
    public int manaCost;
    public int cooldown;
    public int uses; //Negative value is infinite uses
    public Sprite gemImage;
    public string moveInfo; // Format should be " X Damage" and X is replaced by value in calculation
    public List<Sprite> sprites;
   
}
