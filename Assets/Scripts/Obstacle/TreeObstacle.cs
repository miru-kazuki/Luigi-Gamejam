using System.Collections;
using UnityEngine;


public class TreeObstacle : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator treeAnimator; // opsional jika animasi ada di objek pohon
    [SerializeField] private float destroyDelay = 1.0f; // durasi animasi sebelum hilang
    private bool isInteracting = false;

    public void Interact(PlayerType player)
    {
        if (player == PlayerType.Player1 && !isInteracting)
        {
            StartCoroutine(HandleInteraction());
        }
    }

    private IEnumerator HandleInteraction()
    {
        isInteracting = true;

        // Trigger animasi di pohon jika ada
        if (treeAnimator != null)
        {
            treeAnimator.SetTrigger("MoveTree");
        }

        // Matikan collider agar player tidak bisa nge-klik/nabrak berulang kali saat animasi jalan
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;
        
        // Tunggu sampai animasi selesai
        yield return new WaitForSeconds(destroyDelay);

        Destroy(gameObject);
    }
}