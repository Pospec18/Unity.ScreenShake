using UnityEngine;

namespace Pospec.ScreenShake
{
    public class ShakeSource : MonoBehaviour
    {
        public Shake shake;
        public bool cameraSpace = false;
        public bool playOnAwake = true;
        public bool loop = false;

        private void Awake()
        {
            if (playOnAwake)
                Play();
        }

        public void PlayDelayed(float seconds)
        {
            Invoke("Play", seconds);
        }

        public void Play()
        {
            if (loop)
                Invoke("Play", shake.duration);

            if (shake == null)
            {
                Debug.LogWarning("No shake selected", this);
                return;
            }

            if (ShakeListener.ActiveListeners.Count == 0)
                Debug.LogWarning("No active shake listeners in the scene");

            foreach (var listener in ShakeListener.ActiveListeners)
                if (cameraSpace)
                    listener?.Shake(shake);
                else
                    listener?.ShakeFrom(shake, transform);
        }
    }
}
