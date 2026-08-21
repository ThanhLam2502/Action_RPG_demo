using System;
using UnityEngine;

namespace TopdownRPG.Interaction {
    public abstract class Interactable : MonoBehaviour, IInteractable {
        // @formatter:off
        [Header("Interaction Data")]
        [SerializeField] private bool canInteract = true;
        // @formatter:on
        
        public bool CanInteract => canInteract;
        protected string InteractableName = "";
        protected float InteractionDistance = 2f;
        protected GameObject InteractableNameCanvas;
        // protected InteractableNameText InteractableNameText;

        public virtual void Start() {
            // InteractableNameCanvas = GameObject.FindGameObjectWithTag("Canvas");
            // InteractableNameText = InteractableNameCanvas.GetComponentInChildren<InteractableNameText>();
        }

        private void OnDestroy() {
            TargetOff();
        }

        public virtual void TargetOn() {
            // Enable outline / highlight
            // InteractableNameText.ShowText(this);
            // InteractableNameText.SetInteractableNamePosition(this);
        }

        public virtual void TargetOff() {
            // Enable outline / highlight
            // InteractableNameText.HideText();
        }

        public abstract void Interact(Interactor interactor);
    }
}