using Unity.Cinemachine;
using UnityEngine;

public class DollyRailCameraScript : MonoBehaviour
{
    public CinemachineSplineDolly splineDolly;
    public CinemachineCamera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(splineDolly.CameraPosition <= 0)
        {
            cam.Priority = -1;
            gameObject.SetActive(false);
        }
    }
}
