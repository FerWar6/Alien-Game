using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SettingsManager : MonoBehaviour
{
    // Singleton instance of SettingsManager
    public static SettingsManager instance { get; private set; }

    // Player sensitivity setting, adjustable in the inspector with a range slider
    [Range(0, 20)]
    public float playerSens = 0.98f;

    // Music volume setting, adjustable in the inspector with a range slider
    [Range(-30, 20)]
    public int musicVolume = 40;

    // Sound effect volume setting, adjustable in the inspector with a range slider
    [Range(-30, 20)]
    public int soundEffectVolume = 40;

    // List of player preference names for saving and loading settings
    public List<string> playerPrefNames = new List<string>();

    // Event triggered when settings are loaded
    public UnityEvent OnSettingsLoaded = new UnityEvent();

    // Flag to track if settings have been loaded
    private bool settingsLoaded = false;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Implementing the Singleton pattern to ensure only one instance of SettingsManager exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make sure the instance persists across scenes
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Check if settings have been loaded before
        settingsLoaded = PlayerPrefs.GetInt("settingsLoaded") != 0 ? true : false;
        if (!settingsLoaded)
        {
            LoadSettings(); // Load settings if they haven't been loaded before
            PlayerPrefs.SetInt("settingsLoaded", true ? 1 : 0); // Mark settings as loaded
        }
    }

    // Method to save current settings
    public void SaveSettings()
    {
        PlayerPrefs.SetInt("settingsLoaded", false ? 1 : 0); // Mark settings as not loaded

        // Save each setting to PlayerPrefs using corresponding names
        PlayerPrefs.SetFloat(playerPrefNames[0], playerSens);
        PlayerPrefs.SetInt(playerPrefNames[1], musicVolume);
        PlayerPrefs.SetInt(playerPrefNames[2], soundEffectVolume);
    }

    // Method to load settings from PlayerPrefs
    private void LoadSettings()
    {
        // Load each setting from PlayerPrefs using corresponding names
        playerSens = PlayerPrefs.GetFloat(playerPrefNames[0]);
        musicVolume = PlayerPrefs.GetInt(playerPrefNames[1]);
        soundEffectVolume = PlayerPrefs.GetInt(playerPrefNames[2]);

        // Apply loaded settings to AudioManager
        AudioManager.instance.SetMusicVolume(musicVolume);
        AudioManager.instance.SetSoundEffectVolume(soundEffectVolume);

        OnSettingsLoaded.Invoke(); // Trigger the settings loaded event
    }

    // Method called when the application is about to quit
    void OnApplicationQuit()
    {
        SaveSettings(); // Save settings before quitting
    }
}
