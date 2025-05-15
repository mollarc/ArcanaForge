using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInspector : MonoBehaviour
{
    public TMP_Text itemName;
    public TMP_Text itemDescription;
    public TMP_Text manaValue;
    public TMP_Text cooldownValue;
    public TMP_Text usesValue;
    public Image itemImage;
    public Sprite emptyImage;
    public GridLayoutGroup grid;
    public GameObject effectTooltipPrefab;

    public List<GameObject> effectTooltipsGO;
    public List<string> effectDescriptions;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadTooltipData(Tooltip tooltip)
    {
        itemName.text = tooltip.tooltipName;
        itemDescription.text = tooltip.tooltipDescription;
        manaValue.text = tooltip.tooltipMana;
        cooldownValue.text = tooltip.tooltipCooldown;
        usesValue.text = tooltip.tooltipUses;
        itemImage.sprite = tooltip.tooltipSprite;
    }

    public void ClearInspector()
    {
        itemName.text = "";
        itemDescription.text = "";
        manaValue.text = "";
        cooldownValue.text = "";
        usesValue.text = "";
        itemImage.sprite = emptyImage;
        effectDescriptions.Clear();
    }

    public void LoadEffectTooltips()
    {
        if (effectDescriptions != null)
        {
            foreach (string s in effectDescriptions)
            {
                GameObject effectTooltip = Instantiate(effectTooltipPrefab, grid.GetComponentInChildren<GridLayoutGroup>().gameObject.transform);
                effectTooltip.GetComponentInChildren<TMP_Text>().text = s;
                effectTooltipsGO.Add(effectTooltip);
            }
        }
    }

    public void ClearEffectTooltips()
    {
        for(int i = effectTooltipsGO.Count - 1; i >= 0; i--)
        {
            Destroy(effectTooltipsGO[i]);
            effectTooltipsGO.RemoveAt(i);
        }
    }
}
