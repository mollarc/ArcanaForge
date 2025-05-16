using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Unit
{
    //Get the GameObject’s mesh renderer to access the GameObject’s material and color
    public SpriteRenderer m_Renderer;

    public bool target = false;
    public bool casting = false;

    private bool isHovered = false;

    public EnemyMoves enemyMoves;

    public Image moveImage;

    public SceneFade fade;
    public GameObject targetingImagePrefab;
    public GameObject tempTargetingImage;
    public GameObject wandEffectSpriteLocation;
    public WandObject currentWand;
    GameObject canvas;

    public EnemyMoveTooltip tooltip;

    Vector3 pointA;
    Vector3 pointB;
    Vector3 pointC;

    Vector3 originalScale;

    new void Start()
    {
        base.Start();
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        currentWand = GameObject.FindGameObjectWithTag("Wand").GetComponent<WandObject>();
        tempTargetingImage = Instantiate(targetingImagePrefab);
        originalScale = targetingImagePrefab.transform.localScale;
        //Fetch the mesh renderer component from the GameObject
        m_Renderer = GetComponentInChildren<SpriteRenderer>();
        //Fetch the original color of the GameObject
        pointA = transform.position;
        pointB = transform.position + Vector3.left;
        pointC = transform.position + Vector3.right;
    }

    public override void TakeDamage(int dmg, bool isHit)
    {
        dmgIncomingFlat = 0;
        GetModifiers();
        if (stackableEffects != null)
        {
            List<StackableEffectSO> effectsToRemove = new List<StackableEffectSO>();
            foreach (StackableEffectSO s in stackableEffects)
            {
                if (isHit)
                {
                    switch (s.effectName)
                    {
                        case "Frigid":
                            if (dmg <= 0)
                            {
                                break;
                            }
                            //dmgIncomingFlat += s.amount;
                            s.TickEffect();
                            if (s.amount <= 0)
                            {
                                effectsToRemove.Add(s);
                                battleHUD.RemoveStatus(stackableEffects.IndexOf(s));
                            }
                            break;
                    }
                }
            }
            foreach (StackableEffectSO s in effectsToRemove)
            {
                stackableEffects.Remove(s);
            }
        }
        if (isHit)
        {
            //dmg = (int)Mathf.Round(dmg * dmgIncomingPercent);
            dmg += dmgIncomingFlat;
            if (dmg != 0)
            {
                EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(FmodHitSoundPath);
                if (eventInstance.isValid())
                {
                    print("HitSound");
                    eventInstance.start();
                }
            }
        }
        if (currentShield - dmg < 0)
        {
            dmg -= currentShield;
            currentShield = 0;
        }
        else
        {
            currentShield -= dmg;
            dmg = 0;
        }
        currentHP -= dmg;

        if (currentHP <= 0)
        {
            isDead = true;
        }
        SetHUD();
        if (isDead)
        {
            Destroy(tempTargetingImage);
        }
    }

    public void TakeHailDamage(int dmg)
    {
        dmgIncomingFlat = 0;
        GetModifiers();
        if (stackableEffects != null)
        {
            List<StackableEffectSO> effectsToRemove = new List<StackableEffectSO>();
            foreach (StackableEffectSO s in stackableEffects)
            {
                switch (s.effectName)
                {
                    case "Frigid":
                        if (dmg <= 0)
                        {
                            break;
                        }
                        dmgIncomingFlat += s.amount;
                        s.TickEffect();
                        if (s.amount <= 0)
                        {
                            effectsToRemove.Add(s);
                            battleHUD.RemoveStatus(stackableEffects.IndexOf(s));
                        }
                        break;
                }
            }
            foreach (StackableEffectSO s in effectsToRemove)
            {
                stackableEffects.Remove(s);
            }
        }
        dmg += dmgIncomingFlat;
        if (dmg != 0)
        {
            EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(FmodHitSoundPath);
            if (eventInstance.isValid())
            {
                print("HitSound");
                eventInstance.start();
            }
        }
        if (currentShield - dmg < 0)
        {
            dmg -= currentShield;
            currentShield = 0;
        }
        else
        {
            currentShield -= dmg;
            dmg = 0;
        }
        currentHP -= dmg;

        if (currentHP <= 0)
        {
            isDead = true;
        }
        SetHUD();
        if (isDead)
        {
            Destroy(tempTargetingImage);
        }
    }

    public void AttackAnim()
    {
        StartCoroutine(AttackMoveOverTime());
    }

    public void AttackedAnim()
    {
        StartCoroutine(AttackedMoveOverTime());
    }

    IEnumerator AttackMoveOverTime()
    {
        float elapsedTime = 0;
        while (elapsedTime <= 0.5f)
        {
            transform.position = Vector3.Lerp(pointA, pointB, elapsedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        while (elapsedTime <= 1f)
        {
            transform.position = Vector3.Lerp(pointB, pointA, elapsedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = pointA;
    }

    IEnumerator AttackedMoveOverTime()
    {
        float elapsedTime = 0;
        float animDuration = 0.2f;
        while (elapsedTime <= animDuration)
        {
            transform.position = Vector3.Lerp(pointA, pointC - new Vector3(0.75f, 0, 0), elapsedTime / animDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        elapsedTime = 0;
        while (elapsedTime <= animDuration)
        {
            transform.position = Vector3.Lerp(pointC - new Vector3(0.75f, 0, 0), pointA, elapsedTime / animDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = pointA;
    }

    private void OnMouseOver()
    {
        if (isDead)
        {
            return;
        }
        if (!isHovered)
        {
            currentWand.outsideDMGModifierPercent = dmgIncomingPercent;
            foreach (StackableEffectSO s in stackableEffects)
            {
                switch (s.effectName)
                {
                    case "Frigid":
                        currentWand.outsideEffectDMG = s.amount;
                        break;
                }
            }
        }
        currentWand.CalculateValues();
        isHovered = true;
        Image image = tempTargetingImage.GetComponent<Image>();
        //first you need the RectTransform component of your canvas
        RectTransform CanvasRect = canvas.GetComponent<RectTransform>();
        tempTargetingImage.transform.SetParent(canvas.transform);
        tempTargetingImage.transform.localScale = originalScale;
        //then you calculate the position of the UI element
        //0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this, you need to subtract the height / width of the canvas * 0.5 to get the correct position.

        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(gameObject.transform.position);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)) - (ViewportPosition.y * CanvasRect.sizeDelta.y * 0.625f));

        //now you can set the position of the ui element
        image.rectTransform.anchoredPosition = WorldObject_ScreenPosition;

    }

    private void OnMouseExit()
    {
        if (isHovered)
        {
            currentWand.outsideDMGModifierPercent = 1f;
            currentWand.outsideDMGModifierFlat = 0;
            currentWand.outsideEffectDMG = 0;
            currentWand.CalculateValues();
            isHovered = false;
        }
        if (target)
        {
            return;
        }
        tempTargetingImage.transform.SetParent(gameObject.transform);
        tempTargetingImage.transform.localScale = originalScale;
    }

    public void TargetSelect()
    {
        target = true;
        tempTargetingImage.transform.SetParent(canvas.transform);
        tempTargetingImage.transform.localScale = originalScale;
    }

    public void TargetDeselect()
    {
        target = false;
        tempTargetingImage.transform.SetParent(gameObject.transform);
        tempTargetingImage.transform.localScale = originalScale;
    }

    public void SetMove()
    {
        StartCoroutine(fade.FadeOutEffect());
        GetModifiers();
        enemyMoves.modifierValue = 1f;
        enemyMoves.modifierValueFlat = 0;
        enemyMoves.modifierValue += dmgOutgoingPercent;
        enemyMoves.modifierValueFlat += dmgOutgoingFlat;
        tooltip.tooltipDescription = enemyMoves.LoadMove();
        if (enemyMoves.currentMove.statusEffects != null)
        {
            foreach (StackableEffectSO s in enemyMoves.currentMove.statusEffects)
            {
                string effect = s.effectName + ": " + s.effectTooltip;
                tooltip.GetEffectTooltip(effect);
            }
        }
        moveImage.sprite = enemyMoves.moveImage;
        tooltip.tooltipSprite = m_Renderer.sprite;
        tooltip.tooltipName = enemyMoves.moveName;
    }

    public void RefreshMove()
    {
        GetModifiers();
        enemyMoves.modifierValue = 1f;
        enemyMoves.modifierValueFlat = 0;
        enemyMoves.modifierValue += dmgOutgoingPercent;
        enemyMoves.modifierValueFlat += dmgOutgoingFlat;
        tooltip.tooltipDescription = enemyMoves.RefreshMoveInfo();
        if (enemyMoves.currentMove.statusEffects != null)
        {
            foreach (StackableEffectSO s in enemyMoves.currentMove.statusEffects)
            {
                tooltip.GetEffectTooltip(s.effectName + ": " + s.effectTooltip);
            }
        }
        moveImage.sprite = enemyMoves.moveImage;
        tooltip.tooltipSprite = m_Renderer.sprite;
        tooltip.tooltipName = enemyMoves.moveName;
    }
}
