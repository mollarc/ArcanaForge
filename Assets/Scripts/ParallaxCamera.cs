using UnityEngine;
using System.Collections;

public class ParallaxCamera : MonoBehaviour
{
    public float speedX = 0.1f;
    public float speedY = 0.1f;
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    public float maxX = 200.0f;
    public float maxY = 200.0f;
    public float maxXRotation = 15.0f;
    public float maxYRotation = 15.0f;

    private float x = 0.0f;
    private float y = 0.0f;
    private float h = 0.0f;
    private float v = 0.0f;

    void Update()
    {
        x -= speedX * Input.GetAxis("Mouse X");
        y -= speedY * Input.GetAxis("Mouse Y");
        h -= speedH * Input.GetAxis("Mouse X");
        v -= speedV * Input.GetAxis("Mouse Y");

        float X = transform.position.x;
        float Y = transform.position.y;
        float XRotation = transform.eulerAngles.x;
        float YRotation = transform.eulerAngles.y;
        if ( -maxX <= X && X <= maxX)
        {
            print("X Camera");
            transform.position = new Vector3(x, transform.position.y, 0.0f);
            
        }
        else
        {
            if (X < 0)
            {
                transform.position = new Vector3(-maxX, transform.position.y, 0.0f);
            }
            else
            {
                transform.position = new Vector3(maxX, transform.position.y, 0.0f);
            }
        }
        if(-maxXRotation <= XRotation && XRotation <= maxXRotation)
        {
            transform.eulerAngles = new Vector3(h, transform.eulerAngles.y, 0.0f);
        }
        else
        {
            if (XRotation < 0)
            {
                transform.eulerAngles = new Vector3(-maxXRotation, transform.eulerAngles.y, 0.0f);
            }
            else
            {
                transform.eulerAngles = new Vector3(maxXRotation, transform.eulerAngles.y, 0.0f);
            }
        }
        if (-maxY <= Y && Y <= maxY)
        {
            print("Y Camera");
            transform.position = new Vector3(transform.position.x, y, 0.0f);
            
        }
        else
        {
            if (Y < 0)
            {
                transform.position = new Vector3(transform.position.x, -maxY, 0.0f);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, maxY, 0.0f);
            }
        }
        if (-maxYRotation <= YRotation && YRotation <= maxYRotation)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, v, 0.0f);
        }
        else
        {
            if (YRotation < 0)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, -maxYRotation, 0.0f);
            }
            else
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, maxYRotation, 0.0f);
            }
        }

    }
}