using System;
using System.Collections;
using UnityEngine;

namespace Pospec.ScreenShake
{
    [CreateAssetMenu(fileName = "CircleShake", menuName = "Shake/Circle")]
    public class CircleShake : Shake
    {
        public float amplitude = 0.1f;
        public float frequency = 5;
        public float rotation = 10;

        public override IEnumerator ShakeCoroutine(Action<Vector2, float> changePositionAndRotationBy)
        {
            if (duration <= 0)
                yield break;
            float t = 0;
            float angle = UnityEngine.Random.Range(0, 360);
            float rotationAngle = UnityEngine.Random.Range(-rotation, rotation);
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
                    angle = UnityEngine.Random.Range(angle - 180 - 90, angle - 180 + 90);
                    rotationAngle = UnityEngine.Random.Range(-rotation, rotation) * curve.Evaluate(t / duration);
                    lastChange = t;
                }

                Vector2 deltaOffset = -currentOffset;
                float deltaRotation = -currentRotaion;
                currentOffset = Vector2.Lerp(lastOffsetPoint, nextOffsetPoint, (t - lastChange) * frequency);
                currentRotaion = Mathf.SmoothStep(lastRotation, rotationAngle, (t - lastChange) * frequency);
                deltaOffset += currentOffset;
                deltaRotation += currentRotaion;
                changePositionAndRotationBy?.Invoke(deltaOffset, deltaRotation);
                yield return null;
            }
            changePositionAndRotationBy?.Invoke(-currentOffset, -currentRotaion);
        }

        public override IEnumerator ShakeCoroutine(Action<Vector3> changeRotationBy)
        {
            if (duration <= 0)
                yield break;
            float t = 0;
            float angle = UnityEngine.Random.Range(0, 360);
            float rotationAngle = UnityEngine.Random.Range(-rotation, rotation);
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
                    angle = UnityEngine.Random.Range(angle - 180 - 90, angle - 180 + 90);
                    rotationAngle = UnityEngine.Random.Range(-rotation, rotation) * curve.Evaluate(t / duration);
                    lastChange = t;
                }

                Vector2 deltaOffset = -currentOffset;
                float deltaRotation = -currentRotaion;
                currentOffset = Vector2.Lerp(lastOffsetPoint, nextOffsetPoint, (t - lastChange) * frequency);
                currentRotaion = Mathf.SmoothStep(lastRotation, rotationAngle, (t - lastChange) * frequency);
                deltaOffset += currentOffset;
                deltaRotation += currentRotaion;
                changeRotationBy?.Invoke(new Vector3(deltaOffset.x, deltaOffset.y, deltaRotation));
                yield return null;
            }
            changeRotationBy?.Invoke(new Vector3(-currentOffset.x, -currentOffset.y, -currentRotaion));
        }
    }
}
