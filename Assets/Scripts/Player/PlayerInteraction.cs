using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private PlayerType playerType;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Animator playerAnimator; // Assign Animator milik Player

    private void Awake()
    {
        if (mainCamera == null) mainCamera = Camera.main;
        if (playerAnimator == null) playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            TryInteract();
        }
    }

    private void TryInteract()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (hit.collider != null)
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                // Jalankan animasi di player jika yang diklik adalah rintangan yang sesuai
                if (playerType == PlayerType.Player1 && hit.collider.GetComponent<TreeObstacle>() != null)
                {
                    if (playerAnimator != null) playerAnimator.SetTrigger("Interact");
                }
                else if (playerType == PlayerType.Player2 && hit.collider.GetComponent<DoorObstacle>() != null)
                {
                    if (playerAnimator != null) playerAnimator.SetTrigger("Interact");
                }

                interactable.Interact(playerType);
            }
        }
    }
}