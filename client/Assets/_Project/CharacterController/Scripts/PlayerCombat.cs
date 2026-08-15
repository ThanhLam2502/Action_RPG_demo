using UnityEngine;

namespace TopdownRPG.Character
{
    public class PlayerCombat : MonoBehaviour
    {
        #region Class Variable
        public bool IsAttacking { get; private set; }
        
        public bool HasSword { get; private set; } = false;
        public bool IsDrawingWeapon { get; private set; }
        public bool IsSheathingWeapon { get; private set; }

        private PlayerActionsInput _playerActionsInput;
        #endregion

        #region Startup
        private void Awake() {
            _playerActionsInput = GetComponent<PlayerActionsInput>();
        }
        
        private void OnEnable() {
            _playerActionsInput.AttackPerformed += HandleAttack;
            _playerActionsInput.SwitchWeaponPerformed += HandleSwitchWeapon;
        }

        private void OnDisable() {
            _playerActionsInput.AttackPerformed -= HandleAttack;
            _playerActionsInput.SwitchWeaponPerformed -= HandleSwitchWeapon;
        }
        #endregion
        
        private void HandleAttack() {
            IsAttacking = true;
        }
        
        public void OnAttackFinish() {
            IsAttacking = true;
        }
        
        private void HandleSwitchWeapon() {
            if (HasSword == false) {
                IsDrawingWeapon = true;
                IsSheathingWeapon = false;
            }
            else {
                IsDrawingWeapon = false;
                IsSheathingWeapon = true;
            }
        }
        
        public void OnSwitchWeapon() {
            HasSword = !HasSword;
        }
    }
}