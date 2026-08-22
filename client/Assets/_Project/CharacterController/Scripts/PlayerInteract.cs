using System;
using UnityEngine;
using TopdownRPG.Interaction;

namespace TopdownRPG.Character {
    // TODO: chuyển về pure C#
    public class PlayerInteract : MonoBehaviour {
        private PlayerState _playerState;
        private Interactor _interactor;
        private PlayerLocomotionInput _playerLocomotionInput;

        private IInteractable _interactable;

        private void Awake() {
            _playerState = GetComponent<PlayerState>();
            _playerLocomotionInput = GetComponent<PlayerLocomotionInput>();
            _interactor = GetComponent<Interactor>();
        }

        private void Update() {
            if (_playerLocomotionInput.MovementInput != Vector2.zero
                || _playerState.Movement == MovementState.Jumping
                || _playerState.Movement == MovementState.Falling) {
                CancelGathering();
            }
        }

        public void TryGathering() {
            _interactor.TryInteract();
        }

        public void StartGathering(IInteractable interactable) {
            _interactable = interactable;
            _playerState.SetInteraction(InteractionState.Gathering);
        }

        private void CancelGathering() {
            _playerState.SetInteraction(InteractionState.None);
            _interactable = null;
        }

        public void CompleteGathering() {
            _playerState.SetInteraction(InteractionState.None);
            if (_interactable.GameObject) {
                Destroy(_interactable.GameObject);
            }
        }
    }
}