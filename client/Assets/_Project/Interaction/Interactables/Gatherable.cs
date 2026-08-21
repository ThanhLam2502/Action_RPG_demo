using TopdownRPG.Character;
using UnityEngine;

namespace TopdownRPG.Interaction
{
    public class Gatherable : MonoBehaviour
    {
        public string InteractableName => gameObject.name;
        public bool Interact(GameObject interactor)
        {
            Debug.Log($"Gather {gameObject.name}");
            return true;
        }
    }
}