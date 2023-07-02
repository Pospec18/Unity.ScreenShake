using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Pospec.ScreenShake
{
    public class CinemachineShaker : CameraShaker
    {
        [SerializeField] private CinemachineCameraOffset cameraOffset;
        [SerializeField] private CinemachineCameraRotation cameraRotation;

        private void Reset()
        {
            cameraOffset = GetComponent<CinemachineCameraOffset>();
        }

        protected override void ChangeOffsetBy(Vector2 deltaOffset, float deltaRotation)
        {
            cameraOffset.m_Offset += (Vector3)deltaOffset;
            cameraRotation.m_Offset += new Vector3(0, 0, deltaRotation);
        }

        protected override void ChangeOffsetBy(Vector3 deltaRotation)
        {
            Debug.Log("AAaa");
            cameraRotation.m_Offset += deltaRotation;
        }

        protected override Vector3 CameraPosition()
        {
            return cameraOffset.VirtualCamera.transform.position;
        }

        protected override Vector3 CameraForward()
        {
            return cameraOffset.VirtualCamera.transform.forward;
        }
    }
}
