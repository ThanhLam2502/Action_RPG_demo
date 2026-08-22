using System;
using UnityEngine;

namespace TopdownRPG.Interaction {
    public class Interactor : MonoBehaviour {
        #region Class Variable
        // @formatter:off
        [Header("Detection")]
        [SerializeField] private float _maxInteractDistance = 10f;
        [SerializeField] private float _interactDistance = 1f;
        [SerializeField] private float _scanInterval = 0.1f;
        [SerializeField] private LayerMask _interactLayer;

        [Header("Reference")]
        [SerializeField] private Transform _interactionPoint;
        // @formatter:on

        private int _numFound;
        private float _scanTimer;
        private readonly Collider[] _colliders = new Collider[16]; // TODO: mở rộng nếu cần thiết

        private IInteractable _interactableTarget;
        #endregion

        #region Unity Lifecycle
        private void Awake() {
        }
        #endregion

        #region Update Logic
        private void Update() {
            _scanTimer -= Time.deltaTime;

            if (_scanTimer <= 0f) {
                _scanTimer = _scanInterval;
                FindCurrentInteractable();
            }
        }
        #endregion

        private void FindCurrentInteractable() {
            _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _maxInteractDistance, _colliders, _interactLayer);

            IInteractable closestInteractable = null;
            float closestDistanceSqr = float.MaxValue;

            for (int i = 0; i < _numFound; i++) {
                IInteractable interactable = _colliders[i].GetComponentInParent<IInteractable>();
                if (interactable == null)
                    continue;

                Vector3 closestPoint = _colliders[i].ClosestPoint(_interactionPoint.position);
                float distanceSqr = (closestPoint - _interactionPoint.position).sqrMagnitude;

                if (distanceSqr < closestDistanceSqr) {
                    closestDistanceSqr = distanceSqr;
                    closestInteractable = interactable;
                }
            }

            // -- không tìm thấy - clear
            if (closestInteractable == null) {
                if (_interactableTarget != null) {
                    _interactableTarget.TargetOff();
                    _interactableTarget.HighlightOff();
                    _interactableTarget = null;
                }

                return;
            }

            // -- Change interactable
            if (_interactableTarget != closestInteractable) {
                // disable old target
                if (_interactableTarget != null) {
                    _interactableTarget.TargetOff();
                    _interactableTarget.HighlightOff();
                }

                // change target
                _interactableTarget = closestInteractable;
                _interactableTarget.HighlightOn();
            }
            
            // TODO: chỉ gọi TargetOn/TargetOff khi thực sự có thay đổi
            // Check distance target & update target state
            float interactDistanceSqr = _interactDistance * _interactDistance;
            if (closestDistanceSqr <= interactDistanceSqr) {
                _interactableTarget?.TargetOn();
            } else {
                _interactableTarget?.TargetOff();
            }
        }

        private void TryInteract() {
            if (_interactableTarget == null)
                return;

            float interactDistanceSqr = _interactDistance * _interactDistance;
            _interactableTarget.GameObject.TryGetComponent(out Collider targetCollider);
            if (targetCollider == null)
                return;

            Vector3 closestPoint = targetCollider.ClosestPoint(_interactionPoint.position);
            float distanceSqr = (closestPoint - _interactionPoint.position).sqrMagnitude;
            if (distanceSqr <= interactDistanceSqr) {
                _interactableTarget.Interact(this);
            }
        }

        #region Debug
        private void OnDrawGizmosSelected() {
            if (!_interactionPoint)
                return;

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(_interactionPoint.position, _maxInteractDistance);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_interactionPoint.position, _interactDistance);
        }
        #endregion
    }
}