using UnityEngine;

[CreateAssetMenu(fileName = "WandComponentSO", menuName = "Scriptable Objects/WandComponentSO")]
public class WandComponentSO : ScriptableObject
{
    public string componentName;
    public string componentType;
    public Sprite gemImage;
}
