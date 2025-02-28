using System.Collections;
using TMPro;
using UnityEngine;

public class Enemy : Unit
{
    //When the mouse hovers over the GameObject, it turns to this color (red)
    Color m_MouseOverColor = Color.green;

    //This stores the GameObject’s original color
    Color m_OriginalColor;

    //Get the GameObject’s mesh renderer to access the GameObject’s material and color
    public SpriteRenderer m_Renderer;

    public bool target = false;

    public bool casting = false;

    public BattleHUD enemyHUD;

    public EnemyMoves enemyMoves;

    Vector3 pointA;
    Vector3 pointB;

    void Start()
    {
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
        if (BattleSystem.state == BattleState.CASTING)
        {
            m_Renderer.color = m_MouseOverColor;
        }
    }

    private void OnMouseExit()
    {
        m_Renderer.color = m_OriginalColor;
    }

    public void TargetSelect()
    {
        target = true;
        //this.GetComponentInChildren<SpriteRenderer>().color = Color.red;
    }

    public void TargetDeselect()
    {
        target = false;
        this.GetComponentInChildren<SpriteRenderer>().color = m_OriginalColor;
    }
}
