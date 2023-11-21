using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Slider musicSlider;
    public Slider audioSlider;
    public AudioSource musicAudioSource;
    public AudioSource audioSource;
    public Sprite xSprite, pauseSprite;

    GameObject panel, pause;
    bool isPaused = false;

    void Start()
    {
        panel = transform.GetChild(0).Find("Menu Items").gameObject;
        pause = transform.GetChild(0).Find("Pause").gameObject;

        // Set initial slider values to the current audio levels
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        audioSlider.value = PlayerPrefs.GetFloat("AudioVolume");

        // Read and set previously saved value
        musicAudioSource.volume = musicSlider.value;
        audioSource.volume = audioSlider.value;

        // Add listeners to the sliders
        musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
        audioSlider.onValueChanged.AddListener(ChangeAudioVolume);
        // StartCoroutine(StartScene());
    }

    //IEnumerator StartScene()
    //{

    //}

    void Update()
    {
        // Toggle pause on pressing the P key
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        // Pause or resume all audio sources in the scene

        if (isPaused)
        {
            panel.SetActive(true);
            audioSource.Pause();
        }
        else
        {
            panel.SetActive(false);
            audioSource.UnPause();
        }


        // Optionally, you can add more functionality for pausing and resuming.  isPaused = !isPaused;

        // Pause or resume all audio sources in the scene

        if (isPaused)
        {
            pause.GetComponent<Image>().sprite = xSprite;
            audioSource.Pause();
            Time.timeScale = 0.0f;
        }
        else
        {
            pause.GetComponent<Image>().sprite = pauseSprite;
            audioSource.UnPause();
            Time.timeScale = 1.0f;
        }


        // Optionally, you can add more functionality for pausing and resuming.
    }

    void ChangeMusicVolume(float volume)
    {
        musicAudioSource.volume = volume;

        // Save the music volume preference
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    void ChangeAudioVolume(float volume)
    {
        // Adjust the volume of all audio sources

        audioSource.volume = volume;


        // Save the audio volume preference
        PlayerPrefs.SetFloat("AudioVolume", volume);
    }
}