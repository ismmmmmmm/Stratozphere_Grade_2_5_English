using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Lesson_4_Slide34 : Audio_Narration
{
    // Start is called before the first frame update
    [Header("Single")]
    public GameObject textHolder;
    public TextMeshProUGUI questions;
    public Animator animator,cam,canvasFade;
    public Button NextSlideButton;
    private int animCount, mosquitoCount, currentAnswer, audioCount, levelCount, wrongAudio=6,infoCount=11;


    [Header("Multiple")]
    public Button[] mosquitoButton;
    public Animator[] mosquitoAnim;
    public Image[] mosquitoImage;
    public Sprite[] mosquitoSprite;
    public GameObject[] canvas;
    [SerializeField]
    string[] questionText;
    
    [SerializeField]
    int[] correctAnswers;
    void Start()
    {
        for (int i = 0; i < mosquitoButton.Length; i++)
        {
            int index = i;
            mosquitoButton[i].onClick.AddListener(()=> SetMosquitoValue(index));
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
            SetAudioNarration(36);
            for (int i = 0; i < mosquitoButton.Length; i++)
            {
                yield return new WaitForSeconds(.4f);
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
        yield return new WaitForSeconds (8);
        cam.Play("Slide34_Zoom");
        yield return new WaitForSeconds(1.2f);
        canvasFade.Play("Slide34_Fade");
        yield return new WaitForSeconds(1.2f);
        cam.Play("Slide34_Reverse");
        yield return new WaitForSeconds(1.2f);
        yield return new WaitForSeconds(clip[audioCount].length-8.4F);audioCount++;


        // mosquito Show\
        SetAudioNarration (audioCount);
        questions.text= questionText[levelCount];   
        textHolder.SetActive(true);
        for (int i = 0; i < mosquitoImage.Length; i++)
        {
            mosquitoImage[i].sprite = mosquitoSprite[mosquitoCount];mosquitoCount++;

        }
        for (int i = 0;i < mosquitoButton.Length;i++) {
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
            NextSlideButton.onClick.AddListener(NewPage);
        }
    }

    public void NewPage()
    {
        canvas[0].SetActive(false);
        canvas[1].SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
