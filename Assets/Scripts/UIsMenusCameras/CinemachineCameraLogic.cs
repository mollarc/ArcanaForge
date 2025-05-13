using Unity.Cinemachine;
using UnityEngine;

public class CinemachineCameraLogic : MonoBehaviour
{
    public CinemachinePositionComposer positionComposer;
    public CinemachinePanTilt panTilt;
    public CinemachineInputAxisController inputAxisController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        positionComposer.TargetOffset = new Vector3(panTilt.PanAxis.Value * -0.1f, panTilt.TiltAxis.Value * 0.1f);
    }

    public void BattleEndTurnCamera()
    {
        panTilt.PanAxis.Recentering.Wait = 0f;
        panTilt.TiltAxis.Recentering.Wait = 0f;
        inputAxisController.enabled = false;
    }

    public void BattlePlayerTurnCamera()
    {
        panTilt.PanAxis.Recentering.Wait = 0.25f;
        panTilt.TiltAxis.Recentering.Wait = 0.25f;
        inputAxisController.enabled = true;
    }
}
