using System;
using UnityEngine;

namespace TopdownRPG.Character {
    public class PlayerCombat : MonoBehaviour {
        #region Class Variable
        [SerializeField] private GameObject weapon;
        [SerializeField] private GameObject weaponHolder;
        [SerializeField] private float comboWindow = 2f;

        public bool IsAttacking { get; private set; }
        public bool HasSword { get; private set; } = false;
        public bool IsDrawingWeapon { get; private set; }

        public bool IsSheathingWeapon { get; private set; }

        // 0 = Attack 1; 1 = Attack 2; 2 = Attack 3
        public int ComboStep { get; private set; }

        private float _comboExpireTime;
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

        #region Update Logic
        private void Update() {
            if (!IsAttacking && ComboStep > 0 && Time.time >= _comboExpireTime) {
                // Reset Combo
                ComboStep = 0;
            }
        }
        #endregion

        private void TryAttack() {
            // Combo đã hết thời gian → Attack 1
            if (ComboStep == 0 || Time.time >= _comboExpireTime) {
                ComboStep = 1;
            } else {
                ComboStep++;

                // Sau Attack 3 -> quay lại Attack 1
                if (ComboStep > 3) {
                    ComboStep = 1;
                }
            }

            IsAttacking = true;
        }

        private void HandleAttack() {
            if (IsDrawingWeapon || IsSheathingWeapon) {
                return;
            }

            if (IsAttacking) {
                Debug.Log($"Block step: {ComboStep}");
                return;
            }

            TryAttack();
        }

        public void OnAttackHit() {
            // Cho phép bắt đâù trigger mới
            IsAttacking = false;
            // Bắt đầu tính combo từ Hit
            _comboExpireTime = Time.time + comboWindow;
        }

        public void OnAttackFinish() {
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