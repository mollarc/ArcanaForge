using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Unit
{
    //When the mouse hovers over the GameObject, it turns to this color (red)
    Color m_MouseOverColor = Color.red;

    //This stores the GameObject’s original color
    Color m_OriginalColor;

    //Get the GameObject’s mesh renderer to access the GameObject’s material and color
    public SpriteRenderer m_Renderer;

    public bool target = false;

    public bool casting = false;

    public BattleHUD enemyHUD;

    public EnemyMoves enemyMoves;

    public Image moveImage;

    public GameObject targetingImagePrefab;
    public GameObject tempTargetingImage;
    GameObject canvas;

    public Tooltip tooltip;

    Vector3 pointA;
    Vector3 pointB;

    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        tempTargetingImage = Instantiate(targetingImagePrefab);
        //Fetch the mesh renderer component from the GameObject
        m_Renderer = GetComponentInChildren<SpriteRenderer>();
        //Fetch the original color of the GameObject
        m_OriginalColor = m_Renderer.color;
        pointA = transform.position;
        pointB = transform.position + Vector3.left;
    }

    public void AttackAnim()
    {
        StartCoroutine(MoveOverTime());
    }

    IEnumerator MoveOverTime()
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
    private void OnMouseOver()
    {
        //this is your object that you want to have the UI element hovering over
        

        //this is the ui element
        
        Image image = tempTargetingImage.GetComponent<Image>();
        //first you need the RectTransform component of your canvas
        RectTransform CanvasRect = canvas.GetComponent<RectTransform>();
        tempTargetingImage.transform.SetParent(canvas.transform);
        //then you calculate the position of the UI element
        //0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this, you need to subtract the height / width of the canvas * 0.5 to get the correct position.

        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(gameObject.transform.position);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f))-(ViewportPosition.y * CanvasRect.sizeDelta.y*0.5f));

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
    }

    public void TargetSelect()
    {
        target = true;
        tempTargetingImage.transform.SetParent(canvas.transform);
    }

    public void TargetDeselect()
    {
        target = false;
        tempTargetingImage.transform.SetParent(gameObject.transform);
    }

    public void SetMove()
    {
        tooltip.tooltipDescription = enemyMoves.LoadMove();
        moveImage.sprite = enemyMoves.moveImage;
        tooltip.tooltipSprite = m_Renderer.sprite;
        tooltip.tooltipName = enemyMoves.moveName;
    }
}
