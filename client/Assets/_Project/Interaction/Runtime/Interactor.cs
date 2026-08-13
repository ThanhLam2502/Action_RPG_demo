// using UnityEngine;
//
// namespace TopdownRPG.Interaction
// {
//     public class Interactor : MonoBehaviour
//     {
//         #region Class Variable
//         [SerializeField] private int _numFound;
//         [SerializeField] private float _scanInterval = 0.1f;
//         [SerializeField] private float _interactionRadius = 0.2f;
//         [SerializeField] private Transform _interactionPoint;
//
//         private readonly Collider[] _colliders = new Collider[4];
//         private IInteractable _currentInteractable;
//         private float _scanTimer;
//         #endregion
//
//         #region Startup
//         #endregion
//
//         #region Update Logic
//         private void Update()
//         {
//             _scanTimer -= Time.deltaTime;
//             if (_scanTimer <= 0f)
//             {
//                 _scanTimer = _scanInterval;
//                 FindCurrentInteractable();
//             }
//         }
//
//         private void FindCurrentInteractable()
//         {
//             _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionRadius, _colliders);
//
//             _currentInteractable = null;
//             float closestDistanceSqr = float.MaxValue;
//             for (int i = 0; i < _numFound; i++)
//             {
//                 IInteractable interactable = _colliders[i].GetComponentInParent<IInteractable>();
//
//                 if (interactable == null)
//                     continue;
//
//                 Vector3 closestPoint = _colliders[i].ClosestPoint(transform.position);
//                 float distanceSqr = (closestPoint - _interactionPoint.position).sqrMagnitude;
//
//                 if (distanceSqr < closestDistanceSqr)
//                 {
//                     closestDistanceSqr = distanceSqr;
//                     _currentInteractable = interactable;
//                 }
//             }
//         }
//
//         private void TryInteract()
//         {
//             if (_currentInteractable == null)
//                 return;
//
//             _currentInteractable.Interact(this);
//         }
//         #endregion
//
//         #region Debug
//         private void OnDrawGizmosSelected()
//         {
//             if (!_interactionPoint)
//                 return;
//
//             Gizmos.color = Color.yellow;
//             Gizmos.DrawWireSphere(_interactionPoint.position, _interactionRadius);
//         }
//         #endregion
//     }
// }