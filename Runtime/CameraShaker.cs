using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Reflection;

namespace Pospec.ScreenShake
{
    public class CameraShaker : MonoBehaviour
    {
        [SerializeField] private CinemachineCameraOffset cameraOffset;
        [SerializeField] private AnimationCurve shakeFallof;
        private float shakePower;

        private void Reset()
        {
            cameraOffset = GetComponent<CinemachineCameraOffset>();
            shakeFallof = new AnimationCurve();
            shakeFallof.AddKey(new Keyframe(0, 1));
            shakeFallof.AddKey(new Keyframe(1, 0));
            
        }
        private void LateUpdate()
        {
            cameraOffset.m_Offset = Random.insideUnitCircle * shakePower;
        }

        public void Shake(float power, float time)
        {
            StartCoroutine(ShakeCoroutine(power, time));
        }


        private IEnumerator ShakeCoroutine(float power, float time)
        {
            if (time <= 0)
                yield break;
            shakePower += power;
            float t = 0;
            float currentPower = power;
            while (t < 1)
            {
                t += Time.deltaTime / time;
                shakePower -= currentPower;
                currentPower = power * shakeFallof.Evaluate(t);
                shakePower += currentPower;
                yield return null;
            }
        }
    }
}
