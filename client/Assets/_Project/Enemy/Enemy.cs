using System;
using TopdownRPG.Character;
using TopdownRPG.Combat;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace TopdownRPG.Enemy {
    public class Enemy : MonoBehaviour, IDamageable {
        // @formatter:off
        [SerializeField] private int health = 3;
        
        [Header("Combat")]
        [SerializeField] private float attackCd = 3f;
        [SerializeField] private float attackRange = 1.5f;
        [SerializeField] private float aggroRange = 6f;
        // @formatter:on

        public GameObject GameObject => gameObject;

        private static readonly int SpeedHash = Animator.StringToHash("speed");
        private static readonly int AttackHash = Animator.StringToHash("attack");
        private static readonly int DamageHash = Animator.StringToHash("damage");
        private static readonly int IsDeathHash = Animator.StringToHash("isDeath");

        private GameObject _player;
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        private float _timePassed;
        private float _newDestinationCd = 0.5f;

        #region Startup
        private void Start() {
            _player = GameObject.FindWithTag("Player");
            _animator = GetComponent<Animator>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }
        #endregion

        private void Update() {
            if (_animator.GetBool(IsDeathHash)) {
                return;
            }

            _animator.SetFloat(SpeedHash, _navMeshAgent.velocity.magnitude / _navMeshAgent.speed);
            if (_timePassed >= attackCd) {
                if (Vector3.Distance(_player.transform.position, transform.position) <= attackRange) {
                    _animator.SetTrigger(AttackHash);
                    _timePassed = 0;
                }
            }

            _timePassed += Time.deltaTime;

            if (_newDestinationCd <= 0 && Vector3.Distance(_player.transform.position, transform.position) <= aggroRange) {
                _newDestinationCd = 0.5f;
                _navMeshAgent.SetDestination(_player.transform.position);
            }

            _newDestinationCd -= Time.deltaTime;
            transform.LookAt(_player.transform);
        }


        public void TakeDamage(int damage) {
            health -= damage;
            if (health <= 0) {
                Die();
                return;
            }
            // reset attack 1 phần cooldown khi bị hit (block time attack)ssssssss
            _timePassed = attackCd * 0.5f;
            // animation dmg cuối thì không cần trigger
            _animator.SetTrigger(DamageHash);
        }

        private void Die() {
            _animator.SetBool(IsDeathHash, true);
            Destroy(gameObject, 5f);
        }

        private void OnAttackHit() {
            GetComponentInChildren<EnemyDamageDealer>().StartDealDamage();
        }
        
        private void OnAttackFinish() {
            GetComponentInChildren<EnemyDamageDealer>().EndDealDamage();
        }
        

        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, aggroRange);
        }
    }
}