using UnityEngine;

namespace TopdownRPG.Interaction {
    public interface IInteractable {
        GameObject GameObject { get; }
        string DisplayName { get; }
        string InteractionAction { get; }
        
        void HighlightOn();
        void HighlightOff();

        void TargetOn();
        void TargetOff();
        
        void Interact(Interactor interactor);
    }
}