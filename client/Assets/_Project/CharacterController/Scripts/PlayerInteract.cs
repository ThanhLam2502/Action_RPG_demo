using System;
using UnityEngine;
using TopdownRPG.Interaction;

namespace TopdownRPG.Character
{
    public class PlayerInteract : MonoBehaviour
    {
        #region Class Variable
        [SerializeField] private float _interactionRadius = 2f;
        
        private PlayerController _playerController;
        private PlayerActionsInput _playerActionsInput;

        private IInteractable _currentInteractable;
        #endregion

        #region Startup
        private void Awake() {
            _playerController = GetComponent<PlayerController>();
            _playerActionsInput = GetComponent<PlayerActionsInput>();
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
            FindCurrentInteractable();
        }

        private void FindCurrentInteractable() {
            Collider[] colliders = Physics.OverlapSphere(
                transform.position,
                _interactionRadius
            );

            IInteractable closestInteractable = null;
            float closestDistanceSqr = float.MaxValue;

            foreach (Collider col in colliders)
            {
                IInteractable interactable = col.GetComponentInParent<IInteractable>();
                if (interactable == null)
                    continue;

                Vector3 closestPoint = col.ClosestPoint(transform.position);
                float distanceSqr = (closestPoint - transform.position).sqrMagnitude;
                if (distanceSqr < closestDistanceSqr)
                {
                    closestDistanceSqr = distanceSqr;
                    closestInteractable = interactable;
                }
            }
            _currentInteractable = closestInteractable;
        }
        
        private void HandleGather() {
            if (_currentInteractable == null)
                return;
            
            _currentInteractable.Interact(gameObject);
            _currentInteractable = null;
            // ----------------------------- //
            _playerActionsInput.SetGatherPressedTrue();
        }
        #endregion

        #region Debug
        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _interactionRadius);
        }
        #endregion
    }
}