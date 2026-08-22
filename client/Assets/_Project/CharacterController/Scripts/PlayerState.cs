using System;
using UnityEngine;

namespace TopdownRPG.Character {
    public class PlayerState : MonoBehaviour {
        // -- movement
        [field: SerializeField] public MovementState Movement { get; private set; } = MovementState.Idling;

        public void SetMovement(MovementState state) => Movement = state;

        public bool IsGrounded() {
            return IsGroundedState(Movement);
        }

        public bool IsGroundedState(MovementState movementState) {
            return movementState == MovementState.Idling ||
                   movementState == MovementState.Walking ||
                   movementState == MovementState.Running ||
                   movementState == MovementState.Sprinting;
        }

        // -- Interaction
        [field: SerializeField] public InteractionState Interaction { get; private set; } = InteractionState.None;
        public void SetInteraction(InteractionState state) => Interaction = state;
    }

    public enum MovementState {
        Idling = 0,
        Walking = 1,
        Running = 2,
        Sprinting = 3,
        Jumping = 4,
        Falling = 5,
        Strafing = 6,
    }

    public enum InteractionState {
        None = 0,
        Gathering = 1,
    }
}