using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Pospec.ScreenShake
{
    public class CameraShaker : MonoBehaviour
    {
        public bool CanShake { get; set; } = true;
        [SerializeField] private CinemachineCameraOffset cameraOffset;
        private const float epsilon = 0.01f;

        private void Reset()
        {
            cameraOffset = GetComponent<CinemachineCameraOffset>();
        }

        public void Shake(Shake shake, Transform sender)
        {
            StartCoroutine(shake.ShakeCoroutine(ChangeOffsetBy));
        }

        private void ChangeOffsetBy(Vector3 deltaOffset)
        {
            if(!CanShake)
                return;

            cameraOffset.m_Offset += deltaOffset;
        }

        public static Vector2 PointOnCircle(float angle, float radius)
        {
            angle *= Mathf.PI / 180;
            return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        }
    }
}
