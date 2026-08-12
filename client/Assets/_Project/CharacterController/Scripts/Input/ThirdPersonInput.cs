using System;
using _Project.CharactorController;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TopdownRPG.Character
{
    [DefaultExecutionOrder(-2)]
    public class ThirdPersonInput : MonoBehaviour, PlayerControls.IThirdPersonMapActions
    {
        #region Class Variable
        public Vector2 ScrollInput { get; private set; }

        [SerializeField] private CinemachineCamera _virtualCamera;
        [SerializeField] private float _cameraZoomSpeed = 0.1f;
        [SerializeField] private float _cameraMinZoom = 1f;
        [SerializeField] private float _cameraMaxZoom = 5f;

        private CinemachineThirdPersonFollow _thirdPersonFollow;
        #endregion

        #region Startup
        private void Awake() {
            var component = _virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
            _thirdPersonFollow = component as CinemachineThirdPersonFollow;
        }

        private void OnEnable() {
            if (PlayerInputManager.Instance?.PlayerControls == null) {
                Debug.LogError("Player controls is not initialized - cannot enable");
                return;
            }

            PlayerInputManager.Instance.PlayerControls.ThirdPersonMap.Enable();
            PlayerInputManager.Instance.PlayerControls.ThirdPersonMap.SetCallbacks(this);
        }

        private void OnDisable() {
            if (PlayerInputManager.Instance?.PlayerControls == null) {
                Debug.LogError("Player controls is not initialized - cannot disable");
                return;
            }

            PlayerInputManager.Instance.PlayerControls.ThirdPersonMap.Disable();
            PlayerInputManager.Instance.PlayerControls.ThirdPersonMap.RemoveCallbacks(this);
        }
        #endregion

        #region Update Logic
        private void Update() {
            float newDistance = _thirdPersonFollow.CameraDistance + ScrollInput.y;
            _thirdPersonFollow.CameraDistance = Mathf.Clamp(newDistance, _cameraMinZoom, _cameraMaxZoom);
        }

        private void LateUpdate() {
            ScrollInput = Vector2.zero;
        }
        #endregion

        #region Input Callback
        public void OnScrollCamera(InputAction.CallbackContext context) {
            if (!context.performed)
                return;

            Vector2 scrollInput = context.ReadValue<Vector2>();
            ScrollInput = -1f * scrollInput * _cameraZoomSpeed;
        }
        #endregion
    }
}