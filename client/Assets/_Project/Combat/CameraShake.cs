using System;
using Unity.Cinemachine;
using UnityEngine;

namespace TopdownRPG.Combat {
    public class CameraShake : MonoBehaviour {
        public static CameraShake Instance { get; private set; }

        private CinemachineCamera _virtualCamera;
        private CinemachineBasicMultiChannelPerlin _noise;

        private float _shakeTime;
        private float _shakeTimeTotal;
        private float _startingIntensity;

        private void Awake() {
            Instance = this;
            _virtualCamera = GetComponent<CinemachineCamera>();
            _noise = GetComponent<CinemachineBasicMultiChannelPerlin>();

            if (_virtualCamera == null)
                Debug.LogError("CameraShake requires a CinemachineCamera.");
            if (_noise == null)
                Debug.LogError("CameraShake requires a CinemachineBasicMultiChannelPerlin component.");
        }

        public void ShakeCamera(float intensity, float time) {
            if (_noise == null)
                return;

            if (intensity > _startingIntensity) {
                _startingIntensity = intensity;
                _noise.AmplitudeGain = intensity;
            }

            if (time > _shakeTime) {
                _shakeTime = time;
                _shakeTimeTotal = time;
            }

            // _startingIntensity = intensity;
            // _shakeTimeTotal = time;
            // _shakeTime = time;
            // _noise.AmplitudeGain = intensity;
        }

        private void Update() {
            if (_shakeTime > 0) {
                _shakeTime -= Time.deltaTime;

                // float normalizedTime = 1f - (_shakeTime / _shakeTimeTotal);
                // _noise.AmplitudeGain = Mathf.Lerp(_startingIntensity, 0f, normalizedTime);
                float normalizedTime = 1f - (_shakeTime / _shakeTimeTotal);
                float falloff = 1f - Mathf.SmoothStep(0f, 1f, normalizedTime); // tạo hiệu ứng giảm dần
                _noise.AmplitudeGain = _startingIntensity * falloff;
            }
        }
    }
}