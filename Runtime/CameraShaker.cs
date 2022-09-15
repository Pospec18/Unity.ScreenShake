using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Reflection;

namespace Pospec.ScreenShake
{
    public class CameraShaker : MonoBehaviour
    {
        [SerializeField] private CinemachineCameraOffset cameraOffset;
        [SerializeField] private AnimationCurve shakeFallof;
        [SerializeField, Min(0)] private float shakeSpeed = 5;
        [SerializeField, Range(0, 90)] private float maxShakeAngle = 10;
        private float shakePower;
        private Vector3 nextShakePoint;
        private float nextShakeAngle;

        private const float epsilon = 0.01f;

        private void Reset()
        {
            cameraOffset = GetComponent<CinemachineCameraOffset>();
            shakeFallof = new AnimationCurve();
            shakeFallof.AddKey(new Keyframe(0, 0));
            shakeFallof.AddKey(new Keyframe(0.1f, 1));
            shakeFallof.AddKey(new Keyframe(1, 0));
            
        }
        private void LateUpdate()
        {
            if (shakePower < epsilon)
            {
                cameraOffset.m_Offset = Vector3.zero;
                cameraOffset.transform.rotation = Quaternion.Euler(Vector3.zero);
                return;
            }

            if (Vector2.Distance(cameraOffset.m_Offset, nextShakePoint) < epsilon)
            {
                nextShakePoint = Random.insideUnitCircle * shakePower;
            }
            Vector3 dir = nextShakePoint - cameraOffset.m_Offset;
            cameraOffset.m_Offset += dir.normalized * Time.deltaTime * shakeSpeed;

            float camRotation = cameraOffset.transform.rotation.eulerAngles.z;
            if (camRotation > 180)
                camRotation -= 360;
            if(Mathf.Abs(camRotation - nextShakeAngle) < epsilon)
            {
                nextShakeAngle = Random.Range(-maxShakeAngle * shakePower, maxShakeAngle * shakePower);
            }
            cameraOffset.transform.Rotate(Vector3.forward, Mathf.Clamp(nextShakeAngle - camRotation, -1, 1) * 20 * shakeSpeed * Time.deltaTime);
        }

        public void Shake(float power, float time)
        {
            StartCoroutine(ShakeCoroutine(power * 0.5f, time));
        }

        private IEnumerator ShakeCoroutine(float power, float time)
        {
            if (time <= 0)
                yield break;
            shakePower += power;
            float t = 0;
            float currentPower = power;
            while (t < 1)
            {
                t += Time.deltaTime / time;
                shakePower -= currentPower;
                currentPower = power * shakeFallof.Evaluate(t);
                shakePower += currentPower;
                yield return null;
            }
            shakePower -= currentPower;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)PointOnCircle(maxShakeAngle, 2));
        }

        private static Vector2 PointOnCircle(float angle, float radius)
        {
            angle *= Mathf.PI / 180;
            return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        }
    }
}
