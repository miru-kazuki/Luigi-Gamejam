using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string nextSceneName;

    [Header("Finish UI")]
    [SerializeField] private GameObject finishPanel;

    [Header("Player References")]
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    private bool player1Finished;
    private bool player2Finished;
    private bool hasFinished;

    private void Start()
    {
        finishPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasFinished || !other.CompareTag("Player"))
            return;

        // Cek apakah yang menyentuh Finish Line adalah Player 1
        if (other.gameObject == player1)
        {
            player1Finished = true;
        }
        // Cek apakah yang menyentuh Finish Line adalah Player 2
        else if (other.gameObject == player2)
        {
            player2Finished = true;
        }

        // Selesaikan level jika kedua player sudah sampai
        if (player1Finished && player2Finished)
        {
            hasFinished = true;
            finishPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextSceneName);
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}