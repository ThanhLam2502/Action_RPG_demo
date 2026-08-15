using UnityEngine;

namespace TopdownRPG.Character {
    public class PlayerCombat : MonoBehaviour {
        #region Class Variable
        [SerializeField] private GameObject weapon;
        [SerializeField] private GameObject weaponHolder;
        
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

        private Coroutine _attackFinishCoroutine;

        private void HandleAttack() {
            if (IsAttacking)
                return;
            if (IsDrawingWeapon || IsSheathingWeapon)
                return;

            IsAttacking = true;
        }

        public void OnAttackFinish() {  
            IsAttacking = false;
        }

        private void HandleSwitchWeapon() {
            if (IsDrawingWeapon || IsSheathingWeapon)
                return;

            if (HasSword) {
                IsSheathingWeapon = true;
            } else {
                IsDrawingWeapon = true;
            }
        }

        public void OnSwitchWeapon() {
            HasSword = !HasSword;

            // Active weapon
            weapon.SetActive(HasSword);
            weaponHolder.SetActive(!HasSword);
            
            // reset animation state
            IsDrawingWeapon = false;
            IsSheathingWeapon = false;
        }
    }
}