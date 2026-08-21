using System.Collections;
using TopdownRPG.Combat;
using UnityEngine;

namespace TopdownRPG.Character {
    public class PlayerCombat : MonoBehaviour, IDamageable {
        #region Class Variable
        // @formatter:off
        // TODO: tách sang sang health system riêng để tránh chồng chéo
        [SerializeField] private int health = 100;
        [SerializeField] private GameObject getHitVFX;

        [Header("Equipment")] 
        [SerializeField] private GameObject slotL;
        [SerializeField] private GameObject slotR;
        [SerializeField] private GameObject weapon;
        [SerializeField] private GameObject weaponHolder;
        [SerializeField] private float comboWindow = 1.0f;
        // @formatter:on

        public GameObject GameObject => gameObject;

        // 0 = Attack 1; 1 = Attack 2; 2 = Attack 3
        public int ComboStep { get; private set; }
        public bool AttackPlaying { get; private set; }
        public bool HasSword { get; private set; } = false;
        public bool IsDrawingWeapon { get; private set; }
        public bool IsSheathingWeapon { get; private set; }
        public bool IsGetHit { get; private set; }

        private int _attackIndex = 0;
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
            if (!AttackPlaying && ComboStep > 0 && Time.time >= _comboExpireTime) {
                _attackIndex = 0; // Reset Combo
            }
        }
        #endregion

        // TODO: đưa về utils
        private void HitVFX(Vector3 hitPosition) {
            if (getHitVFX == null)
                return;

            GameObject hit = Instantiate(getHitVFX, hitPosition, Quaternion.identity);
            Destroy(hit, 3f);
        }
        
        public void TakeDamage(int damage, Vector3 hitPoint) {
            health -= damage;
            if (health <= 0) {
                Die();
                return;
            }

            IsGetHit = true;

            _attackIndex = 0; // Reset Combo
            
            // -- Game feel
            HitVFX(hitPoint); // animation nhận hit 
            CameraShake.Instance.ShakeCamera(1.2f, 0.2f); // TODO: tách ra hệ thống riêng
            
            // TODO: tạm delay để trigger reset animation, chuyển sang FSM về sau
            _DoDelayAction(0.5f);
        }

        private void Die() {
            Debug.Log("DEATH");
        }

        private void _DoDelayAction(float delayTime) {
            StartCoroutine(_DelayAction(delayTime));
        }

        private IEnumerator _DelayAction(float delayTime) {
            //Wait for the specified delay time before continuing.
            yield return new WaitForSeconds(delayTime);
            IsGetHit = false;
        }

        private void HandleAttack() {
            if (IsDrawingWeapon || IsSheathingWeapon) {
                return;
            }

            if (AttackPlaying) {
                return;
            }

            if (_attackIndex == 0 || Time.time >= _comboExpireTime) {
                _attackIndex = 1;
            } else {
                _attackIndex++;
                // Sau Attack 3 -> quay lại Attack 1
                if (_attackIndex > 3) {
                    _attackIndex = 1;
                }
            }

            PlayAttack(_attackIndex);
        }

        private void PlayAttack(int attackIndex) {
            ComboStep = attackIndex;
            AttackPlaying = true;
        }

        public void OnAttackHit() {
            _damageDealer.StartDealDamage(ComboStep);
            // TODO: handle xử lý system riêng -- test effect combo 3
            if (ComboStep == 3 && HasSword) {
                GameObject prefab = Resources.Load<GameObject>("VFX/Explosion01");
                if (prefab != null) {
                    Vector3 explosionPos = weapon.transform.position + weapon.transform.forward * 1.2f;
                    GameObject explosion = Instantiate(prefab, explosionPos, Quaternion.identity);
                    explosion.transform.localScale = Vector3.one * 0.5f;
                    Destroy(explosion, 1f);
                }
            }

            // -- reset trigger
            AttackPlaying = false;
            _comboExpireTime = Time.time + comboWindow;
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