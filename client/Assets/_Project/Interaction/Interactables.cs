using System;
using UnityEngine;

namespace TopdownRPG.Interaction {
    public abstract class Interactable : MonoBehaviour, IInteractable {
        // @formatter:off
        [Header("Interaction Data")]
        [SerializeField] private bool canInteract = true;
        [SerializeField] private string displayName;
        
        [Header("Highlight")]
        [SerializeField] private GameObject highlight;
        // @formatter:on

        public bool CanInteract => canInteract;
        public GameObject GameObject => gameObject;
        public string DisplayName => displayName;
        public abstract string InteractionAction { get; }

        private GameObject _interactableNameCanvas;
        private InteractableUI _interactableUI;

        protected virtual void Awake() {
            HighlightOff();
        }

        public virtual void Start() {
            _interactableNameCanvas = GameObject.FindGameObjectWithTag("Canvas");
            _interactableUI = _interactableNameCanvas.GetComponentInChildren<InteractableUI>();
        }

        private void OnDestroy() {
            TargetOff();
            HighlightOff();
        }

        public virtual void HighlightOn() {
            if (highlight != null)
                highlight.SetActive(true);
        }   

        public virtual void HighlightOff() {
            if (highlight != null)
                highlight.SetActive(false);
        }
        
        public virtual void TargetOn() {
            _interactableUI.Show(DisplayName, InteractionAction);
            _interactableUI.SetInteractableNamePosition(this);
        }

        public virtual void TargetOff() {
            _interactableUI.Hide();
        }

        public abstract void Interact(Interactor interactor);
    }
}