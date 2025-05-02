using Unity.Cinemachine;
using UnityEngine;

public class CinemachineParallaxTest : MonoBehaviour
{
    public CinemachinePositionComposer positionComposer;
    public CinemachinePanTilt panTilt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        positionComposer = gameObject.GetComponent<CinemachinePositionComposer>();
        panTilt = gameObject.GetComponent<CinemachinePanTilt>();
    }

    // Update is called once per frame
    void Update()
    {
        positionComposer.TargetOffset = new Vector3(panTilt.PanAxis.Value * -0.1f, panTilt.TiltAxis.Value * 0.1f);
    }
}
