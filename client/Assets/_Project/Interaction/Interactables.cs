using System;
using UnityEngine;

namespace TopdownRPG.Interaction {
    public abstract class Interactable : MonoBehaviour, IInteractable {
        // @formatter:off
        [Header("Interaction Data")]
        [SerializeField] private bool canInteract = true;
        [SerializeField] private string displayName;
        
        // [Header("UI")]
        // [SerializeField] private InteractableUI interactableUI;
        // @formatter:on

        public bool CanInteract => canInteract;
        public GameObject GameObject => gameObject;
        public string DisplayName => displayName;
        public abstract string InteractionAction { get; }

        private GameObject InteractableNameCanvas;
        private InteractableUI InteractableUI;

        protected virtual void Awake() {
            // if (interactableUI == null)
            //     interactableUI = GetComponentInChildren<InteractableUI>();
        }

        public virtual void Start() {
            InteractableNameCanvas = GameObject.FindGameObjectWithTag("Canvas");
            InteractableUI = InteractableNameCanvas.GetComponentInChildren<InteractableUI>();
        }

        private void OnDestroy() {
            TargetOff();
        }

        public virtual void TargetOn() {
            print("Target On");
            // Enable outline / highlight
            InteractableUI.Show(DisplayName, InteractionAction);
            InteractableUI.SetInteractableNamePosition(this);
        }

        public virtual void TargetOff() {
            print("Target Off");
            // Enable outline / highlight
            InteractableUI.Hide();
        }

        public abstract void Interact(Interactor interactor);
    }
}