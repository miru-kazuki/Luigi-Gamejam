using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [Header("Scene Settings")]
    [Tooltip("Ketik nama scene tujuan sesuai di Build Settings")]
    [SerializeField] private string targetSceneName;

    public void ChangeScene()
    {
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.LogWarning("Nama target scene belum diisi di Inspector!");
        }
    }
}