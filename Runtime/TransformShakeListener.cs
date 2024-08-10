using UnityEngine;

namespace Pospec.ScreenShake
{
    public class TransformShakeListener : ShakeListener
    {
        [SerializeField] private Camera target;

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
            target = Camera.main;
        }

        protected override void ChangeOffsetBy(Vector3 deltaRotation)
        {
            target.transform.localEulerAngles += deltaRotation;
        }

        protected override void ChangeOffsetBy(Vector2 deltaOffset, float deltaRotation)
        {
            target.transform.position += (Vector3)deltaOffset;
            target.transform.localEulerAngles += new Vector3(0, 0, deltaRotation);
        }

        protected override Vector3 CameraForward()
        {
            return target.transform.forward;
        }
    }
}
