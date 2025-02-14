using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestDropDownOverride : TMP_Dropdown
{
    public Animator playerAnimator;
    public GameObject player;
    protected override void Start()
    {
        base.Start();
        player = GameObject.FindWithTag("Player");
        playerAnimator = player.GetComponentInChildren<Animator>();
    }
    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);

    }

    public override void OnDeselect(BaseEventData eventData)
    {
        print("Deselect");
        base.OnDeselect(eventData);
        playerAnimator.SetBool("isSelecting", false);
    }
}
