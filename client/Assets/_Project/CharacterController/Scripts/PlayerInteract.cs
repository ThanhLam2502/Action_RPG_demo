using System;
using UnityEngine;
using TopdownRPG.Interaction;

namespace TopdownRPG.Character
{
    public class PlayerInteract : MonoBehaviour
    {
        #region Class Variable
        [SerializeField] private int _numFound;
        [SerializeField] private float _scanInterval = 0.1f;
        [SerializeField] private float _interactionRadius = 0.2f;
        [SerializeField] private Transform _interactionPoint;
        [SerializeField] private LayerMask _interactionLayer;
        [SerializeField] private GameObject _interactionUIPanel;

        public bool IsGathering { get; private set; }

        private PlayerState _playerState;
        private PlayerActionsInput _playerActionsInput;
        private PlayerLocomotionInput _playerLocomotionInput;

        private float _scanTimer;
        private IInteractable _currentInteractable;
        private readonly Collider[] _colliders = new Collider[8];
        #endregion

        #region Startup
        private void Awake() {
            _playerState = GetComponent<PlayerState>();
            _playerActionsInput = GetComponent<PlayerActionsInput>();
            _playerLocomotionInput = GetComponent<PlayerLocomotionInput>();
        }

        private void OnEnable() {
            _playerActionsInput.GatherPerformed += HandleGather;
        }

        private void OnDisable() {
            _playerActionsInput.GatherPerformed -= HandleGather;
        }
        #endregion

        #region Update Logic
        private void Update() {
            ScanInteractable();

            if (_playerLocomotionInput.MovementInput != Vector2.zero
                || _playerState.CurrentPlayerMovementState == PlayerMovementState.Jumping
                || _playerState.CurrentPlayerMovementState == PlayerMovementState.Falling) {
                CancelGathering();
            }
        }

        private void ScanInteractable() {
            _scanTimer -= Time.deltaTime;
            if (_scanTimer > 0f)
                return;

            _scanTimer = _scanInterval;
            FindCurrentInteractable();
            
            // show Interact Prompt UI
            if (_interactionUIPanel == null)
                return;
            bool shouldShow = _currentInteractable != null;
            if (_interactionUIPanel.activeSelf != shouldShow)
                _interactionUIPanel.SetActive(shouldShow);
        }

        private void FindCurrentInteractable() {
            _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionRadius, _colliders, _interactionLayer);

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

            _currentInteractable = closestInteractable;
        }
        #endregion

        private void HandleGather() {
            if (_currentInteractable == null)
                return;
            // TryInteract
            if (_currentInteractable.Interact(gameObject)) {
                StartGathering();
            }
        }

        private void StartGathering() {
            IsGathering = true;
        }

        private void CancelGathering() {
            IsGathering = false;
        }

        public void CompleteGathering() {
            IsGathering = false;
            if (_currentInteractable is MonoBehaviour interactable) {
                Destroy(interactable.gameObject);
                Debug.Log($"Gather success{gameObject.name}");
            }
            
            _currentInteractable = null;
        }


        #region Debug
        private void OnDrawGizmosSelected() {
            if (!_interactionPoint)
                return;

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(_interactionPoint.position, _interactionRadius);
        }
        #endregion
    }
}