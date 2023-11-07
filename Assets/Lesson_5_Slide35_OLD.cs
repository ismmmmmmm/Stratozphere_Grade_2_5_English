using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Lesson_5_Slide35_OLD : Audio_Narration
{
    [Header("Multiple")]
    public Button[] choices;
    public Sprite[] btnSprite, backgroundSprite;
    public string[] questionText, sentenceText;
    public int[] correctAnswer;
    public AnimationClip[] animations;

    [Header("Single")]
    //public
    public TextMeshProUGUI questionTMP, sentenceTMP;
    public int correctAudioIndex, wrongAudioIndex, helpAudioIndex;
    public Button nextSlideButton, helpBtn;
    public Animator dolly, hennika, mainCam;
    public AnimationClip camZoomIn, camZoomOut;
    public GameObject btnLayoutGroup, slide40;
    typewriterUI typewriterUI_S, typewriterUI_Q;
    public SpriteRenderer background;

    //private
    int _currentValue, _level, _btnCount, _animIndex = 0, _audioIndex = 0;
    bool _isVisible = false, _canClick = true;
    

    void Start()
    {
        slide40.SetActive(false);
        ResetScene();
        nextSlideButton.onClick.AddListener(NextScene);
        helpBtn.onClick.AddListener(HelpButton);
        StartCoroutine(StartScene());
    }

    void Update() //for playtesting purposes
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 8;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Time.timeScale = 1;
        }
    }

    void ResetScene() //deactivate objects
    {
        if (questionTMP != null) questionTMP.gameObject.SetActive(false);
        if (sentenceTMP != null) sentenceTMP.gameObject.SetActive(false); 
        if (helpBtn != null) helpBtn.gameObject.SetActive(false);

        for (int i = 0; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
            choices[i].interactable = false;
            int index = i;
            choices[i].onClick.AddListener(() => SetValue(index));
        }
    }

    void SetSentenceText()
    {
        int index = 0;
        //if (sentenceTMP != null) sentenceTMP.text = sentenceText[index];

     //   typewriterUI_S = sentenceTMP.GetComponent<typewriterUI>();
     //   StartCoroutine(typewriterUI_S.TypeWriterTMP());
        typewriterUI_Q = questionTMP.GetComponent<typewriterUI>();
        index++;
    }

    IEnumerator SetButtonsActive()
    {
        //set sprite   //change color to white
        for (int i = 0; i < choices.Length; i++)
        {
            choices[i].image.color = Color.white;
            choices[i].GetComponent<Image>().sprite = btnSprite[_btnCount]; _btnCount++;
        }
        yield return new WaitForSeconds(.1f);
        //activate all choices 
        foreach (Button button in choices)
        {
            button.gameObject.SetActive(true);
            yield return new WaitForSeconds(2.5f); //delay between buttons
        }

        //start choices uninteractable
        yield return new WaitForSeconds(2f); //start delay before button.interactable
        foreach (Button button in choices)
        {
            button.interactable = true;
        }

        if (questionTMP != null) questionTMP.gameObject.SetActive(true);
        if (helpBtn != null) helpBtn.gameObject.SetActive(true);
    }

    void HelpButton()
    {
        if (helpBtn != null)
        {
            _isVisible = !_isVisible;
            Debug.Log("Help button clicked!");
            helpBtn.transform.Find("HelpPanel").gameObject.SetActive(_isVisible);
            if (_isVisible)
            {
                SetAudioNarration(helpAudioIndex);
                if (hennika != null) hennika.SetTrigger("Remember");
            }
        }
    }

    public void SetValue(int index)
    {
        if (_canClick) //wait for x seconds before clicking again
        {
            _currentValue = index;
            StartCoroutine(CheckAnswer());
            if (_isVisible)
            {
                HelpButton();
            }

            _canClick = false;

            if (choices != null)
            {
                foreach (Button button in choices)
                {
                    button.interactable = false;
                }
            }
            StartCoroutine(EnableClickAfterDelay(3f));
        }
    }

    IEnumerator EnableClickAfterDelay(float delay) //prevent clicks after n seconds
    {
        yield return new WaitForSeconds(delay);
        _canClick = true;

        if (choices != null)
        {
            foreach (Button button in choices)
            {
                button.interactable = true;
            }
        }
    }

    IEnumerator CheckAnswer() //check if clicked choice index == int correctAnswer 
    {
        if (_currentValue == correctAnswer[_level]) //is correct
        {
            if (choices != null)
            {
                choices[correctAnswer[_level]].image.color = Color.green;
                choices[correctAnswer[_level]].GetComponent<Animator>().Play("Choices_Bounce");
            }
            
            if (hennika != null) hennika.Play("Hennika_Idle");
            
            SetAudioNarration(correctAudioIndex);
            yield return new WaitForSeconds(.2f);

            if (nextSlideButton != null) nextSlideButton.gameObject.SetActive(true);
                //level++;
        }

        else //is wrong
        {
            if (hennika != null) hennika.SetTrigger("Hmm"); 

            SetAudioNarration(wrongAudioIndex);
            Debug.Log("Wrong");
        }
    }

    IEnumerator StartScene() //initial scene
    {
        background.sprite = backgroundSprite[0];

        yield return new WaitForSeconds(2); //screen load

        if (camZoomIn != null) mainCam.Play(camZoomIn.name); //camera zoom

        yield return new WaitForSeconds(camZoomIn.length);

        //hennika talks about the challenge
        SetAudioNarration(_audioIndex);
        
        if (hennika != null && animations != null) hennika.Play(animations[_animIndex].name);
        
        yield return new WaitForSeconds(clip[_animIndex].length);
        _animIndex++; _audioIndex++;

        Debug.Log("Start Scene"); //camera zoom out

        if (camZoomOut != null)mainCam.Play(camZoomOut.name);
        
        yield return new WaitForSeconds(camZoomOut.length + .5f);
        //if (questionTMP != null) questionTMP.text = questionText[_level];
        StartCoroutine(StartNextScene()); //Reads sentence
        SetSentenceText();
    }

    void NextScene() //after clicking nextSlideButton
    {   
        _level++;
        if (_level < questionText.Length)
        {
            /*if (nextSlideButton != null) nextSlideButton.gameObject.SetActive(false);
            if (questionTMP != null) questionTMP.text = questionText[_level];
            if (sentenceTMP != null && _level < 5) sentenceTMP.text = sentenceText[_level];

            StartCoroutine(typewriterUI_S.TypeWriterTMP()); //text animation
            StartCoroutine(typewriterUI_Q.TypeWriterTMP());
            ResetScene(); //disable objects
            //wait 2 sec before next scene*/
            StartCoroutine(StartNextScene());
            Debug.Log("Current Level: " + _level);
        }
        
        else Debug.Log("OUT OF INDEX");
        
    }

    void Slide40()
    {
        slide40.SetActive(true);
        background.sprite = backgroundSprite[1];
        sentenceTMP.gameObject.SetActive(false);
        choices = choices.Take(choices.Length - 2).ToArray();
        btnLayoutGroup.GetComponent<RectTransform>().anchoredPosition = new Vector3(-40, -132, 0);
        StartCoroutine(typewriterUI_Q.TypeWriterTMP());
        //if (questionTMP != null) questionTMP.text = questionText[_level];
    }

    IEnumerator StartNextScene() //Reusable Audio and Animation
    {
        if (_level < 5)
        {
            Debug.Log("Level: " + _level + " is < 5");
            if (nextSlideButton != null) nextSlideButton.gameObject.SetActive(false);
            if (questionTMP != null) questionTMP.text = questionText[_level];
            if (sentenceTMP != null) sentenceTMP.text = sentenceText[_level];

            typewriterUI_S = sentenceTMP.gameObject.GetComponent<typewriterUI>();
            StartCoroutine(typewriterUI_S.TypeWriterTMP()); //text animation
            
            ResetScene(); //disable objects
            //wait 2 sec before next scene
            //StartCoroutine(typewriterUI_Q.TypeWriterTMP());

            SetAudioNarration(_audioIndex); //hennika reads sentence as it appears
            if (hennika != null && animations != null) hennika.Play(animations[_animIndex].name);
            if (sentenceTMP != null) sentenceTMP.gameObject.SetActive(true);

            yield return new WaitForSeconds(3.3f);
            StartCoroutine(SetButtonsActive());
        }

        else if (_level >= 5)
        {
            Debug.Log("Next Scene");
            StartCoroutine(Plain_transition());
            yield return new WaitForSeconds(2);
            Slide40();
            yield return new WaitForSeconds(2);
            StartCoroutine(SetButtonsActive()); //remove last 2 elements before activating
        }
        
        _animIndex++; _audioIndex++;
    }
}
