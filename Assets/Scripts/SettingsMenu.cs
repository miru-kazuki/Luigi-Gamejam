using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle muteToggle;

    [Header("Settings Behavior")]
    [SerializeField] private bool pauseGameplay = false;

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("GameVolume", 1f);
        bool isMuted = PlayerPrefs.GetInt("GameMuted", 0) == 1;

        volumeSlider.SetValueWithoutNotify(savedVolume);
        muteToggle.SetIsOnWithoutNotify(isMuted);

        AudioListener.volume = isMuted ? 0f : savedVolume;

        volumeSlider.onValueChanged.AddListener(SetVolume);
        muteToggle.onValueChanged.AddListener(SetMute);

        settingsPanel.SetActive(false);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);

        if (pauseGameplay)
        {
            Time.timeScale = 0f;
        }
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);

        if (pauseGameplay)
        {
            Time.timeScale = 1f;
        }
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = muteToggle.isOn ? 0f : volume;

        PlayerPrefs.SetFloat("GameVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetMute(bool isMuted)
    {
        AudioListener.volume = isMuted ? 0f : volumeSlider.value;

        PlayerPrefs.SetInt("GameMuted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }
}