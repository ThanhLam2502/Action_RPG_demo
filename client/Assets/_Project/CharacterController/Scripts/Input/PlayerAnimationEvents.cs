using UnityEngine;

namespace TopdownRPG.Character
{
    public class PlayerAnimationEvents : MonoBehaviour
    {
        private PlayerActionsInput _playerActionsInput;
        private void Awake()
        {
            _playerActionsInput = GetComponentInParent<PlayerActionsInput>();
        }

        public void OnAttackFinished()
        {
            _playerActionsInput.SetAttackPressedFalse();
        }

        public void OnGatherFinished()
        {
            _playerActionsInput.SetGatherPressedFalse();
        }
    }
}