using TopdownRPG.Character;
using UnityEngine;

namespace TopdownRPG.Interaction
{
    public class Gatherable : MonoBehaviour, IInteractable
    {
        public string InteractableName => gameObject.name;
        public bool Interact(GameObject interactor)
        {
            var PlayerInteract = interactor.GetComponent<PlayerInteract>();
            Debug.Log($"Gather {gameObject.name}");
            PlayerInteract.HandleGatherStart();
            return true;
        }
    }
}