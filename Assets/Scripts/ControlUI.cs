
using UnityEngine;

public class ControlsTutorial : MonoBehaviour
{
    [Header("Tutorial UI")]
    [SerializeField] private GameObject controlsPanel;

    private void Start()
    {
        controlsPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        controlsPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
