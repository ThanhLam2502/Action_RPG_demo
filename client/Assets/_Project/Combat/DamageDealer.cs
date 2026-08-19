using System;
using System.Collections.Generic;
using UnityEngine;

namespace TopdownRPG.Combat {
    public class DamageDealer : MonoBehaviour {
        // @formatter:off
        [Header("Hit Shape")]
        [SerializeField] private Transform attackPoint;
        [SerializeField] private Vector3 boxSize = new(0.5f, 0.5f, 1.5f);
        
        [Header("Hit Detection")]
        [SerializeField] private LayerMask targetMask;
        // @formatter:on

        private bool canDealDamage;
        private readonly HashSet<GameObject> hitTargets = new();
        private readonly Collider[] hitBuffer = new Collider[16];

        private void Start() {
            canDealDamage = false;
        }

        private void Update() {
            if (!canDealDamage)
                return;

            CheckHit();
        }

        public void StartDealDamage() {
            canDealDamage = true;
            hitTargets.Clear();
        }

        public void EndDealDamage() {
            canDealDamage = false;
        }

        private void CheckHit() {
            var hitCount = Physics.OverlapBoxNonAlloc(attackPoint.position, boxSize * 0.5f, hitBuffer, attackPoint.rotation, targetMask);

            for (int i = 0; i < hitCount; i++) {
                Collider hit = hitBuffer[i];
                IDamageable damageable = hit.GetComponent<IDamageable>();
                if (damageable == null)
                    continue;
                
                Debug.Log($"Name: {damageable.GameObject.name}");
                if (hitTargets.Contains(hit.gameObject))
                    continue;
                
                hitTargets.Add(hit.gameObject);

                damageable.TakeDamage(1); // apply damage
            }
        }
        
        // private void OnDrawGizmosSelected() {
        private void OnDrawGizmos() {
            if (attackPoint == null)
                return;

            Gizmos.matrix = Matrix4x4.TRS(attackPoint.position, attackPoint.rotation, Vector3.one);
            Gizmos.color = canDealDamage ? Color.red : Color.yellow;
            Gizmos.DrawWireCube(Vector3.zero, boxSize);
        }
    }
}