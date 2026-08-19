using System;
using System.Collections.Generic;
using UnityEngine;

namespace TopdownRPG.Combat {
    public class DamageDealer : MonoBehaviour {
        // @formatter:off
        [Header("Hit Detection")]
        [SerializeField] private Transform attackPoint;
        [SerializeField] private float attackRange = 1f;
        [SerializeField] private LayerMask enemyLayer;
        // @formatter:on

        // [SerializeField] private float weaponLength;
        // [SerializeField] private float weaponDamage;
        private bool canDealDamage;
        private List<GameObject> hitTargets;

        private void Start() {
            canDealDamage = false;
            hitTargets = new List<GameObject>();
        }

        private void Update() {
            if (!canDealDamage)
                return;
            
        }

        public void StartDealDamage() {
            canDealDamage = true;
            hitTargets.Clear();
        }

        public void EndDealDamage() {
            canDealDamage = false;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!canDealDamage)
                return;

            IDamageable damageable = other.GetComponentInParent<IDamageable>();

            if (damageable == null)
                return;

            GameObject target = damageable.GameObject;

            if (hitTargets.Contains(target))
                return;

            hitTargets.Add(target);
            print(hitTargets);
        }

        // public void CheckHit() {
        //     Collider[] hits = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);
        //     foreach (Collider hit in hits) {
        //         IDamageable damageable = hit.GetComponentInParent<IDamageable>();
        //         if (damageable == null)
        //             continue;
        //
        //         GameObject target = damageable.GameObject;
        //         if (hitTargets.Contains(target))
        //             continue;
        //
        //         hitTargets.Add(target);
        //     }
        // }
        //
        // private void OnDrawGizmosSelected() {
        //     if (attackPoint == null)
        //         return;
        //     Gizmos.color = Color.yellow;
        //     
        //     Gizmos.matrix = Matrix4x4.TRS(attackPoint.position, attackPoint.rotation, Vector3.one);
        //     //
        //     // Gizmos.DrawWireCube(Vector3.forward * attackRange / 2f, boxSize);
        //     // Gizmos.matrix = Matrix4x4.identity;
        // }
    }
}