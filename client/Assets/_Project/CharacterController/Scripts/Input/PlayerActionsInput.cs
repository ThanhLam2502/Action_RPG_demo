using System;
using _Project.CharactorController;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TopdownRPG.Character
{
    [DefaultExecutionOrder(-2)]
    public class PlayerActionsInput : MonoBehaviour, PlayerControls.IPlayerActionMapActions
    {
        #region Class Variable
        public bool AttackPressed { get; private set; }
        public event Action GatherPerformed;
        #endregion

        #region Startup
        private void OnEnable() {
            if (PlayerInputManager.Instance?.PlayerControls == null) {
                Debug.LogError("Player controls is not initialized - cannot enable");
                return;
            }

            PlayerInputManager.Instance.PlayerControls.PlayerActionMap.Enable();
            PlayerInputManager.Instance.PlayerControls.PlayerActionMap.SetCallbacks(this);
        }

        private void OnDisable() {
            if (PlayerInputManager.Instance?.PlayerControls == null) {
                Debug.LogError("Player controls is not initialized - cannot disable");
                return;
            }

            PlayerInputManager.Instance.PlayerControls.PlayerActionMap.Disable();
            PlayerInputManager.Instance.PlayerControls.PlayerActionMap.RemoveCallbacks(this);
        }
        #endregion

        #region Update Logic
        public void SetAttackPressedFalse() {
            AttackPressed = false;
        }
        #endregion

        #region Input Callback
        public void OnAttack(InputAction.CallbackContext context) {
            if (!context.performed)
                return;
            AttackPressed = true;
        }

        public void OnGather(InputAction.CallbackContext context) {
            if (!context.performed)
                return;

            GatherPerformed?.Invoke();
        }
        #endregion
    }
}