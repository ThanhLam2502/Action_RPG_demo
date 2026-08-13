using System.Linq;
using UnityEngine;
using _Project.CharactorController;

namespace TopdownRPG.Character
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float locomotionBlendSpeed = 0.2f;

        private PlayerState _playerState;
        private PlayerInteract _playerInteract;
        private PlayerController _playerController;
        private PlayerActionsInput _playerActionsInput;
        private PlayerLocomotionInput _playerLocomotionInput;

        // Locomotion
        private static int inputXHash = Animator.StringToHash("inputX");
        private static int inputYHash = Animator.StringToHash("inputY");
        private static int inputMagnitudeHash = Animator.StringToHash("inputMagnitude");
        private static int isIdlingHash = Animator.StringToHash("isIdling");
        private static int isGrounedHash = Animator.StringToHash("isGrounded");
        private static int isJumpingHash = Animator.StringToHash("isJumping");
        private static int isFallingHash = Animator.StringToHash("isFalling");
        private static int isRotatingToTargetHash = Animator.StringToHash("isRotatingToTarget");
        private static int rotationMismatchHash = Animator.StringToHash("rotationMismatch");

        // Actions
        private static int isAttackingHash = Animator.StringToHash("isAttacking");
        private static int isGatheringHash = Animator.StringToHash("isGathering");

        private static int isPlayingActionHash = Animator.StringToHash("isPlayingAction");
        private int[] actionHashes;

        private Vector3 _currentBlendInput = Vector3.zero;

        private float _walkMaxBlendValue = 0.5f;
        private float _runMaxBlendValue = 1.0f;
        private float _sprintMaxBlendValue = 1.5f;

        private void Awake()
        {
            _playerState = GetComponent<PlayerState>();
            _playerInteract = GetComponent<PlayerInteract>();
            _playerController = GetComponent<PlayerController>();
            _playerActionsInput = GetComponent<PlayerActionsInput>();
            _playerLocomotionInput = GetComponent<PlayerLocomotionInput>();

            actionHashes = new int[] { isGatheringHash };
        }

        private void Update()
        {
            UpdateAnimationState();
        }

        private void UpdateAnimationState()
        {
            bool isIdling = _playerState.CurrentPlayerMovementState == PlayerMovementState.Idling;
            bool isRunning = _playerState.CurrentPlayerMovementState == PlayerMovementState.Running;
            bool isSprinting = _playerState.CurrentPlayerMovementState == PlayerMovementState.Sprinting;
            bool isJumping = _playerState.CurrentPlayerMovementState == PlayerMovementState.Jumping;
            bool isFalling = _playerState.CurrentPlayerMovementState == PlayerMovementState.Falling;
            bool isGrounded = _playerState.InGroundedState();
            bool isPlayingAction = actionHashes.Any(hash => _animator.GetBool(hash));

            bool isRunBlendValue = isRunning || isJumping || isFalling;

            Vector2 inputTarget = isSprinting ? _playerLocomotionInput.MovementInput * _sprintMaxBlendValue
                : isRunBlendValue ? _playerLocomotionInput.MovementInput * _runMaxBlendValue
                : _playerLocomotionInput.MovementInput * _walkMaxBlendValue;
            _currentBlendInput = Vector3.Lerp(_currentBlendInput, inputTarget, locomotionBlendSpeed * Time.deltaTime);

            _animator.SetBool(isIdlingHash, isIdling);
            _animator.SetBool(isGrounedHash, isGrounded);
            _animator.SetBool(isFallingHash, isFalling);
            _animator.SetBool(isJumpingHash, isJumping);
            _animator.SetBool(isRotatingToTargetHash, _playerController.IsRotatingToTarget);
            _animator.SetBool(isAttackingHash, _playerActionsInput.AttackPressed);
            _animator.SetBool(isGatheringHash, _playerInteract.IsGathering);
            _animator.SetBool(isPlayingActionHash, isPlayingAction);

            _animator.SetFloat(inputXHash, _currentBlendInput.x);
            _animator.SetFloat(inputYHash, _currentBlendInput.y);
            _animator.SetFloat(inputMagnitudeHash, _currentBlendInput.magnitude);
            _animator.SetFloat(rotationMismatchHash, _playerController.RotationMismatch);
        }
    }
}