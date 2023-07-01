using System;
using System.Collections;
using UnityEngine;

namespace Pospec.ScreenShake
{
    public abstract class Shake : ScriptableObject, IShake
    {
        public abstract IEnumerator ShakeCoroutine(Action<Vector2, float> changePositionAndRotationBy);
        public abstract IEnumerator ShakeCoroutine(Action<Vector3> changeRotationBy);
    }
}
