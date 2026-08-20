using TopdownRPG.Character;
using UnityEngine;

namespace TopdownRPG.Combat {
    public class EnemyDamageDealer : MonoBehaviour {
        // @formatter:off
        [Header("Hit Detection")]
        [SerializeField] private int weaponDamage = 10;
        [SerializeField] private float weaponLength = 0.5f;
        [SerializeField] private Transform attackPoint;
        [SerializeField] private LayerMask targetMask;
        // @formatter:on

        private bool _canDealDamage = false;
        private bool _hasDealDamage = false;

        void Start() {
            _canDealDamage = false;
            _hasDealDamage = false;
        }

        void Update() {
            if (!_canDealDamage)
                return;

            CheckHit();
        }

        private void CheckHit() {
            if (_canDealDamage && !_hasDealDamage) {
                RaycastHit hit;
                if (Physics.Raycast(attackPoint.position, attackPoint.up, out hit, weaponLength, targetMask)) {
                    // Debug.Log($"Enemy hit: {hit.collider.name}");
                    hit.collider.TryGetComponent(out IDamageable playerCombat);
                    playerCombat.TakeDamage(weaponDamage, hit.point);
                    // IDamageable pDamageable = hit.collider.GetComponentInParent<IDamageable>();
                    // pDamageable?.TakeDamage(weaponDamage);
                    _hasDealDamage = true;
                }
            }
        }

        public void StartDealDamage() {
            _canDealDamage = true;
            _hasDealDamage = false;
        }

        public void EndDealDamage() {
            _canDealDamage = false;
        }

        private void OnDrawGizmos() {
            if (attackPoint == null)
                return;

            Gizmos.color = _canDealDamage ? Color.red : Color.yellow;
            Gizmos.DrawLine(attackPoint.position, attackPoint.position + attackPoint.up * weaponLength);
        }
    }
}