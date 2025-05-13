using UnityEngine;
using WandComponentNS;

[CreateAssetMenu(fileName = "WandObject", menuName = "Scriptable Objects/WandObject")]
public class WandObjectSO : ScriptableObject
{
    public string wandName;
    public int typeSlots;
    public int modifierSlots;
    public int shapeSlots;
}
