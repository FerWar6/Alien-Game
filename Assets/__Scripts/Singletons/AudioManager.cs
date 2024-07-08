using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton instance of AudioManager
    public static AudioManager instance { get; private set; }

    // Pools for managing active and inactive audio sources
    public Transform inActiveAudioPool;
    public Transform activeAudioPool;

    // Number of audio sources to preallocate
    public int numberOfAudioSources;

    // Prefab for creating audio sources
    public GameObject audioSource;

    // List to keep track of all audio sources
    public List<GameObject> audioSourceList = new List<GameObject>();

    // Reference to the audio mixer for volume control
    public AudioMixer mixer;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Implementing the Singleton pattern to ensure only one instance of AudioManager exists
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
        // If the audio pools are set, initialize the audio sources
        if (inActiveAudioPool != null && activeAudioPool != null)
        {
            for (int i = 0; i < numberOfAudioSources; i++)
            {
                Vector3 audioSourcePos = new Vector3(0, 0, 0);
                // Instantiate a new audio source and add it to the inactive pool
                GameObject newAudioSource = Instantiate(audioSource, audioSourcePos, Quaternion.identity, inActiveAudioPool);
                AddToList(newAudioSource);
            }
        }
    }

    // Method to play an audio clip at a specified position
    public void SetAudioClip(AudioClip clip, Vector3 position, float volume = 1)
    {
        // Find an available audio source
        GameObject openAudioSource = FindEmptyAudioClip();
        if (openAudioSource != null)
        {
            // Move the audio source to the active pool and set its position
            openAudioSource.transform.parent = activeAudioPool;
            openAudioSource.transform.position = position;

            // Get the AudioSource component and configure it
            AudioSource source = openAudioSource.GetComponent<AudioSource>();
            source.volume = volume;
            source.clip = clip;
            source.Play();

            // Start a coroutine to return the audio source to the inactive pool when done
            StartCoroutine(WaitForSoundToEnd(source, openAudioSource, clip.length));
        }
    }
    public void SetAudioClip(AudioClip clip, Vector3 position, float volume = 1, bool is2D = false)
    {
        // Find an available audio source
        GameObject openAudioSource = FindEmptyAudioClip();
        if (openAudioSource != null)
        {
            // Move the audio source to the active pool and set its position
            openAudioSource.transform.parent = activeAudioPool;
            openAudioSource.transform.position = position;

            // Get the AudioSource component and configure it
            AudioSource source = openAudioSource.GetComponent<AudioSource>();
            if (is2D) source.spatialBlend = 0; // Set to 2D if specified
            source.volume = volume;
            source.clip = clip;
            source.Play();

            // Start a coroutine to return the audio source to the inactive pool when done
            StartCoroutine(WaitForSoundToEnd(source, openAudioSource, clip.length));
        }
    }

    // Find an available (empty) audio source from the list
    public GameObject FindEmptyAudioClip()
    {
        GameObject openClip = null;
        for (int i = 0; i < audioSourceList.Count; i++)
        {
            if (audioSourceList[i].GetComponent<AudioSource>().clip == null)
            {
                openClip = audioSourceList[i];
                return openClip;
            }
        }
        return null;
    }

    // Add a new audio source to the list
    public void AddToList(GameObject source)
    {
        audioSourceList.Add(source);
    }

    // Coroutine to wait for the audio clip to finish playing
    private IEnumerator WaitForSoundToEnd(AudioSource audioSource, GameObject sourceObject, float clipLength)
    {
        yield return new WaitForSeconds(clipLength);

        // Return the audio source to the inactive pool and clear its clip
        sourceObject.transform.parent = inActiveAudioPool;
        audioSource.clip = null;
    }

    // Set the volume for music
    public void SetMusicVolume(int input)
    {
        mixer.SetFloat("Music Volume", input);
    }

    // Set the volume for sound effects
    public void SetSoundEffectVolume(int input)
    {
        mixer.SetFloat("Sound Effect Volume", input);
    }
}
