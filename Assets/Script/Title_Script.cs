using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title_Script : Audio_Narration
{
    public Button continueButton;


    private void Start()
    {
        transition = GameObject.FindGameObjectWithTag("Transition").GetComponent<Animator>();
        if (PlayerPrefs.HasKey("Continue"))
        {
            continueButton.interactable = true;
        }
        else { continueButton.interactable = false; }
    }

    public void MainMenuButtonClick()
    {
        StartCoroutine(NextSceneCoroutine(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void Continue()
    {
        StartCoroutine(NextSceneCoroutine(PlayerPrefs.GetInt("Continue")));
    }
}
