using UnityEngine;
using ModifierComponentNS;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "ModifierComponent", menuName = "Scriptable Objects/ModifierComponent")]
public class ModifierComponentSO : WandComponentSO
{
    public float modifierValue;//Write percentages as decimals
    public int numberOfCasts;
    public int targets; //Int Values refer to "None,Single,Blast,All" in order starting from 0. Highest value takes priority.
    public List<moves> moveInfos;
}
    