using UnityEngine;

namespace TopdownRPG.Character
{
    public class PlayerAnimationEvents : MonoBehaviour
    {
        private PlayerActionsInput _playerActionsInput;
        private PlayerInteract _playerInteract;
        
        private void Awake()
        {
            _playerActionsInput = GetComponentInParent<PlayerActionsInput>();
            _playerInteract = GetComponentInParent<PlayerInteract>();
        }

        public void OnAttackFinished()
        {
            _playerActionsInput.SetAttackPressedFalse();
        }

        public void OnGatherFinished()
        {
            _playerInteract.CompleteGathering();
        }
    }
}