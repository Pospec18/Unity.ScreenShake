using System;
using System.Collections;
using UnityEngine;

namespace Pospec.ScreenShake
{
    public interface IShake
    {
        /// <summary>
        /// Calculates shake
        /// </summary>
        /// <param name="changePositionAndRotationBy">callback to sending computed values</param>
        /// <returns></returns>
        IEnumerator ShakeCoroutine(Action<Vector2, float> changePositionAndRotationBy);

        /// <summary>
        /// Calculates shake
        /// </summary>
        /// <param name="changePositionAndRotationBy">callback to sending computed values</param>
        /// <param name="direction">direction of the shake</param>
        /// <returns></returns>
        IEnumerator ShakeCoroutine(Action<Vector2, float> changePositionAndRotationBy, Vector2 direction);

        /// <summary>
        /// Calculates shake
        /// </summary>
        /// <param name="changeRotationBy">callback to sending computed values</param>
        /// <returns></returns>
        IEnumerator ShakeCoroutine(Action<Vector3> changeRotationBy);

        /// <summary>
        /// Calculates shake
        /// </summary>
        /// <param name="changeRotationBy">callback to sending computed values</param>
        /// <param name="direction">direction of the shake</param>
        /// <param name="cameraForward">forward vector of camera</param>
        /// <returns></returns>
        IEnumerator ShakeCoroutine(Action<Vector3> changeRotationBy, Vector3 direction, Vector3 cameraForward);
    }
}
