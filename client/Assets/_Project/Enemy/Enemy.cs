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
        [SerializeField] private GameObject getHitVFX;
        
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
            if (_player == null || _animator.GetBool(IsDeathHash)) {
                return;
            }

            _animator.SetFloat(SpeedHash, _navMeshAgent.velocity.magnitude / _navMeshAgent.speed);
            if (_timePassed >= attackCd && Vector3.Distance(_player.transform.position, transform.position) <= attackRange) {
                _animator.SetTrigger(AttackHash);
                _timePassed = 0;
            }

            _timePassed += Time.deltaTime;

            if (_newDestinationCd <= 0 && Vector3.Distance(_player.transform.position, transform.position) <= aggroRange) {
                Vector3 destination = _player.transform.position;
                destination.y = transform.position.y;
                _navMeshAgent.SetDestination(destination);
            }

            _newDestinationCd -= Time.deltaTime;
            
            // look player
            // transform.LookAt(_player.transform);
            /*Vector3 direction = _player.transform.position - transform.position;
            direction.y = 0f;
            if (direction.sqrMagnitude > 0.001f)
                transform.rotation = Quaternion.LookRotation(direction);*/
        }
        
        private void OnAttackHit() {
            GetComponentInChildren<EnemyDamageDealer>().StartDealDamage();
        }

        private void OnAttackFinish() {
            _navMeshAgent.isStopped = false;
            GetComponentInChildren<EnemyDamageDealer>().EndDealDamage();
        }

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

            // reset attack 1 phần cooldown khi bị hit (block time attack)
            _timePassed = attackCd * 0.5f;
            // animation dmg cuối thì không cần trigger
            _animator.SetTrigger(DamageHash);
            HitVFX(hitPoint);
        }

        private void Die() {
            _animator.SetBool(IsDeathHash, true);
            Destroy(gameObject, 5f);
        }


        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, aggroRange);
        }
    }
}