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

    protected override void DestroyDropdownList(GameObject dropdownList)
    {
        base.DestroyDropdownList(dropdownList);
        playerAnimator.SetBool("isSelecting", false);
    }
}
