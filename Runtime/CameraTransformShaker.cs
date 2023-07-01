using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pospec.ScreenShake
{
    public class CameraTransformShaker : CameraShaker
    {
        [SerializeField] private Transform cameraTransform;

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
    }
}
