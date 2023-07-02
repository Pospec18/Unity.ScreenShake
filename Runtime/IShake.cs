using System;
using System.Collections;
using UnityEngine;

namespace Pospec.ScreenShake
{
    public interface IShake
    {
        IEnumerator ShakeCoroutine(Action<Vector2, float> changePositionAndRotationBy);
        IEnumerator ShakeCoroutine(Action<Vector2, float> changePositionAndRotationBy, Vector2 direction);
        IEnumerator ShakeCoroutine(Action<Vector3> changeRotationBy);
        IEnumerator ShakeCoroutine(Action<Vector3> changeRotationBy, Vector3 direction, Vector3 cameraForward);
    }
}
