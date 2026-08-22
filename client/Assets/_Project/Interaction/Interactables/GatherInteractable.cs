using TopdownRPG.Character;
using UnityEngine;


namespace TopdownRPG.Interaction {
    public class GatherInteractable : Interactable {
        // @formatter:off
        [Header("Resource")]
        [SerializeField] private string interactionAction = "Gather";
        // @formatter:on

        public override string InteractionAction => interactionAction;

        public override void Interact(Interactor interactor) {
            
            // todo: refactor sau, tạm thời mặc định Interactor là player
            interactor.TryGetComponent(out PlayerController player);


            // playerInteractor.ActionController.Gather(
            //     () => CompleteGather(playerInteractor)
            // );
        }
    }
}