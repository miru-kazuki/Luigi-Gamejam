using System.Collections;
using UnityEngine;

public class DoorObstacle : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private float destroyDelay = 1.0f;
    private bool isInteracting = false;

    public void Interact(PlayerType player)
    {
        if (player == PlayerType.Player2 && !isInteracting)
        {
            StartCoroutine(HandleInteraction());
        }
    }

    private IEnumerator HandleInteraction()
    {
        isInteracting = true;

        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("OpenDoor");
        }

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        yield return new WaitForSeconds(destroyDelay);

        Destroy(gameObject);
    }
}