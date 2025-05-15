using System.Collections.Generic;
using UnityEngine;
using TypeComponentNS;

[CreateAssetMenu(fileName = "TypeComponent", menuName = "Scriptable Objects/TypeComponent")]
public class TypeComponentSO : WandComponentSO
{
    public int healValue;
    public int damageValue;
    public int shieldValue;
    public int targets; //Int Values refer to "None,Single,Blast,All" in order starting from 0. Highest value takes priority.
    public List<moves> moveInfos;
    public string FmodEventPath;
    public List<string> FmodEventPaths;
}
