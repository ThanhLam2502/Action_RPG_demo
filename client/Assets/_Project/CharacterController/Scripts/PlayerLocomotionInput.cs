using System;
using _Project.CharactorController;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TopdownRPG.Character
{
    [DefaultExecutionOrder(-2)]
    public class PlayerLocomotionInput : MonoBehaviour, PlayerControls.IPlayerLocomotionMapActions
    {
        #region Class Variable

        [SerializeField]
        private bool holdToSprint = true;
        public bool SprintToggleOn { get; private set; }
        public bool JumpPressed { get; private set; }
        public PlayerControls PlayerControls { get; private set; }
        public Vector2 MovementInput { get; private set; }
        public Vector2 LookInput { get; private set; }

        #endregion

        #region Startup

        private void OnEnable() {
            PlayerControls = new PlayerControls();
            PlayerControls.Enable();

            PlayerControls.PlayerLocomotionMap.Enable();
            PlayerControls.PlayerLocomotionMap.SetCallbacks(this);
        }

        private void OnDisable() {
            PlayerControls.PlayerLocomotionMap.Disable();
            PlayerControls.PlayerLocomotionMap.RemoveCallbacks(this);
        }

        #endregion

        #region Late Update Logic

        private void LateUpdate() {
            JumpPressed = false;
        }

        #endregion

        #region Input Callback

        public void OnMovement(InputAction.CallbackContext context) {
            MovementInput = context.ReadValue<Vector2>();
            // print($"Movement Input: {MovementInput}");
        }

        public void OnLook(InputAction.CallbackContext context) {
            LookInput = context.ReadValue<Vector2>();
            // print($"Look Input: {LookInput}");
        }

        public void OnToggleSprint(InputAction.CallbackContext context) {
            if (context.performed) {
                SprintToggleOn = holdToSprint || !SprintToggleOn;
            }
            else if (context.canceled) {
                SprintToggleOn = !holdToSprint && SprintToggleOn;
            }
        }

        public void OnJump(InputAction.CallbackContext context) {
            if (!context.performed)
                return;
            JumpPressed = true;
        }

        #endregion
    }
}