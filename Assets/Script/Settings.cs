using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public AudioSource music;
    public AudioSource voice;
    public AudioSource[] hovers;

    public Slider musicSlider;
    public Slider voiceSlider;
    void Awake()
    {
        if (PlayerPrefs.HasKey("Music"))
        {
            
         musicSlider.value = PlayerPrefs.GetFloat("Music");
            Debug.Log("Loaded");
        }

        else { musicSlider.value = 0.5f; }

        if (PlayerPrefs.HasKey("Voice"))
        {
            voiceSlider.value = PlayerPrefs.GetFloat("Voice");
        }

        else { voiceSlider.value = 0.5f; }

        
        PlayerPrefs.SetInt("Continue", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetInt("Continue2", SceneManager.GetActiveScene().buildIndex);
        Debug.Log(musicSlider.value);
        Debug.Log(voiceSlider.value);
    }

    private void Update()
    {
        music.volume = musicSlider.value;
        voice.volume = voiceSlider.value;
    }

    public void ChangeValue()
    {
        music.volume = musicSlider.value;
        voice.volume = voiceSlider.value;
        for(int i = 0; i < hovers.Length; i++)
        {
            hovers[i].volume = voiceSlider.value;
        }
        PlayerPrefs.SetFloat("Music", musicSlider.value);
        PlayerPrefs.SetFloat("Voice", voiceSlider.value);
        Debug.Log(musicSlider.value);
        Debug.Log(voiceSlider.value);

    }
}
