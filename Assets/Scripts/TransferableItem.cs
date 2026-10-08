using UnityEngine;

public class TransferableItem : MonoBehaviour
{
    [Header("Player References")]
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;

    [Header("Transfer Settings")]
    [SerializeField] private float maxTransferDistance = 4f;
    [SerializeField] private Vector3 holdOffset = new Vector3(0f, 0.8f, 0f);

    [Header("UI / Visual Indicator")]
    [SerializeField] private GameObject rangeIndicator; // Assign GameObject sprite lingkaran di Inspector

    private Transform currentOwner;

    public Transform CurrentOwner => currentOwner;

    private void Start()
    {
        if (player1 != null)
        {
            AttachToPlayer(player1);
        }

        if (rangeIndicator != null)
        {
            rangeIndicator.SetActive(false);
        }
    }

    private void Update()
    {
        if (currentOwner != null)
        {
            transform.position = currentOwner.position + holdOffset;
        }

        CheckTransferRange();
    }

    private void CheckTransferRange()
    {
        if (player1 == null || player2 == null || rangeIndicator == null) return;

        float distance = Vector2.Distance(player1.position, player2.position);
        bool inRange = distance <= maxTransferDistance;

        if (rangeIndicator.activeSelf != inRange)
        {
            rangeIndicator.SetActive(inRange);
        }
    }

    public void TryTransferItem(Transform requester)
    {
        if (requester != currentOwner) return;

        float distanceBetweenPlayers = Vector2.Distance(player1.position, player2.position);

        if (distanceBetweenPlayers <= maxTransferDistance)
        {
            Transform targetPlayer = (currentOwner == player1) ? player2 : player1;
            AttachToPlayer(targetPlayer);
        }
    }

    private void AttachToPlayer(Transform targetPlayer)
    {
        currentOwner = targetPlayer;
        transform.position = currentOwner.position + holdOffset;
    }

    private void OnDrawGizmosSelected()
    {
        if (player1 != null && player2 != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(player1.position, player2.position);
        }
    }
}