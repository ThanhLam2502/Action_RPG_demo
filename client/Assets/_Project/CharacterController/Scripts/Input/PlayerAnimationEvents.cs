using UnityEngine;

namespace TopdownRPG.Character {
    public sealed class PlayerAnimationEvents : MonoBehaviour {
        private PlayerCombat _playerCombat;
        // private PlayerInteract _playerInteract;

        private void Awake() {
            _playerCombat = GetComponentInParent<PlayerCombat>();
            // _playerInteract = GetComponentInParent<PlayerInteract>();
        }

        public void WeaponSwitch() {
            _playerCombat.OnSwitchWeapon();
        }

        public void Hit() {
            _playerCombat.OnAttackHit();
        }

        public void AttackFinished() {
            _playerCombat.OnAttackFinish();
        }

        public void OnGatherFinished() {
            // _playerInteract.CompleteGathering();
        }

        private void FootL() {
        }

        private void FootR() {
        }
    }
}