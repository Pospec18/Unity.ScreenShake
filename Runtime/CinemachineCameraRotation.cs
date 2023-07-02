using UnityEngine;
using Cinemachine.Utility;
using Cinemachine;

/// <summary>
/// An add-on module for Cinemachine Virtual Camera that adds a final rotation offset to the camera
/// </summary>
[AddComponentMenu("")] // Hide in menu
[ExecuteAlways]
[SaveDuringPlay]
public class CinemachineCameraRotation : CinemachineExtension
{
    /// <summary>
    /// Offset the camera's rotation by this much (camera space) in eulers
    /// </summary>
    [Tooltip("Offset the camera's rotation by this much (camera space) in eulers")]
    public Vector3 m_Offset = Vector3.zero;

    /// <summary>
    /// When to apply the offset
    /// </summary>
    [Tooltip("When to apply the offset")]
    public CinemachineCore.Stage m_ApplyAfter = CinemachineCore.Stage.Aim;

    /// <summary>
    /// Applies the specified offset to the camera state
    /// </summary>
    /// <param name="vcam">The virtual camera being processed</param>
    /// <param name="stage">The current pipeline stage</param>
    /// <param name="state">The current virtual camera state</param>
    /// <param name="deltaTime">The current applicable deltaTime</param>
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == m_ApplyAfter)
        {
            state.OrientationCorrection *= Quaternion.Euler(m_Offset);
        }
    }
}
