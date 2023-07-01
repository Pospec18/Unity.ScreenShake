using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Pospec.ScreenShake
{
    public class CinemachineShaker : CameraShaker
    {
        [SerializeField] private CinemachineCameraOffset cameraOffset;

        private void Reset()
        {
            cameraOffset = GetComponent<CinemachineCameraOffset>();
        }

        protected override void ChangeOffsetBy(Vector2 deltaOffset, float deltaRotation)
        {
            cameraOffset.m_Offset += (Vector3)deltaOffset;
        }

        protected override void ChangeOffsetBy(Vector3 deltaRotation)
        {
            throw new System.NotImplementedException();
        }
    }
}
