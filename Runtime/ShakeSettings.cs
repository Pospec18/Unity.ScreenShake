using UnityEngine;
using UnityEngine.UI;

namespace Pospec.ScreenShake
{
    public class ShakeSettings : MonoBehaviour
    {
        [SerializeField] private Toggle canShakeToggle;
        private const string canShakeId = "CanShake";

        private static ShakeSettingsData _data;
        public static ShakeSettingsData Data
        {
            get
            {
                if (_data == null)
                    _data = LoadData();
                return _data;
            }
        }

        private void OnValidate()
        {
            if (canShakeToggle != null)
            {
                canShakeToggle.interactable = true;
            }
        }

        private void Start()
        {
            canShakeToggle.onValueChanged.AddListener(SetCanShake);
            canShakeToggle.isOn = Data.ShakeOn;
        }

        private void OnDestroy()
        {
            canShakeToggle.onValueChanged.RemoveListener(SetCanShake);
        }

        private void SetCanShake(bool on)
        {
            Data.ShakeOn = on;
            Save();
        }

        private static ShakeSettingsData LoadData()
        {
            if (!PlayerPrefs.HasKey(canShakeId))
            {
                PlayerPrefs.SetInt(canShakeId, 1);
                PlayerPrefs.Save();
            }

            return new ShakeSettingsData()
            {
                ShakeOn = PlayerPrefs.GetInt(canShakeId) != 0
            };
        }

        private void Save()
        {
            PlayerPrefs.SetInt(canShakeId, Data.ShakeOn ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public class ShakeSettingsData
    {
        public bool ShakeOn = true;
    }
}
