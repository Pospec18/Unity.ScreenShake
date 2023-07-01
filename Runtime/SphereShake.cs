using System;
using System.Collections;
using UnityEngine;

namespace Pospec.ScreenShake
{
    [CreateAssetMenu(fileName = "SphereShake", menuName = "Shake/Sphere")]
    public class SphereShake : Shake
    {
        public float coneAngle = 45;
        public float frequency = 5;
        public float maxPitch = 1;

        public override IEnumerator ShakeCoroutine(Action<Vector2, float> changePositionAndRotationBy)
        {
            if (duration <= 0)
                yield break;

            float t = 0;
            float Scale() => curve.Evaluate(t / duration);

            float azimuth = UnityEngine.Random.Range(0, 360);
            float inclination = UnityEngine.Random.Range(0, coneAngle * Scale());
            float lastChange = 0;
            Quaternion prevTarget = ShakeMath.RotationOnSphere(0, 0, 0);
            Quaternion target = ShakeMath.RotationOnSphere(inclination, azimuth, UnityEngine.Random.Range(-1, 1) * maxPitch * Scale());
            Quaternion prevRotation = prevTarget;
            while (t < duration)
            {
                t += Time.deltaTime;
                Quaternion currentRotation = Quaternion.Lerp(prevTarget, target, (t - lastChange) * frequency);
                if ((t - lastChange) * frequency > 1)
                {
                    lastChange = t;
                    float scale = Scale();
                    azimuth = UnityEngine.Random.Range(azimuth - 180 - 90, azimuth - 180 + 90);
                    inclination = UnityEngine.Random.Range(0, coneAngle * scale);
                    prevTarget = target;
                    target = ShakeMath.RotationOnSphere(inclination, azimuth, UnityEngine.Random.Range(-1, 1) * maxPitch * scale);
                }

                Vector3 deltaOffset = currentRotation.eulerAngles - prevTarget.eulerAngles;
                deltaOffset.x = deltaOffset.x < 0 ? deltaOffset.x + 360 : deltaOffset.x;
                deltaOffset.x = deltaOffset.x < 180 ? deltaOffset.x : deltaOffset.x - 360;
                deltaOffset.y = deltaOffset.y < 0 ? deltaOffset.y + 360 : deltaOffset.y;
                deltaOffset.y = deltaOffset.y < 180 ? deltaOffset.y : deltaOffset.y - 360;
                changePositionAndRotationBy?.Invoke((Vector2)deltaOffset, deltaOffset.z);
                prevTarget = currentRotation;
                yield return null;
            }

            Vector3 remainingOffset = -prevTarget.eulerAngles;
            remainingOffset.x = remainingOffset.x < 0 ? remainingOffset.x + 360 : remainingOffset.x;
            remainingOffset.x = remainingOffset.x < 180 ? remainingOffset.x : remainingOffset.x - 360;
            remainingOffset.y = remainingOffset.y < 0 ? remainingOffset.y + 360 : remainingOffset.y;
            remainingOffset.y = remainingOffset.y < 180 ? remainingOffset.y : remainingOffset.y - 360;
            changePositionAndRotationBy?.Invoke((Vector2)remainingOffset, remainingOffset.z);
        }

        public override IEnumerator ShakeCoroutine(Action<Vector3> changeRotationBy)
        {
            if (duration <= 0)
                yield break;

            float t = 0;
            float Scale() => curve.Evaluate(t / duration);

            float azimuth = UnityEngine.Random.Range(0, 360);
            float inclination = UnityEngine.Random.Range(0, coneAngle * Scale());
            float lastChange = 0;
            Quaternion prevTarget = ShakeMath.RotationOnSphere(0, 0, 0);
            Quaternion target = ShakeMath.RotationOnSphere(inclination, azimuth, UnityEngine.Random.Range(-1f, 1f) * maxPitch * Scale());
            Quaternion prevRotation = prevTarget;
            while (t < duration)
            {
                t += Time.deltaTime;
                Quaternion currentRotation = Quaternion.Lerp(prevTarget, target, (t - lastChange) * frequency);
                if ((t - lastChange) * frequency > 1)
                {
                    lastChange = t;
                    float scale = Scale();
                    azimuth = UnityEngine.Random.Range(azimuth - 180 - 90, azimuth - 180 + 90);
                    inclination = UnityEngine.Random.Range(0, coneAngle * scale);
                    prevTarget = target;
                    target = ShakeMath.RotationOnSphere(inclination, azimuth, UnityEngine.Random.Range(-1f, 1f) * maxPitch * scale);
                }

                Vector3 deltaOffset = currentRotation.eulerAngles - prevTarget.eulerAngles;
                changeRotationBy?.Invoke(deltaOffset);
                prevTarget = currentRotation;
                yield return null;
            }

            Vector3 remainingOffset = -prevTarget.eulerAngles;
            changeRotationBy?.Invoke(remainingOffset);
        }
    }
}
