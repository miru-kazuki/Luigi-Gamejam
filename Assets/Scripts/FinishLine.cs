using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private string nextSceneName;

    private bool player1Finished = false;
    private bool player2Finished = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1"))
        {
            player1Finished = true;
        }

        if (other.CompareTag("Player2"))
        {
            player2Finished = true;
        }

        if (player1Finished && player2Finished)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}