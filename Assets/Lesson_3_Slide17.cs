using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Lesson_4_Slide17 : Audio_Narration
{
    public TextMeshProUGUI textMeshProUGUI;
    public Button zogleberBtn,nextSlideButton;
    public Animator hennika,transit;
    public Sprite[] imageSprite;
    public string[] questins;

    public Button[] choices;
    public int[] correctAnswer;

    private int audioCount,choiceValue, totalCanvas,wrongAudio=5,correctAudio=3,finalSceneAudio=7;
    void Start()
    {
        for(int i = 0; i < choices.Length; i++)
        {
            int index = i;
            choices[i].onClick.AddListener(() => SetValue(index));
        }
        StartCoroutine(StartScene());
    }

    public void SetValue(int index)
    {
        choiceValue = index;
        StartCoroutine(CheckAnswer());
    }

    private IEnumerator CheckAnswer()
    {
        if (correctAnswer[totalCanvas] == choiceValue)
        {
            yield return new WaitForSeconds(.2f);
            totalCanvas++;
            SetAudioNarration(correctAudio);
            nextSlideButton.gameObject.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(.2f);
            totalCanvas++;
            SetAudioNarration(wrongAudio); 
            nextSlideButton.gameObject.SetActive(true);
        }

        choices[0].gameObject.SetActive(false);
        choices[1].gameObject.SetActive(false);
        textMeshProUGUI.gameObject.SetActive(false);
    }

    private IEnumerator StartScene()
    {
        yield return new WaitForSeconds(1);
        hennika.Play("S17_Hennika_Landing");
        yield return new WaitForSeconds(.5f);
        SetAudioNarration(audioCount); audioCount++;
        for (int i=0; i < 3; i++)
        {
            hennika.Play("S17_Hennika_Perc_Speaking"); yield return new WaitForSeconds(2);
            hennika.SetBool("isIdle", true); yield return new WaitForSeconds(.5f);
        }

        textMeshProUGUI.gameObject.SetActive(true);
        for (int i = 3; i < 8; i++)
        {
            hennika.Play("S17_Hennika_Perc_Speaking"); yield return new WaitForSeconds(2);
            hennika.SetBool("isIdle", true); yield return new WaitForSeconds(.5f);
        }
        zogleberBtn.onClick.AddListener(GoToZogle);
        zogleberBtn.gameObject.SetActive(true);

    }


    public void GoToZogle()
    {
        zogleberBtn.onClick.RemoveAllListeners();
        zogleberBtn.gameObject.SetActive(false);
        textMeshProUGUI.gameObject.SetActive(false);
        nextSlideButton.onClick.AddListener(NextPage1);
        Application.OpenURL("www.zogleber.com");
        NextPage(); 
    }
    public void NextPage()
    {
        StartCoroutine(NextPageCoroutine());
    }

    private IEnumerator NextPageCoroutine()
    {
        invisibleWall.SetActive(true);
        SetAudioNarration(audioCount); yield return new WaitForSeconds(clip[audioCount].length); audioCount++;
        textMeshProUGUI.gameObject.SetActive(true);
        textMeshProUGUI.text = questins[0];
        choices[0].gameObject.SetActive(true);
        choices[1].gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        invisibleWall.SetActive(false);
    }
    public void NextPage1()
    {
        nextSlideButton.gameObject.SetActive(false);
        StartCoroutine(NextPageCoroutine1());
    }

    private IEnumerator NextPageCoroutine1()
    {
        invisibleWall.SetActive(true);
        wrongAudio++; correctAudio++;
        SetAudioNarration(audioCount); yield return new WaitForSeconds(clip[audioCount].length); audioCount++;
        textMeshProUGUI.gameObject.SetActive(true);
        choices[0].image.sprite = imageSprite[0];
        choices[1].image.sprite = imageSprite[1];
        textMeshProUGUI.text = questins[1];
        choices[0].gameObject.SetActive(true);
        choices[1].gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        nextSlideButton.onClick.RemoveAllListeners();
        nextSlideButton.onClick.AddListener(FinalScene);
        invisibleWall.SetActive(false);
    }
    public void FinalScene()
    {
        StartCoroutine(FinalSceneCoroutine());
    }
    private IEnumerator FinalSceneCoroutine()
    {
        //transit.Play("Transition_Plain");
        //yield return new WaitForSeconds(1.4f);
        invisibleWall.SetActive(true);
        textMeshProUGUI.gameObject.SetActive(false);
        choices[0].gameObject.SetActive(false);
        choices[1].gameObject.SetActive(false);
        nextSlideButton.gameObject.SetActive(false);
        yield return new WaitForSeconds(.5f);
        //transit.Play("Transition_Plain_Reverse");
        //yield return new WaitForSeconds(1.4f);
        SetAudioNarration(finalSceneAudio); finalSceneAudio++;
        for (int i = 0; i < 3; i++)
        {
            hennika.Play("S17_Hennika_Perc_Speaking"); yield return new WaitForSeconds(2);
            hennika.SetBool("isIdle", true); yield return new WaitForSeconds(.5f);
        }

        textMeshProUGUI.text = questins[2];
        textMeshProUGUI.gameObject.SetActive(true);
        for (int i = 3; i < 6; i++)
        {
            hennika.Play("S17_Hennika_Perc_Speaking"); yield return new WaitForSeconds(2);
            hennika.SetBool("isIdle", true); yield return new WaitForSeconds(.5f);
        }

        invisibleWall.SetActive(false);
        zogleberBtn.onClick.AddListener(GoToNextSlide);
        zogleberBtn.gameObject.SetActive(true);

    }

    public void GoToNextSlide()
    {
        Application.OpenURL("www.zogleber.com");
        LoadScene();
    }
}
