using TopdownRPG.Character;
using UnityEngine;

namespace TopdownRPG.Interaction
{
    public class Gatherable : MonoBehaviour, IInteractable
    {
        public string InteractableName => gameObject.name;
        public bool Interact(GameObject interactor)
        {
            var playerInteract = interactor.GetComponent<PlayerInteract>();
            Debug.Log($"Gather {gameObject.name}");
            playerInteract.HandleGatherStart();
            return true;
        }
    }
}