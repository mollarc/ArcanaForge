using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropDown : MonoBehaviour
{
    public Animator playerAnimator;
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerAnimator = player.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClickHandler(BaseEventData eventData)
    {
        print("Mouse down on dropdown");
        playerAnimator.SetBool("isSelecting", true);
    }

    public void ComponentChange()
    {
        playerAnimator.SetBool("Swapped", true);
        playerAnimator.SetBool("isSelecting", false);
        StartCoroutine(ChangeSwapped());

    }

    public IEnumerator ChangeSwapped()
    {
        yield return new WaitForSeconds(0.25f);
        playerAnimator.SetBool("Swapped", false);
    }
}
