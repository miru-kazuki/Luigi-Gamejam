using UnityEngine;
using UnityEngine.SceneManagement;

public class SalamanderAI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private PlayerController[] players;
    [SerializeField] private TransferableItem jimatItem; // Reference ke GameObject Jimat

    private void Start()
    {
        if (players == null || players.Length == 0)
        {
            GameObject[] playerObjs = GameObject.FindGameObjectsWithTag("Player");
            players = new PlayerController[playerObjs.Length];
            for (int i = 0; i < playerObjs.Length; i++)
            {
                players[i] = playerObjs[i].GetComponent<PlayerController>();
            }
        }
    }

    private void Update()
    {
        if (players == null || players.Length == 0) return;

        foreach (var player in players)
        {
            if (player == null) continue;

            // Cek apakah player ini adalah pemilik jimat saat ini
            bool holdsJimat = (jimatItem != null && jimatItem.CurrentOwner == player.transform);

            if (holdsJimat)
            {
                bool playerIsOnRight = player.transform.position.x > transform.position.x;
                bool isLookingAtSalamander = (playerIsOnRight && !player.isFacingRight) ||
                                             (!playerIsOnRight && player.isFacingRight);

                if (isLookingAtSalamander)
                {
                    // Player membawa jimat dan menatap salamander -> Berhenti
                    return;
                }
            }
        }

        // Jika tidak ada yang menghentikan, kejar player terdekat
        PlayerController targetPlayer = GetClosestPlayer();
        if (targetPlayer != null)
        {
            MoveTowardsTarget(targetPlayer.transform);
        }
    }

    private PlayerController GetClosestPlayer()
    {
        PlayerController closest = null;
        float minDistance = Mathf.Infinity;

        foreach (var player in players)
        {
            if (player == null) continue;
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = player;
            }
        }

        return closest;
    }

    private void MoveTowardsTarget(Transform target)
    {
        Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            RestartGame();
        }
    }

    private void RestartGame()
    {
        Debug.Log("gameover");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}