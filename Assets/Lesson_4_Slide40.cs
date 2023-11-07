using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Lesson_4_Slide40 : Audio_Narration
{
    // Start is called before the first frame update
    [Header("Single")]
    public GameObject textHolder;
    public TextMeshProUGUI questions;
    public Animator animator, cam, canvasFade;
    public Button NextSlideButton;
    private int animCount, mosquitoCount, currentAnswer, audioCount, levelCount, wrongAudio = 6, infoCount = 11;


    [Header("Multiple")]
    public Button[] mosquitoButton;
    public Animator[] mosquitoAnim;
    public Image[] mosquitoImage;
    public Sprite[] mosquitoSprite;
    [SerializeField]
    string[] questionText;

    [SerializeField]
    int[] correctAnswers;
    void Start()
    {
        for (int i = 0; i < mosquitoButton.Length; i++)
        {
            int index = i;
            mosquitoButton[i].onClick.AddListener(() => SetMosquitoValue(index));
        }

        NextSlideButton.onClick.AddListener(NextPage);
        StartCoroutine(StartScene());
    }

    public void SetMosquitoValue(int index)
    {
        currentAnswer = index;
        CheckAnswer();
    }

    public void CheckAnswer()
    {
        StartCoroutine(CheckAnswerCoroutine());
    }

    private IEnumerator CheckAnswerCoroutine()
    {
        if (currentAnswer == correctAnswers[levelCount])
        {
            SetAudioNarration(11);
            for (int i = 0; i < mosquitoButton.Length; i++)
            {
                mosquitoButton[i].gameObject.SetActive(false);  
            }
            textHolder.gameObject.SetActive(false);
            levelCount++;
            wrongAudio++;
            infoCount++;
            CheckSlide();
            NextSlideButton.gameObject.SetActive(true);
        }
        else
        {
            SetAudioNarration(wrongAudio);
            yield return new WaitForSeconds(clip[wrongAudio].length);
        }
    }
    public void NextPage()
    {
        StartCoroutine(NextPageCoroutine());
    }

    private IEnumerator NextPageCoroutine()
    {
        NextSlideButton.gameObject.SetActive(false);
        questions.text = questionText[levelCount];
        textHolder.SetActive(true);
        for (int i = 0; i < mosquitoImage.Length; i++)
        {
            mosquitoImage[i].sprite = mosquitoSprite[mosquitoCount]; mosquitoCount++;

        }
        for (int i = 0; i < mosquitoButton.Length; i++)
        {
            yield return new WaitForSeconds(.5f);
            mosquitoButton[i].gameObject.SetActive(true);

        }

        Debug.Log(mosquitoCount);
    }

    private IEnumerator StartScene()
    {
        yield return new WaitForSeconds(3);
        SetAudioNarration(audioCount);
        yield return new WaitForSeconds(clip[audioCount].length);
        canvasFade.Play("Lesson_4_Slide40");
        yield return new WaitForSeconds(1.2f);
        cam.Play("Slide34_Zoom");
        yield return new WaitForSeconds(.6f);
        canvasFade.Play("Lesson_4_Slide_40_Fade");
        cam.Play("Slide34_Reverse");
        yield return new WaitForSeconds(1.2f);audioCount++;


        // mosquito Show\
        SetAudioNarration(audioCount);
        questions.text = questionText[levelCount];
        textHolder.SetActive(true);
        for (int i = 0; i < mosquitoImage.Length; i++)
        {
            mosquitoImage[i].sprite = mosquitoSprite[mosquitoCount]; mosquitoCount++;

        }
        for (int i = 0; i < mosquitoButton.Length; i++)
        {
            yield return new WaitForSeconds(.5f);
            mosquitoButton[i].gameObject.SetActive(true);

        }
        audioCount++;


        Debug.Log(mosquitoCount);
    }

    public void CheckSlide()
    {
        if (levelCount == 4)
        {
            NextSlideButton.onClick.RemoveAllListeners();
            NextSlideButton.onClick.AddListener(LoadScene);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
