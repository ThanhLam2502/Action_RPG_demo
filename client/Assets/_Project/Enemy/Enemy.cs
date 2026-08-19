using System;
using TopdownRPG.Combat;
using UnityEngine;
using UnityEngine.AI;

namespace TopdownRPG.Enemy {
    public class Enemy : MonoBehaviour, IDamageable {
        // @formatter:off
        [SerializeField] private int health = 3;
        
        [Header("Combat")]
        [SerializeField] private float attackCD = 3f;
        [SerializeField] private float attackRange = 1f;
        [SerializeField] private float aggroRange = 4f;
        // @formatter:on
        
        public GameObject GameObject => gameObject;
        
        private static readonly int DamageHash = Animator.StringToHash("damage");
        private static readonly int IsDeathHash = Animator.StringToHash("isDeath");
        
        private GameObject _player;
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        private float _timePassed;
        private float _newDestinationCD = 0.5f;

        private void Start() {
            _player = GameObject.FindWithTag("Player");
            _animator = GetComponent<Animator>();
        }

        public void TakeDamage(int damage) {
            health -= damage;
            _animator.SetTrigger(DamageHash);

            if (health <= 0) {
                Die();
            }
        }

        private void Die() {
            _animator.SetBool(IsDeathHash, true);
            Destroy(gameObject, 2f);
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, aggroRange);
        }
    }
}