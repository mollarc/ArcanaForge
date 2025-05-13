using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class ParallaxCamera : MonoBehaviour
{
    public float speedX = 0.1f;
    public float speedY = 0.1f;
    public float speedYRotate = 0.5f;
    public float speedXRotate = 1.0f;

    public float minX = -5.0f;
    public float maxX = 5.0f;
    public float minY = -1.0f;
    public float maxY = 0.25f;
    public float maxXRotation = 10.0f;
    public float maxYRotation = 15.0f;

    private float x = 0.0f;
    private float y = 0.0f;
    private float yR = 0.0f;
    private float xR = 0.0f;

    public Vector3 mouseWorldPosition;

    public GameObject pointToLook;

    void Update()
    {
        x -= speedX * Input.GetAxis("Mouse X");
        y -= speedY * Input.GetAxis("Mouse Y");
        yR += speedYRotate * Input.GetAxis("Mouse X");
        xR += speedXRotate * Input.GetAxis("Mouse Y");
        mouseWorldPosition= Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, 0.01f));
        if ( minX <= mouseWorldPosition.x && mouseWorldPosition.x <= maxX)
        {
            transform.position = new Vector3(mouseWorldPosition.x, transform.position.y, 0.0f);

        }
        if (minY <= mouseWorldPosition.y && mouseWorldPosition.y <= maxY)
        {
            transform.position = new Vector3(transform.position.x, mouseWorldPosition.y, 0.0f);
        }
        transform.LookAt(pointToLook.transform);
    }
}