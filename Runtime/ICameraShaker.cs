using UnityEngine;

namespace Pospec.ScreenShake
{
    public interface ICameraShaker
    {
        void ShakeFrom(IShake shake, Transform sender);
        void ShakeFrom(IShake shake, Vector3 worldPosition);
        void DirectionalShake(IShake shake, Vector3 direction);
        void Shake(IShake shake);
    }
}
