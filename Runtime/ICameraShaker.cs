using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pospec.ScreenShake
{
    public interface ICameraShaker
    {
        void Shake(IShake shake, Transform sender);
    }
}
