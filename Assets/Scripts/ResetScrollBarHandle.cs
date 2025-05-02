
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Scrollbar))]
public class FIX_Scrollbar : MonoBehaviour
{
    //public GameObject scrollbar;

    public void setFixedHandleSize()
    {
        GetComponent<Scrollbar>().size = 0;
    }
}