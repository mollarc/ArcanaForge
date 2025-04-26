using UnityEngine;

public class tempfollow : MonoBehaviour
{
    Vector3 worldPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        gameObject.transform.position = worldPosition;
    }
}
