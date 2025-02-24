using TMPro;
using UnityEngine;

public class Enemy : Unit
{
    //When the mouse hovers over the GameObject, it turns to this color (red)
    Color m_MouseOverColor = Color.green;

    //This stores the GameObjectís original color
    Color m_OriginalColor;

    //Get the GameObjectís mesh renderer to access the GameObjectís material and color
    SpriteRenderer m_Renderer;

    public bool target = false;

    public bool casting = false;

    public BattleHUD enemyHUD;

    public EnemyMoves enemyMoves;

    void Start()
    {
        //Fetch the mesh renderer component from the GameObject
        m_Renderer = GetComponentInChildren<SpriteRenderer>();
        //Fetch the original color of the GameObject
        m_OriginalColor = m_Renderer.color;
    }
    private void OnMouseOver()
    {
        m_Renderer.color = m_MouseOverColor;
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
