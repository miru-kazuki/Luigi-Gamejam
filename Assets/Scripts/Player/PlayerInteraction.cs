using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private PlayerType playerType;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Animator playerAnimator; // Assign Animator milik Player
    [SerializeField] private float interactionRange = 3f; // Jarak maksimal interaksi
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
        Debug.Log("PlayerInteraction: TryInteract called for " + playerType.ToString());
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (hit.collider != null)
        {
            // if distance between the player and the interactable object is too far, we can add a check here (optional)
            if (Vector2.Distance(transform.position, hit.collider.transform.position) > interactionRange)
            {
                Debug.Log("PlayerInteraction: Interactable is too far for " + playerType.ToString());
                return;
            }
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                Debug.Log("PlayerInteraction: Interactable found for " + playerType.ToString());
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