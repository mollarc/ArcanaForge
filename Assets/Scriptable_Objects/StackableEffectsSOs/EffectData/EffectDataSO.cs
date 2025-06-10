using UnityEngine;

[CreateAssetMenu(fileName = "EffectDataSO", menuName = "Scriptable Objects/EffectDataSO")]
public class EffectDataSO : ScriptableObject
{
    public string effectName;
    public Sprite effectIcon;
    public string effectTooltip;
    public string effectColor;
}
