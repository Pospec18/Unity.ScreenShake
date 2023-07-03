using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pospec.ScreenShake
{
    [CreateAssetMenu(fileName = "CircleShake", menuName = "Shake/Circle")]
    public class CircleShake : Shake
    {
        [Tooltip("Maximum size of shake"), Min(0)]
        public float amplitude = 0.1f;

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
                changePositionAndRotationBy?.Invoke(new Vector2(rotation.y, rotation.x), rotation.z);
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
            foreach (Vector3 offset in ComputeShake(Vector2.SignedAngle(Vector2.right, direction)))
            {
                changePositionAndRotationBy?.Invoke(offset * scale, offset.z.ClampToRotationAngle() * scale);
                yield return null;
            }
        }

        public override IEnumerator ShakeCoroutine(Action<Vector3> changeRotationBy, Vector3 direction, Vector3 cameraForward)
        {
            float scale = direction.magnitude;
            foreach (Vector3 offset in ComputeShake(Vector3.SignedAngle(Vector3.right, direction, cameraForward)))
            {
                changeRotationBy?.Invoke(ShakeMath.ScaleAngles(offset.ToRotation(), scale));
                yield return null;
            }
        }

        private IEnumerable<Vector3> ComputeShake(float angle)
        {
            if (duration <= 0)
                yield break;
            float t = 0;
            float rotationAngle = UnityEngine.Random.Range(-maxPitch, maxPitch);
            float lastChange = 0;
            Vector2 lastOffsetPoint = Vector2.zero;
            float lastRotation = 0;
            Vector2 currentOffset = Vector2.zero;
            float currentRotaion = 0;
            while (t < duration)
            {
                t += Time.deltaTime;

                Vector2 nextOffsetPoint = ShakeMath.PointOnCircle(angle, amplitude * curve.Evaluate(t / duration));
                if ((t - lastChange) * frequency > 1)
                {
                    lastOffsetPoint = nextOffsetPoint;
                    lastRotation = currentRotaion;
                    angle = UnityEngine.Random.Range(angle - 180 - shakeSpread, angle - 180 + shakeSpread);
                    rotationAngle = UnityEngine.Random.Range(-maxPitch, maxPitch) * curve.Evaluate(t / duration);
                    lastChange = t;
                }

                Vector2 deltaOffset = -currentOffset;
                float deltaRotation = -currentRotaion;
                currentOffset = Vector2.Lerp(lastOffsetPoint, nextOffsetPoint, Mathf.SmoothStep(0, 1, (t - lastChange) * frequency));
                currentRotaion = Mathf.SmoothStep(lastRotation, rotationAngle, (t - lastChange) * frequency);
                deltaOffset += currentOffset;
                deltaRotation += currentRotaion;
                yield return new Vector3(deltaOffset.x, deltaOffset.y, deltaRotation);
            }
            yield return new Vector3(-currentOffset.x, -currentOffset.y, -currentRotaion);
        }
    }
}
