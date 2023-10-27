using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Lesson_5_Slide35 : Audio_Narration
{
    [Header("Multiple")]
    public Button[] choices;
    public Sprite[] btnSprite;
    public GameObject[] scene;
    public string[] questionText, sentenceText;
    public int[] correctAnswer;

    [Header("Single")]
    public TextMeshProUGUI questionTMP, sentenceTMP;
    int currentValue, level, btnCount;
    public int correctAudioIndex, wrongAudioIndex;
    public Button nextSlideButton;
    bool isVisible= false;
    public Button helpBtn;
    public Animator dolly, hennika, mainCam;
    public AnimationClip camZoomIn, camZoomOut;
    public GameObject btnLayoutGroup;

    void Start()
    {
        questionTMP.gameObject.SetActive(false);
        sentenceTMP.gameObject.SetActive(false);
        btnLayoutGroup.SetActive(false);
        helpBtn.gameObject.SetActive(false);

        for (int i = 0; i < choices.Length; i++)
        {
            int index = i;
            choices[i].onClick.AddListener(() => SetValue(index));
        }

        nextSlideButton.onClick.AddListener(NextScene);
        helpBtn.onClick.AddListener(HelpButton);
        StartCoroutine(StartScene());
    }

    void SetSentenceText()
    {
        int index = 0;
        sentenceTMP.text = sentenceText[index];
        index++;
    }

    void SetChoicesButtonsSprite()
    {
        for (int i = 0; i < choices.Length; i++)
        {
            choices[i].GetComponent<Image>().sprite = btnSprite[btnCount]; btnCount++;
        }
    }

    void HelpButton()
    {
        isVisible = !isVisible;
        Debug.Log("Help button clicked!");
        helpBtn.transform.Find("HelpPanel").gameObject.SetActive(isVisible);
    }

    public void SetValue(int index)
    {
        Debug.Log(currentValue);
        currentValue = index;
        StartCoroutine(CheckAnswer());

        if (isVisible)
        {
            HelpButton();
            Debug.Log("OPEN");
        }
    }

    IEnumerator CheckAnswer()
    {
        if (currentValue == correctAnswer[level]) //is correct
        {
            choices[correctAnswer[level]].image.color = Color.green;
            yield return new WaitForSeconds(.2f);
            
            hennika.Play("Hennika_Idle");
            Debug.Log("Correct");
            level++;
            SetAudioNarration(correctAudioIndex);
            nextSlideButton.gameObject.SetActive(true);
        }

        else //is wrong
        {
            
            yield return new WaitForSeconds(.2f);
            hennika.Play("Hennika_Hmm");
            SetAudioNarration(wrongAudioIndex);
            Debug.Log("Wrong");
        }
    }

    IEnumerator StartScene()
    {
        yield return new WaitForSeconds(2); //screen load

        mainCam.Play(camZoomIn.name); //camera zoom
        yield return new WaitForSeconds(camZoomIn.length);

        SetAudioNarration(0); //hennika talks about the challenge
        hennika.Play("Hennika_Intro_Race");
        yield return new WaitForSeconds(clip[0].length);

        Debug.Log("Start Scene");
        mainCam.Play(camZoomOut.name);
        yield return new WaitForSeconds(camZoomOut.length);

        SetChoicesButtonsSprite();
        SetSentenceText();
        questionTMP.text = questionText[level];
        questionTMP.gameObject.SetActive(true);
        sentenceTMP.gameObject.SetActive(true);
      
        yield return new WaitForSeconds(1);
        //hennika.Play
    }

    void NextScene()
    {
        SetChoicesButtonsSprite();
        nextSlideButton.gameObject.SetActive(false);
        scene[level - 1].SetActive(false);
        scene[level].SetActive(true);
        questionTMP.text = questionText[level];
        sentenceTMP.text = sentenceText[level];
    }

}
