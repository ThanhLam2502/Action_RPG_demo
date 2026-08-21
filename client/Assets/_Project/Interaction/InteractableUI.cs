using System;
using TMPro;
using UnityEngine;

namespace TopdownRPG.Interaction {
    public class InteractableUI : MonoBehaviour {
        private TextMeshProUGUI _textMeshPro;
        private Transform _cameraTransform;

        private void Awake() {
            
        }

        void Start() {
            _cameraTransform = Camera.main?.transform;
            _textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
            if (_textMeshPro == null)
                Debug.LogError($"{name}: Cannot find TextMeshProUGUI in children.");
            
            Hide();
        }

        void Update() {
        }

        public void Show(string displayName, string action) {
            _textMeshPro.text = $"{displayName}\n [E] {action}";
        }

        public void Hide() {
            _textMeshPro.text = "";
        }

        public void SetInteractableNamePosition(IInteractable interactable) {
            GameObject interactableObject = interactable.GameObject;
            // Collider collider = interactableObject.GetComponent<Collider>();

            if (interactableObject.TryGetComponent(out BoxCollider boxCollider)) {
                transform.position = interactableObject.transform.position + Vector3.up * boxCollider.bounds.size.y * 0.5f;
                transform.LookAt(2 * transform.position - _cameraTransform.position);
            } else if (interactableObject.TryGetComponent(out CapsuleCollider capsuleCollider)) {
                transform.position = interactableObject.transform.position + Vector3.up * capsuleCollider.height;
                transform.LookAt(2 * transform.position - _cameraTransform.position);
            } else {
                Debug.LogWarning("InteractableUI doesn't have a Collider component");
            }
        }
    }
}