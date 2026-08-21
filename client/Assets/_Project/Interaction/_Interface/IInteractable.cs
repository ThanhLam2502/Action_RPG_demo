using UnityEngine;

namespace TopdownRPG.Interaction {
    public interface IInteractable {
        // string InteractableName { get; }

        void TargetOn();
        void TargetOff();
        void Interact(Interactor interactor);
    }
}