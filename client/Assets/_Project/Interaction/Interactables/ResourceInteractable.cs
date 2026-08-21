using TopdownRPG.Character;
using UnityEngine;


namespace TopdownRPG.Interaction {
    public class ResourceInteractable : Interactable {
        // @formatter:off
        [Header("Resource")]
        [SerializeField] private string interactionAction = "Gather";
        // @formatter:on

        public override string InteractionAction => interactionAction;

        public override void Interact(Interactor interactor) {
            
            // todo: refactor sau, tạm thời mặc định Interactor là player
            PlayerController player = interactor.GetComponent<PlayerController>();
            
            

            // playerInteractor.ActionController.Gather(
            //     () => CompleteGather(playerInteractor)
            // );
        }
    }
}