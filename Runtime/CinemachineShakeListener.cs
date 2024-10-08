using UnityEngine;

namespace Pospec.ScreenShake
{
    public class CinemachineShakeListener : ShakeListener
    {
        [SerializeField] private CinemachineCameraOffset cameraOffset;
        [SerializeField] private CinemachineCameraRotation cameraRotation;

        private void OnEnable()
        {
            ActiveListeners.Add(this);
        }

        private void OnDisable()
        {
            ActiveListeners.Remove(this);
        }

        private void Reset()
        {
            cameraOffset = FindObjectOfType<CinemachineCameraOffset>();
            cameraRotation = FindObjectOfType<CinemachineCameraRotation>();
        }

        protected override void ChangeOffsetBy(Vector2 deltaOffset, float deltaRotation)
        {
            cameraOffset.m_Offset += (Vector3)deltaOffset;
            cameraRotation.m_Offset += new Vector3(0, 0, deltaRotation);
        }

        protected override void ChangeOffsetBy(Vector3 deltaRotation)
        {
            cameraRotation.m_Offset += deltaRotation;
        }

        protected override Vector3 CameraForward()
        {
            return cameraOffset.VirtualCamera.transform.forward;
        }
    }
}
