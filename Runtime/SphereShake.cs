using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pospec.ScreenShake
{
    [CreateAssetMenu(fileName = "SphereShake", menuName = "Shake/Sphere")]
    public class SphereShake : Shake
    {
        [Tooltip("Maximum angle of cone from camera's center forward, where camera can rotate"), Range(0, 180)]
        public float coneAngle = 5;

        [Tooltip("Number of changes of shake direction in second"), Min(0)]
        public float frequency = 5;

        [Tooltip("Maximum z rotation of camera"), Min(0)]
        public float maxPitch = 1;

        [Tooltip("How much is shake movement spread to all directions"), Range(0, 90)]
        public float shakeSpread = 90;

        public override IEnumerator ShakeCoroutine(Action<Vector2, float> changePositionAndRotationBy)
        {
            foreach (Vector3 rotation in ComputeShake(UnityEngine.Random.Range(0, 360)))
            {
                Vector3 offset = rotation.ClampToEulerRotation();
                changePositionAndRotationBy?.Invoke(offset, offset.z);
                yield return null;
            }
        }

        public override IEnumerator ShakeCoroutine(Action<Vector3> changeRotationBy)
        {
            foreach (Vector3 rotation in ComputeShake(UnityEngine.Random.Range(0, 360)))
            {
                changeRotationBy?.Invoke(rotation);
                yield return null;
            }
        }

        public override IEnumerator ShakeCoroutine(Action<Vector2, float> changePositionAndRotationBy, Vector2 direction)
        {
            float scale = direction.magnitude;
            foreach (Vector3 rotation in ComputeShake(Vector2.SignedAngle(Vector2.right, direction)))
            {
                Vector3 offset = ShakeMath.ScaleAngles(rotation.ToOffset(), scale);
                changePositionAndRotationBy?.Invoke(offset, offset.z);
                yield return null;
            }
        }

        public override IEnumerator ShakeCoroutine(Action<Vector3> changeRotationBy, Vector3 direction, Vector3 cameraForward)
        {
            float scale = direction.magnitude;
            foreach (Vector3 rotation in ComputeShake(Vector3.SignedAngle(Vector3.right, direction, cameraForward)))
            {
                changeRotationBy?.Invoke(ShakeMath.ScaleAngles(rotation, scale));
                yield return null;
            }
        }

        private IEnumerable<Vector3> ComputeShake(float angle)
        {
            if (duration <= 0)
                yield break;

            float t = 0;
            float Scale() => curve.Evaluate(t / duration);

            float azimuth = angle;
            float inclination = UnityEngine.Random.Range(0, coneAngle * Scale());
            float lastChange = 0;
            Quaternion prevTarget = ShakeMath.RotationOnSphere(0, 0, 0);
            Quaternion prevRotation = prevTarget;
            Quaternion target = ShakeMath.RotationOnSphere(inclination, azimuth, UnityEngine.Random.Range(-1f, 1f) * maxPitch * Scale());
            while (t < duration)
            {
                t += Time.deltaTime;
                Quaternion currentRotation = Quaternion.Lerp(prevTarget, target, Mathf.SmoothStep(0, 1, (t - lastChange) * frequency));
                if ((t - lastChange) * frequency > 1)
                {
                    lastChange = t;
                    float scale = Scale();
                    azimuth = UnityEngine.Random.Range(azimuth - 180 - shakeSpread, azimuth - 180 + shakeSpread);
                    inclination = UnityEngine.Random.Range(0, coneAngle * scale);
                    prevTarget = target;
                    target = ShakeMath.RotationOnSphere(inclination, azimuth, UnityEngine.Random.Range(-1f, 1f) * maxPitch * scale);
                }

                Vector3 deltaOffset = currentRotation.eulerAngles - prevRotation.eulerAngles;
                prevRotation = currentRotation;
                yield return deltaOffset;
            }

            Vector3 offset = -prevTarget.eulerAngles;
            yield return offset;
        }
    }
}
