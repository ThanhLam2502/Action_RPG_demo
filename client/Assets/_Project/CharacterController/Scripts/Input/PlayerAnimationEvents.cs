using UnityEngine;

namespace TopdownRPG.Character
{
    public class PlayerAnimationEvents : MonoBehaviour
    {
        private PlayerCombat _playerCombat;
        private PlayerInteract _playerInteract;

        private void Awake() {
            _playerCombat = GetComponentInParent<PlayerCombat>();
            _playerInteract = GetComponentInParent<PlayerInteract>();
        }

        public void WeaponSwitch() {
            _playerCombat.OnSwitchWeapon();
        }

        public void OnAttackFinished() {
            _playerCombat.OnAttackFinish();
        }


        public void OnGatherFinished() {
            _playerInteract.CompleteGathering();
        }


        protected virtual void FootL() {
        }

        protected virtual void FootR() {
        }
    }
}