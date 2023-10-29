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
<<<<<<< Updated upstream

    [Header("Single")]
    public TextMeshProUGUI questionTMP, sentenceTMP;
    int currentValue, level, btnCount;
    public int correctAudioIndex, wrongAudioIndex;
    public Button nextSlideButton;
    bool isVisible= false;
=======
    public AnimationClip[] animations;

    [Header("Single")]
    public TextMeshProUGUI questionTMP, sentenceTMP;
    int currentValue, level, btnCount, AnimIndex = 0, AudioIndex = 0;
    public int correctAudioIndex, wrongAudioIndex, helpAudioIndex;
    public Button nextSlideButton;
    bool isVisible = false;
>>>>>>> Stashed changes
    public Button helpBtn;
    public Animator dolly, hennika, mainCam;
    public AnimationClip camZoomIn, camZoomOut;
    public GameObject btnLayoutGroup;

<<<<<<< Updated upstream
    void Start()
    {
        questionTMP.gameObject.SetActive(false);
        sentenceTMP.gameObject.SetActive(false);
        btnLayoutGroup.SetActive(false);
=======

    void Start()
    {
        ResetScene();
        nextSlideButton.onClick.AddListener(NextScene);
        helpBtn.onClick.AddListener(HelpButton);
        StartCoroutine(StartScene());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 3;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Time.timeScale = 1;
        }
    }

    void ResetScene()
    {
        questionTMP.gameObject.SetActive(false);
        sentenceTMP.gameObject.SetActive(false);
        //btnLayoutGroup.SetActive(false);
>>>>>>> Stashed changes
        helpBtn.gameObject.SetActive(false);

        for (int i = 0; i < choices.Length; i++)
        {
<<<<<<< Updated upstream
            int index = i;
            choices[i].onClick.AddListener(() => SetValue(index));
        }

        nextSlideButton.onClick.AddListener(NextScene);
        helpBtn.onClick.AddListener(HelpButton);
        StartCoroutine(StartScene());
=======
            choices[i].gameObject.SetActive(false);
            choices[i].interactable = false;
            int index = i;
            choices[i].onClick.AddListener(() => SetValue(index));
        }
>>>>>>> Stashed changes
    }

    void SetSentenceText()
    {
        int index = 0;
        sentenceTMP.text = sentenceText[index];
        index++;
    }

<<<<<<< Updated upstream
    void SetChoicesButtonsSprite()
=======
    IEnumerator SetButtonsActive()
    {
        //set sprite   //change color to white
        for (int i = 0; i < choices.Length; i++)
        {
            choices[i].image.color = Color.white;
            choices[i].GetComponent<Image>().sprite = btnSprite[btnCount]; btnCount++;
        }
        yield return new WaitForSeconds(.1f);
        //activate all choices 
        foreach (Button button in choices)
        {
            button.gameObject.SetActive(true);
            yield return new WaitForSeconds(2.5f);

        }

        //start choices uninteractable
        foreach (Button button in choices)
        {
            button.interactable = true;
        }

        yield return new WaitForSeconds(clip[AnimIndex].length);
        helpBtn.gameObject.SetActive(true);
    }

    /*void SetChoicesButtonsSprite()
>>>>>>> Stashed changes
    {
        for (int i = 0; i < choices.Length; i++)
        {
            choices[i].GetComponent<Image>().sprite = btnSprite[btnCount]; btnCount++;
        }
<<<<<<< Updated upstream
    }
=======
    }*/
>>>>>>> Stashed changes

    void HelpButton()
    {
        isVisible = !isVisible;
        Debug.Log("Help button clicked!");
        helpBtn.transform.Find("HelpPanel").gameObject.SetActive(isVisible);
<<<<<<< Updated upstream
=======
        if (isVisible)
        {
            SetAudioNarration(helpAudioIndex);
            hennika.Play("Hennika_Remember");
        }
        
        
>>>>>>> Stashed changes
    }

    public void SetValue(int index)
    {
<<<<<<< Updated upstream
        Debug.Log(currentValue);
=======
        //Debug.Log(currentValue);
>>>>>>> Stashed changes
        currentValue = index;
        StartCoroutine(CheckAnswer());

        if (isVisible)
        {
            HelpButton();
<<<<<<< Updated upstream
            Debug.Log("OPEN");
=======
>>>>>>> Stashed changes
        }
    }

    IEnumerator CheckAnswer()
    {
        if (currentValue == correctAnswer[level]) //is correct
        {
            choices[correctAnswer[level]].image.color = Color.green;
<<<<<<< Updated upstream
            yield return new WaitForSeconds(.2f);
            
            hennika.Play("Hennika_Idle");
            Debug.Log("Correct");
            level++;
            SetAudioNarration(correctAudioIndex);
            nextSlideButton.gameObject.SetActive(true);
=======

            hennika.Play("Hennika_Idle");
            Debug.Log("Correct");
            SetAudioNarration(correctAudioIndex);
            yield return new WaitForSeconds(.2f);
            
            nextSlideButton.gameObject.SetActive(true);
            //level++;
>>>>>>> Stashed changes
        }

        else //is wrong
        {
<<<<<<< Updated upstream
            
            yield return new WaitForSeconds(.2f);
=======
            //yield return new WaitForSeconds(.2f);
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
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
=======
        //hennika talks about the challenge
        SetAudioNarration(AudioIndex);
        hennika.Play(animations[AnimIndex].name);
        yield return new WaitForSeconds(clip[AnimIndex].length);
        AnimIndex++; AudioIndex++;

        Debug.Log("Start Scene"); //camera zoom out
        mainCam.Play(camZoomOut.name);
        yield return new WaitForSeconds(camZoomOut.length);

        /*SetAudioNarration(AudioIndex); //hennika reads sentence/choices as it appears
        hennika.Play(animations[AnimIndex].name);
        questionTMP.text = questionText[level];
        questionTMP.gameObject.SetActive(true);
        //timer + anim
        sentenceTMP.gameObject.SetActive(true);
        AnimIndex++; AudioIndex++;*/

        yield return new WaitForSeconds(.5f);
        StartCoroutine(StartNextScene()); //Reads sentence
        SetSentenceText();

        //SetChoicesButtonsSprite(); //initialize buttons

>>>>>>> Stashed changes
    }

    void NextScene()
    {
<<<<<<< Updated upstream
        SetChoicesButtonsSprite();
        nextSlideButton.gameObject.SetActive(false);
        scene[level - 1].SetActive(false);
        scene[level].SetActive(true);
        questionTMP.text = questionText[level];
        sentenceTMP.text = sentenceText[level];
=======
        level++;
        //SetChoicesButtonsSprite();
        //StartCoroutine(SetButtonsActive());
        nextSlideButton.gameObject.SetActive(false);
        /*scene[level - 1].SetActive(false);
        scene[level].SetActive(true);*/
        questionTMP.text = questionText[level];
        sentenceTMP.text = sentenceText[level];

        ResetScene();
        //reset buttons

        //wait 2 sec
        StartCoroutine(StartNextScene());
    }

    IEnumerator StartNextScene() //Reusable Audio and Animation
    {
        SetAudioNarration(AudioIndex); //hennika reads sentence as it appears
        hennika.Play(animations[AnimIndex].name);
        questionTMP.text = questionText[level];
        Debug.Log("Current level: " + level);
        questionTMP.gameObject.SetActive(true);
        //timer + anim
        sentenceTMP.gameObject.SetActive(true);

        yield return new WaitForSeconds(3.3f);
        StartCoroutine(SetButtonsActive());
        AnimIndex++; AudioIndex++;
>>>>>>> Stashed changes
    }

}
