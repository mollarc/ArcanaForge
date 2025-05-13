using Mono.Cecil;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Unit
{
    //Get the GameObject’s mesh renderer to access the GameObject’s material and color
    public SpriteRenderer m_Renderer;

    public bool target = false;

    public bool casting = false;

    public EnemyMoves enemyMoves;

    public Image moveImage;

    public SceneFade fade;
    public GameObject targetingImagePrefab;
    public GameObject tempTargetingImage;
    GameObject canvas;

    public Tooltip tooltip;

    Vector3 pointA;
    Vector3 pointB;
    Vector3 pointC;

    Vector3 originalScale;

    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        tempTargetingImage = Instantiate(targetingImagePrefab);
        originalScale = targetingImagePrefab.transform.localScale;
        //Fetch the mesh renderer component from the GameObject
        m_Renderer = GetComponentInChildren<SpriteRenderer>();
        //Fetch the original color of the GameObject
        pointA = transform.position;
        pointB = transform.position + Vector3.left;
        pointC = transform.position + Vector3.right;
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
        while(elapsedTime <= 1f)
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
            transform.position = Vector3.Lerp(pointA, pointC- new Vector3(0.75f, 0, 0), elapsedTime/animDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        elapsedTime = 0;
        while (elapsedTime <= animDuration)
        {
            transform.position = Vector3.Lerp(pointC - new Vector3(0.75f, 0, 0), pointA, elapsedTime/animDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = pointA;
    }

    public new void TakeDamage(int dmg, bool i)
    {
        base.TakeDamage(dmg,i);
        if (isDead)
        {
            Destroy(tempTargetingImage);
        }
    }

    private void OnMouseOver()
    {
        if (isDead)
        {
            return;
        }
        //this is your object that you want to have the UI element hovering over
        

        //this is the ui element
        
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
        ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f))-(ViewportPosition.y * CanvasRect.sizeDelta.y*0.625f));

        //now you can set the position of the ui element
        image.rectTransform.anchoredPosition = WorldObject_ScreenPosition;
        
    }

    private void OnMouseExit()
    {
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
        tooltip.tooltipDescription = enemyMoves.LoadMove();
        moveImage.sprite = enemyMoves.moveImage;
        tooltip.tooltipSprite = m_Renderer.sprite;
        tooltip.tooltipName = enemyMoves.moveName;
    }
}
