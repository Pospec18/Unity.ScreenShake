using UnityEngine;

namespace Pospec.ScreenShake
{
    public abstract class ShakeListener : MonoBehaviour, IShakeListener
    {
        public bool CanShake { get; set; } = true;

        [SerializeField] private bool is2D = true;
        public bool Is2D { get => is2D; set => is2D = value; }

        public void ShakeFrom(IShake shake, Transform sender)
        {
            ShakeFrom(shake, sender.position);
        }

        public void ShakeFrom(IShake shake, Vector3 worldPosition)
        {
            Vector3 direction = CameraPosition() - worldPosition;
            DirectionalShake(shake, direction.normalized / direction.magnitude);
        }

        public void DirectionalShake(IShake shake, Vector3 direction)
        {
            if (Is2D)
                StartCoroutine(shake.ShakeCoroutine((d, v) =>
                {
                    if (!CanShake)
                        return;

                    ChangeOffsetBy(d, v);
                }, direction));
            else
                StartCoroutine(shake.ShakeCoroutine((d) =>
                {
                    if (!CanShake)
                        return;

                    ChangeOffsetBy(d);
                }, direction, CameraForward()));
        }

        public void Shake(IShake shake)
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

        protected abstract Vector3 CameraPosition();
        protected abstract Vector3 CameraForward();
    }
}
