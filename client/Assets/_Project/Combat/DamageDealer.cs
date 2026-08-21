using System;
using System.Collections.Generic;
using UnityEngine;

namespace TopdownRPG.Combat {
    public class DamageDealer : MonoBehaviour {
        // @formatter:off
        [SerializeField] private int weaponDamage = 10;
        [SerializeField] private Transform attackPoint;
        
        [Header("Hit Detection")]
        [SerializeField] private Vector3 boxSize = new(0.5f, 0.5f, 1.5f);
        [SerializeField] private LayerMask targetMask;
        // @formatter:on

        private int _attackIndex = 0;
        private bool _canDealDamage;
        private readonly HashSet<GameObject> _hitTargets = new();
        private readonly Collider[] _hitBuffer = new Collider[16];

        private void Start() {
            _canDealDamage = false;
        }

        private void Update() {
            if (!_canDealDamage)
                return;

            CheckHit();
        }

        public void StartDealDamage(int attackIndex) {
            _attackIndex = attackIndex;
            _canDealDamage = true;
            _hitTargets.Clear();
        }

        public void EndDealDamage() {
            _canDealDamage = false;
        }

        private void CheckHit() {
            var hitCount = Physics.OverlapBoxNonAlloc(attackPoint.position, boxSize * 0.5f, _hitBuffer, attackPoint.rotation, targetMask);

            for (int i = 0; i < hitCount; i++) {
                Collider hit = _hitBuffer[i];
                IDamageable damageable = hit.GetComponent<IDamageable>();
                if (damageable == null)
                    continue;

                // hit.transform.TryGetComponent(out IDamageable damageableComponent);
                // Debug.Log($"Name: {damageable.GameObject.name}");
                if (!_hitTargets.Add(damageable.GameObject))
                    continue;

                // TODO: test thui
                Vector3 hitPoint = hit.ClosestPoint(attackPoint.position);
                if (_attackIndex == 3) {
                    // -- Game feel Heavy
                    CameraShake.Instance.ShakeCamera(1.8f, 0.14f);
                    damageable.TakeDamage(weaponDamage * 3, hitPoint);
                } else {
                    // -- Game feel Normal
                    CameraShake.Instance.ShakeCamera(0.8f, 0.10f);
                    damageable.TakeDamage(weaponDamage, hitPoint); // apply damage
                }
            }
        }

        // private void OnDrawGizmosSelected() {
        private void OnDrawGizmos() {
            if (attackPoint == null)
                return;

            Gizmos.matrix = Matrix4x4.TRS(attackPoint.position, attackPoint.rotation, Vector3.one);
            Gizmos.color = _canDealDamage ? Color.red : Color.yellow;
            Gizmos.DrawWireCube(Vector3.zero, boxSize);
        }
    }
}