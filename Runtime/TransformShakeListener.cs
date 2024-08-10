using UnityEngine;

namespace Pospec.ScreenShake
{
    public class TransformShakeListener : ShakeListener
    {
        [SerializeField] private Transform cameraTransform;

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
            cameraTransform = Camera.main.GetComponent<Transform>();
        }

        protected override void ChangeOffsetBy(Vector3 deltaRotation)
        {
            cameraTransform.localEulerAngles += deltaRotation;
        }

        protected override void ChangeOffsetBy(Vector2 deltaOffset, float deltaRotation)
        {
            cameraTransform.transform.position += (Vector3)deltaOffset;
            cameraTransform.localEulerAngles += new Vector3(0, 0, deltaRotation);
        }

        protected override Vector3 CameraPosition()
        {
            return cameraTransform.position;
        }

        protected override Vector3 CameraForward()
        {
            return cameraTransform.forward;
        }
    }
}
