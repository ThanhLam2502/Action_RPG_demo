using UnityEngine;

namespace TopdownRPG.Interaction
{
    public interface IInteractable
    {
        public void Interact(GameObject interactor);
    }
}