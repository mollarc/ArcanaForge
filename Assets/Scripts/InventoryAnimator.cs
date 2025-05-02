using UnityEngine;
using UnityEngine.UI;

public class InventoryAnimator : MonoBehaviour
{
    public HorizontalLayoutGroup horizontalLayoutGroup;
    RectTransform rect;
    public GameObject scrollbarAreaObj;
    public Image scrollbarImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rect = GetComponent<RectTransform>();
        scrollbarAreaObj.SetActive(false);
        scrollbarImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        RectOffset tempPadding = new RectOffset(
            horizontalLayoutGroup.padding.left,
            horizontalLayoutGroup.padding.right,
            horizontalLayoutGroup.padding.top,
            horizontalLayoutGroup.padding.bottom
            );
        if (tempPadding.left < 0)
        {
            tempPadding.left += 2;
            horizontalLayoutGroup.padding = tempPadding;
            LayoutRebuilder.MarkLayoutForRebuild(rect);
        }
        else if(scrollbarAreaObj.activeSelf == false)
        {
            scrollbarAreaObj.SetActive(true);
            scrollbarImage.enabled=true;
        }
    }
}
