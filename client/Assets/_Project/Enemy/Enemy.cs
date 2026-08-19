using System;
using TopdownRPG.Combat;
using UnityEngine;

namespace TopdownRPG.Enemy {
    public class Enemy : MonoBehaviour, IDamageable {
        [SerializeField] private int health = 3;

        private static readonly int DamageHash = Animator.StringToHash("damage");
        private static readonly int IsDeathHash = Animator.StringToHash("isDeath");
        
        public GameObject GameObject => gameObject;
        
        private GameObject _player;
        private Animator _animator;

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
    }
}