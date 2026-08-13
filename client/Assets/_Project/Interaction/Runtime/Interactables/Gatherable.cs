using UnityEngine;

namespace TopdownRPG.Interaction
{
    public class Gatherable : MonoBehaviour, IInteractable
    {
        public void Interact(GameObject interactor) {
            Debug.Log($"Gather {gameObject.name}");
        }
        
        public void FinishGather()
        {
            Debug.Log($"Finish Gather {gameObject.name}");
            Destroy(gameObject);
        }
    }
}