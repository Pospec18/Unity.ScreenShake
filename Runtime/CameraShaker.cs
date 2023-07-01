using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pospec.ScreenShake
{
    public abstract class CameraShaker : MonoBehaviour, ICameraShaker
    {
        public bool CanShake { get; set; } = true;

        [SerializeField] private bool is2D = true;
        public bool Is2D { get => is2D; set => is2D = value; }
        
        public virtual void Shake(IShake shake, Transform sender)
        {
            if (Is2D)
                StartCoroutine(shake.ShakeCoroutine((d, v) =>
                {
                    if (!CanShake)
                        return;

                    ChangeOffsetBy(d, v);
                }));
            else
                StartCoroutine(shake.ShakeCoroutine((d) =>
                {
                    if (!CanShake)
                        return;

                    ChangeOffsetBy(d);
                }));
        }

        protected abstract void ChangeOffsetBy(Vector3 deltaRotation);
        protected abstract void ChangeOffsetBy(Vector2 deltaOffset, float deltaRotation);
    }
}
