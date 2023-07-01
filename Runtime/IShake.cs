using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pospec.ScreenShake
{
    public interface IShake
    {
        IEnumerator ShakeCoroutine(Action<Vector2, float> changePositionAndRotationBy);
        IEnumerator ShakeCoroutine(Action<Vector3> changeRotationBy);
    }
}
