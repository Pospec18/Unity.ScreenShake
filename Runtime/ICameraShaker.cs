using UnityEngine;

namespace Pospec.ScreenShake
{
    public interface ICameraShaker
    {
        /// <summary>
        /// Trigger shake from objects potition
        /// </summary>
        /// <param name="shake">shake to be used</param>
        /// <param name="sender">object where shake was triggered</param>
        void ShakeFrom(IShake shake, Transform sender);

        /// <summary>
        /// Trigger shake from position in world
        /// </summary>
        /// <param name="shake">shake to be used</param>
        /// <param name="worldPosition"></param>
        void ShakeFrom(IShake shake, Vector3 worldPosition);

        /// <summary>
        /// Shake camera in specific direction
        /// </summary>
        /// <param name="shake">shake to be used</param>
        /// <param name="direction">direction of shake</param>
        void DirectionalShake(IShake shake, Vector3 direction);

        /// <summary>
        /// Nondirectional shake
        /// </summary>
        /// <param name="shake">shake to be used</param>
        void Shake(IShake shake);
    }
}
