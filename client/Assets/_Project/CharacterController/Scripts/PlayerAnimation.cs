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
        private PlayerCombat _playerCombat;
        // private PlayerInteract _playerInteract;
        private PlayerController _playerController;
        private PlayerLocomotionInput _playerLocomotionInput;

        // Locomotion
        private static readonly int InputXHash = Animator.StringToHash("inputX");
        private static readonly int InputYHash = Animator.StringToHash("inputY");
        private static readonly int InputMagnitudeHash = Animator.StringToHash("inputMagnitude");
        private static readonly int IsIdlingHash = Animator.StringToHash("isIdling");
        private static readonly int IsGroundedHash = Animator.StringToHash("isGrounded");
        private static readonly int IsJumpingHash = Animator.StringToHash("isJumping");
        private static readonly int IsFallingHash = Animator.StringToHash("isFalling");
        private static readonly int IsRotatingToTargetHash = Animator.StringToHash("isRotatingToTarget");
        private static readonly int RotationMismatchHash = Animator.StringToHash("rotationMismatch");

        // Actions
        private static readonly int HasSwordHash = Animator.StringToHash("hasSword");
        private static readonly int IsGatheringHash = Animator.StringToHash("isGathering");
        private static readonly int IsAttackingHash = Animator.StringToHash("isAttacking");
        private static readonly int IsDrawingWeaponHash = Animator.StringToHash("isDrawingWeapon");
        private static readonly int IsSheathingWeaponHash = Animator.StringToHash("isSheathingWeapon");
        private static readonly int ComboStepHash = Animator.StringToHash("comboStep");
        private static readonly int IsGetHitHash = Animator.StringToHash("isGetHit");
        

        private static readonly int IsPlayingActionHash = Animator.StringToHash("isPlayingAction");
        private int[] actionHashes;

        private Vector3 _currentBlendInput = Vector3.zero;

        private float _walkMaxBlendValue = 0.5f;
        private float _runMaxBlendValue = 1.0f;
        private float _sprintMaxBlendValue = 1.5f;

        private void Awake() {
            _playerState = GetComponent<PlayerState>();
            _playerCombat = GetComponent<PlayerCombat>();
            // _playerInteract = GetComponent<PlayerInteract>();
            _playerController = GetComponent<PlayerController>();
            _playerLocomotionInput = GetComponent<PlayerLocomotionInput>();

            actionHashes = new int[] { IsGatheringHash, IsDrawingWeaponHash, IsSheathingWeaponHash };
        }

        private void Update() {
            UpdateAnimationState();
        }

        private void UpdateAnimationState() {
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

            _animator.SetBool(IsIdlingHash, isIdling);
            _animator.SetBool(IsGroundedHash, isGrounded);
            _animator.SetBool(IsFallingHash, isFalling);
            _animator.SetBool(IsJumpingHash, isJumping);
            _animator.SetBool(IsRotatingToTargetHash, _playerController.IsRotatingToTarget);

            // Action:
            // -- Non-cancelable Action
            _animator.SetBool(HasSwordHash, _playerCombat.HasSword);
            _animator.SetBool(IsDrawingWeaponHash, _playerCombat.IsDrawingWeapon);
            _animator.SetBool(IsSheathingWeaponHash, _playerCombat.IsSheathingWeapon);

            _animator.SetInteger(ComboStepHash, _playerCombat.ComboStep);
            _animator.SetBool(IsAttackingHash, _playerCombat.AttackPlaying);
            _animator.SetBool(IsGetHitHash, _playerCombat.IsGetHit);
            // -- Action cancellable
            // _animator.SetBool(IsGatheringHash, _playerInteract.IsGathering);
            _animator.SetBool(IsPlayingActionHash, isPlayingAction);

            _animator.SetFloat(InputXHash, _currentBlendInput.x);
            _animator.SetFloat(InputYHash, _currentBlendInput.y);
            _animator.SetFloat(InputMagnitudeHash, _currentBlendInput.magnitude);
            _animator.SetFloat(RotationMismatchHash, _playerController.RotationMismatch);
        }
    }
}