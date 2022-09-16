using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pospec.ScreenShake
{
    [CreateAssetMenu(fileName = "Shake", menuName = "Shake")]
    public class Shake : ScriptableObject
    {
        public float amplitude = 0.1f;
        public float frequency = 5;
        public float duration = 0.2f;
        public AnimationCurve curve;
        [Tooltip("If not fixed, direction will be calculated from sender to camera")]
        public bool fixedDirection;
        public Vector2 direction;

        private void Reset()
        {
            curve = new AnimationCurve();
            curve.AddKey(new Keyframe(0, 0));
            curve.AddKey(new Keyframe(0.1f, 1));
            curve.AddKey(new Keyframe(1, 0));
        }

        public virtual IEnumerator ShakeCoroutine(Action<Vector3> changeOffsetBy)
        {
            if (duration <= 0)
                yield break;
            float t = 0;
            float angle = UnityEngine.Random.Range(0, 360);
            float lastChange = 0;
            Vector2 lastOffsetPoint = Vector2.zero;
            Vector2 currentOffset = Vector2.zero;
            while (t < duration)
            {
                yield return null;
                t += Time.deltaTime;

                Vector2 nextOffsetPoint = CameraShaker.PointOnCircle(angle, amplitude * curve.Evaluate(t / duration));
                if ((t - lastChange) * frequency > 1)
                {
                    lastOffsetPoint = nextOffsetPoint;
                    angle = UnityEngine.Random.Range(angle - 180 - 90, angle - 180 + 90);
                    lastChange = t;
                }

                Vector2 deltaOffset = -currentOffset;
                currentOffset = Vector2.Lerp(lastOffsetPoint, nextOffsetPoint, (t - lastChange) * frequency);
                deltaOffset += currentOffset;
                changeOffsetBy?.Invoke(deltaOffset);
            }
            changeOffsetBy?.Invoke(-currentOffset);
        }
    }
}
