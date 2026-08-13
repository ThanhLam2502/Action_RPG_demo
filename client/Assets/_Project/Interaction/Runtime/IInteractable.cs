using UnityEngine;

namespace TopdownRPG.Interaction
{
    public interface IInteractable
    {
        public string InteractableName { get; }
        public bool Interact(GameObject interactor);
    }
}