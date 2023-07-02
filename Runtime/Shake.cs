using System;
using System.Collections;
using UnityEngine;

namespace Pospec.ScreenShake
{
    public abstract class Shake : ScriptableObject, IShake
    {
        [Tooltip("Duration of the shake in seconds")]
        public float duration = 0.2f;

        [Tooltip("Size of shake in time")]
        public AnimationCurve curve;

        private void Reset()
        {
            curve = new AnimationCurve();
            curve.AddKey(new Keyframe(0, 0));
            curve.AddKey(new Keyframe(0.1f, 1));
            curve.AddKey(new Keyframe(1, 0));
        }

        public abstract IEnumerator ShakeCoroutine(Action<Vector2, float> changePositionAndRotationBy);
        public abstract IEnumerator ShakeCoroutine(Action<Vector2, float> changePositionAndRotationBy, Vector3 direction);
        public abstract IEnumerator ShakeCoroutine(Action<Vector3> changeRotationBy);
        public abstract IEnumerator ShakeCoroutine(Action<Vector3> changeRotationBy, Vector3 direction);
    }
}
