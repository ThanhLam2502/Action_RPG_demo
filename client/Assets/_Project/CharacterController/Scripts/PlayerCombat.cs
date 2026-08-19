using System;
using TopdownRPG.Combat;
using UnityEngine;

namespace TopdownRPG.Character {
    public class PlayerCombat : MonoBehaviour {
        #region Class Variable
        [SerializeField] private GameObject slotL;
        [SerializeField] private GameObject slotR;
        [SerializeField] private GameObject weapon;
        [SerializeField] private GameObject weaponHolder;
        [SerializeField] private float comboWindow = 1.5f;

        public bool AttackRequested { get; private set; }
        public bool HasSword { get; private set; } = false;
        public bool IsDrawingWeapon { get; private set; }

        public bool IsSheathingWeapon { get; private set; }

        // 0 = Attack 1; 1 = Attack 2; 2 = Attack 3
        public int ComboStep { get; private set; }
        
        private float _comboExpireTime;
        private DamageDealer _damageDealer;
        private PlayerActionsInput _playerActionsInput;
        #endregion

        #region Startup
        private void Awake() {
            // TODO: măc định lấy của Unarmed; khi trang bị chuyển sang lấy từ Weapon
            _damageDealer = weapon.GetComponent<DamageDealer>();
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
            if (!AttackRequested && ComboStep > 0 && Time.time >= _comboExpireTime) {
                // Reset Combo
                ComboStep = 0;
            }
        }
        #endregion

        private void HandleAttack() {
            if (IsDrawingWeapon || IsSheathingWeapon) {
                return;
            }

            if (AttackRequested) {
                Debug.Log($"Block step: {ComboStep}");
                return;
            }

            // Combo đã hết thời gian -> Attack 1
            if (ComboStep == 0 || Time.time >= _comboExpireTime) {
                ComboStep = 1;
            } else {
                ComboStep++;
                // Sau Attack 3 -> quay lại Attack 1
                if (ComboStep > 3) {
                    ComboStep = 1;
                }
            }

            AttackRequested = true;
        }

        public void OnAttackHit() {
            _damageDealer.StartDealDamage();

            AttackRequested = false; // Cho phép bắt đâù trigger mới
            _comboExpireTime = Time.time + comboWindow; // Bắt đầu tính combo từ Hit
        }

        public void OnAttackFinish() {
            _damageDealer.EndDealDamage();
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

            if (HasSword) {
                if (weapon == null)
                    LoadWeapon();
                // TODO: set _damageDealer = Weapon
                weapon.SetActive(true);
            } else {
                // TODO: set _damageDealer = Unarmed
                weapon.SetActive(false);
            }
            weaponHolder.SetActive(!HasSword);
            
            // reset animation state
            IsDrawingWeapon = false;
            IsSheathingWeapon = false;
        }

        private void LoadWeapon() {
            // GameObject prefab = Resources.Load<GameObject>("Weapons/Prefab/2Hand_Sword");
            // if (prefab == null) {
            //     Debug.LogError("[PlayerCombat] Cannot find weapon prefab: " + "Resources/Weapons/Prefab/2Hand_Sword");
            //     return;
            // }
            //
            // weapon = Instantiate(prefab, slotR.transform);
            // weapon.transform.localPosition = Vector3.zero;
            // weapon.transform.localRotation = Quaternion.identity;
            // weapon.transform.localScale = Vector3.one;
        }
    }
}