using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public BattleSystem battleSystem;
    private Camera _mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void OnClick(InputAction.CallbackContext context)
    //{
    //    if (!context.started)
    //    {
    //        return;
    //    }

    //    var rayhit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));

    //    if (!rayhit.collider)
    //    {
    //        return;
    //    }

    //    if(rayhit.collider.gameObject.tag == "Enemy")
    //    {
    //        Enemy enemy = (Enemy)rayhit.collider.gameObject.GetComponent<Enemy>();
    //        battleSystem.MarkTargets(enemy);
    //    }
    //    Debug.Log(rayhit.collider.gameObject.name);
    //}
}
