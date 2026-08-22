using System;
using TMPro;
using UnityEngine;

namespace TopdownRPG.Interaction {
    public class InteractableUI : MonoBehaviour {
        private TextMeshProUGUI _textMeshPro;
        private Transform _cameraTransform;

        private const float UIOffset = 0.2f;
        

        void Start() {
            _cameraTransform = Camera.main?.transform;
            _textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
            if (_textMeshPro == null)
                Debug.LogError($"{name}: Cannot find TextMeshProUGUI in children.");

            Hide();
        }

        public void Show(string displayName, string action) {
            _textMeshPro.text = $"{displayName}\n [E] {action}";
        }

        public void Hide() {
            _textMeshPro.text = "";
        }

        public void SetInteractableNamePosition(IInteractable interactable) {
            GameObject interactableObject = interactable.GameObject;
            Collider colliderComponent = interactableObject.GetComponent<Collider>();
            if (!colliderComponent) {
                Debug.LogWarning($"InteractableUI: {interactableObject.name} doesn't have a Collider component.");
                return;
            }

            transform.position = new Vector3(
                interactableObject.transform.position.x,
                colliderComponent.bounds.max.y + UIOffset,
                interactableObject.transform.position.z
            );
            transform.LookAt(2f * transform.position - (_cameraTransform.position));
        }
    }
}