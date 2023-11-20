using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lesson_5_Slide35 : Audio_Narration
{
    [Header("Multiple")]
    public GameObject[] choicesBtn;
    Button[] choices;
    [SerializeField] Sprite[] btnSprite, backgroundSprite, sentenceCrate;
    [SerializeField] string[] questionText, sentenceText;
    [SerializeField] int[] correctAnswer;
    [SerializeField] AnimationClip[] animations;

    [Header("Single")]
    [SerializeField] SpriteRenderer background;
    [SerializeField] TextMeshProUGUI questionTMP, sentenceTMP;
    [SerializeField] int correctAudioIndex, wrongAudioIndex, helpAudioIndex;
    [SerializeField] Button nextSlideButton, helpBtn;
    [SerializeField] Animator dolly, hennika, mainCam;
    [SerializeField] RuntimeAnimatorController hennikaS35, hennikaS40;
    [SerializeField] AnimationClip camZoomIn, camZoomOut;
    [SerializeField] GameObject btnLayoutGroup, slide40, visuals, s35_canvas, slide50, sentenceCrateGO, moveToGO;

    GameObject _correctBtn, _slide40_sentence;
    typewriterUI _typewriterUI_S;
    int _currentValue, _level, _btnCount, _animIndex = 1, _audioIndex = 1
        , _speedMultiplier = 10, currentSprite, _wrongIndex;
    int[] wrongAudIndex = { 24, 24, 24, 25, 25 }; //s45-49
    float waitSec;
    bool _isVisible = false, _isFast;
    Transform _slide40Canvas; Vector3 _layoutGroupDefaultSize;
    Lesson_5_Slide40 _s40;
    Vector3 _defaultScale;

    void Awake()
    {
        _slide40_sentence = slide40.transform.GetChild(0).Find("Sentence").gameObject;
        _s40 = slide40.GetComponent<Lesson_5_Slide40>();
        _slide40Canvas = slide40.transform.GetChild(0);
        slide50.SetActive(false);
    }

    void Start()
    {
        Debug.Log(sentenceCrateGO.transform.localPosition);
        ConvertChoicesToButtons();
        ResetScene();
        //        StartCoroutine(InitialSceneSequence_Slide35());
        StartCurrentScene(_level);
        helpBtn.onClick.AddListener(HelpButton);

        //  _typewriterUI_S = sentenceTMP.gameObject.GetComponent<typewriterUI>();
        // _typewriterUI_Q = questionTMP.gameObject.GetComponent<typewriterUI>();
    }

    void Update()
    {
        FastForward();
    }

    void FastForward()
    {
        if (Input.GetKeyUp(KeyCode.Space) && !_isFast)
        {
            Time.timeScale = _speedMultiplier;
            mainCam.speed = _speedMultiplier;
            hennika.speed = _speedMultiplier;
            dolly.speed = _speedMultiplier;
            source.pitch = _speedMultiplier;
            _isFast = true;
        }

        else if (Input.GetKeyUp(KeyCode.Space) && _isFast)
        {
            Time.timeScale = 1f;
            mainCam.speed = 1f;
            hennika.speed = 1f;
            dolly.speed = 1f;
            source.pitch = 1f;
            _isFast = false;
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            // _canClick = true;
            if (choices != null)
            {
                choices[correctAnswer[_level]].image.color = Color.green;
            }

            if (hennika != null) hennika.Play("Hennika_Idle");

            SetAudioNarration(correctAudioIndex);

            if (nextSlideButton != null) nextSlideButton.gameObject.SetActive(true);
            nextSlideButton.onClick.AddListener(ActivateNextScene);
            _level++;
        }
    }

    IEnumerator InitialSceneSequence_Slide35()
    {
        yield return new WaitForSeconds(2); //screen load

        if (camZoomIn != null) mainCam.Play(camZoomIn.name); //camera zoom in
        yield return new WaitForSeconds(camZoomIn.length);

        hennika.runtimeAnimatorController = hennikaS35;
        SetAudioNarration(0); //hennika talks about the challenge
        if (hennika != null && animations != null) hennika.Play(animations[0].name);
        yield return new WaitForSeconds(clip[0].length);

        if (camZoomOut != null) mainCam.Play(camZoomOut.name); //camera zoom out
        yield return new WaitForSeconds(camZoomOut.length);

        StartCoroutine(Slide35_Sequence());
    }

    IEnumerator InitialSceneSequence_Slide40()
    {
        nextButton.SetActive(false);
        _slide40_sentence.SetActive(false);
        StartCoroutine(Plain_transition());

        yield return new WaitForSeconds(2); //camera transition on cover full screen, *adjust to fit*
        background.sprite = backgroundSprite[1];
        visuals.SetActive(true);
        ResetScene();

        hennika.runtimeAnimatorController = hennikaS40;
        if (camZoomIn != null) mainCam.Play(camZoomIn.name); //camera zoom in

        yield return new WaitForSeconds(camZoomIn.length + .5f); //delay before speaking
        SetAudioNarration(9); //hennika talks about the challenge
        if (hennika != null && animations != null) hennika.Play(animations[7].name);

        yield return new WaitForSeconds(clip[9].length - .2f);
        hennika.SetBool("isSpeakDone", true);
        if (camZoomOut != null) mainCam.Play(camZoomOut.name); //camera zoom out

        yield return new WaitForSeconds(camZoomOut.length - 1.5f); //delay before showing sentence

        btnLayoutGroup.transform.SetParent(_slide40Canvas, true);
        btnLayoutGroup.transform.localScale = _layoutGroupDefaultSize;
        btnLayoutGroup.GetComponent<RectTransform>().anchoredPosition = new Vector3(-40, -95, 0);
        choices = choices.Take(choices.Length - 2).ToArray();

        helpAudioIndex = 10; _audioIndex = 13;
        helpBtn.transform.Find("HelpPanel").transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
            "Remember, a plural subject, or a subject that is more than one in quantity, " +
            "takes a plural verb, or the base form of a verb.";
        helpBtn.onClick.RemoveAllListeners();
        helpBtn.onClick.AddListener(HelpButton_s40);

        if (slide40 != null) slide40.SetActive(true);
        if (sentenceTMP != null) sentenceTMP.gameObject.SetActive(false);

        StartCoroutine(Slide40_Sequence());
    }

    IEnumerator InitialSceneSequence_Slide45()
    {
        StartCoroutine(Plain_transition());
        yield return new WaitForSeconds(2); //camera transition on cover full screen, *adjust to fit*
        _slide40_sentence.SetActive(false);
        visuals.SetActive(false);
        helpBtn.gameObject.SetActive(false);
        _s40.GetComponent<Lesson_5_Slide40>().enabled = false;
        ResetScene();
        if (camZoomIn != null) mainCam.Play(camZoomIn.name); //camera zoom in

        yield return new WaitForSeconds(camZoomIn.length + .5f); //delay before speaking
        hennika.SetBool("isSpeakDone", false);
        _audioIndex = 18;
        SetAudioNarration(_audioIndex); //hennika talks about the challenge
        if (hennika != null && animations != null) hennika.Play(animations[7].name);

        yield return new WaitForSeconds(clip[_audioIndex].length - .2f);
        _audioIndex++;
        hennika.SetBool("isSpeakDone", true);
        if (camZoomOut != null) mainCam.Play(camZoomOut.name); //camera zoom out

        yield return new WaitForSeconds(camZoomOut.length - 1.5f); //delay before showing sentence
        Transform vLayoutGroup = btnLayoutGroup.transform.Find("VerticalLayout");
        foreach (Button button in choices)
        {
            button.transform.SetParent(vLayoutGroup, true);
            button.GetComponent<RectTransform>().sizeDelta = new Vector2(1246, 259);
        }
        StartCoroutine(Slide45_Sequence());
    }

    IEnumerator ActivateSentence()
    {
        if (sentenceTMP != null && _level <= 4) //Disable sentence on slide 40+
        {
            sentenceTMP.gameObject.SetActive(false); // Disable the object first
            yield return null; // Wait for the next frame
            sentenceTMP.gameObject.SetActive(true); // Re-enable the object
            sentenceTMP.text = sentenceText[_level];
            _typewriterUI_S = sentenceTMP.gameObject.GetComponent<typewriterUI>();
            StartCoroutine(_typewriterUI_S.TypeWriterTMP());

            SetAudioNarration(_audioIndex); // hennika reads sentence as it appears
            if (hennika != null && animations != null) hennika.Play(animations[_animIndex].name);
        }
    }

    void ActivateQuestion()
    {
        if (questionTMP != null)
        {
            questionTMP.text = questionText[_level];
            questionTMP.gameObject.SetActive(true);
        }

        invisibleWall.SetActive(false);

        // typewriterUI_Q = questionTMP.gameObject.GetComponent<typewriterUI>();
        // StartCoroutine(typewriterUI_Q.TypeWriterTMP()); //text animation
    }

    void ActivateSentenceCrate()
    {
        if (_slide40_sentence != false)
        {
            _slide40_sentence.SetActive(true);
            _slide40_sentence.GetComponent<Image>().sprite = sentenceCrate[currentSprite];
            currentSprite++;
        }
    }

    void StartCurrentScene(int currentLevel)
    {
        invisibleWall.SetActive(true);
        if (currentLevel < questionText.Length + 2) // +s50-51
        {
            if (currentLevel == 0) //first scene of slide35
            {
                StartCoroutine(InitialSceneSequence_Slide35());
            }

            else if (currentLevel == 5) //first scene of slide40
            {
                StartCoroutine(InitialSceneSequence_Slide40());
            }

            else if (currentLevel <= 4) //slide35-39
            {
                StartCoroutine(Slide35_Sequence());
            }

            else if (currentLevel <= 9) //slide40-44
            {
                StartCoroutine(Slide40_Sequence());
            }

            else if (currentLevel == 10) //first scene of slide45
            {
                StartCoroutine(InitialSceneSequence_Slide45());
            }

            else if (currentLevel <= 14) //slide45-49
            {
                StartCoroutine(Slide45_Sequence());
            }

            else if (currentLevel > 14) //s50-51
            {
                StartCoroutine(FinalScene_s50_51());
            }

            Debug.Log("Current Level: " + currentLevel);
        }
        else Debug.Log("Level out of range");
    }

    void ResetScene() //deactivate objects
    {
        if (questionTMP != null) questionTMP.gameObject.SetActive(false);
        if (sentenceTMP != null) sentenceTMP.gameObject.SetActive(false);
        if (_slide40_sentence != null) _slide40_sentence.SetActive(false);
        if (nextSlideButton != null) nextSlideButton.gameObject.SetActive(false);
        if (helpBtn != null) helpBtn.gameObject.SetActive(false);
        if (_correctBtn != null) _correctBtn.transform.localScale = _defaultScale;
        _s40._isCorrect = false;
        ; for (int i = 0; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
            choices[i].interactable = false;
            int index = i;
            choices[i].onClick.RemoveAllListeners();
            choices[i].onClick.AddListener(() => SetButtonsValue(index));
        }
    }

    #region Button Functions
    void ConvertChoicesToButtons()
    {
        _layoutGroupDefaultSize = btnLayoutGroup.transform.localScale;
        choices = new Button[choicesBtn.Length];
        for (int i = 0; i < choicesBtn.Length; i++)
        {
            Button button = choicesBtn[i].GetComponent<Button>();
            choices[i] = button;
        }
    }

    void SetButtonsValue(int index)
    {
        _currentValue = index;

        if (_isVisible)
        {
            HelpButton(); //close open panel after choice button clicked
        }

        if (choices != null)
        {
            foreach (Button button in choices)
            {
                //  button.interactable = false;
            }
        }

        StartCoroutine(CheckAnswer());
        //         StartCoroutine(EnableClickAfterDelay(3f));

    }

    IEnumerator SetButtonsActive(Action onComplete)
    {
        for (int i = 0; i < choices.Length; i++) //set sprite
        {
            choices[i].image.color = Color.white; //reset choice buttons color
            choices[i].GetComponent<Image>().sprite = btnSprite[_btnCount]; _btnCount++;
        }

        yield return new WaitForSeconds(.1f);
        //activate all choices 
        foreach (Button button in choices)
        {
            button.gameObject.SetActive(true);
            if (_level == 3) yield return new WaitForSeconds(1.2f); // faster delay between buttons on faster text
            else yield return new WaitForSeconds(2.5f); //delay between buttons
        }

        //start choices uninteractable
        //yield return new WaitForSeconds(2f); //start delay before button.interactable
        if (_level <= 4 || _level > 9) //s45-49
        {
            foreach (Button button in choices)
            {
                button.interactable = true;
            }
        }

        //wait all choice buttons to show up before activating questionTMP
        if (questionTMP != null) questionTMP.gameObject.SetActive(true);
        if (helpBtn != null) helpBtn.gameObject.SetActive(true);

        onComplete.Invoke();
    }

    IEnumerator isWrong() //prevent clicks after n seconds
    {
        if (_level <= 4)
        {
            invisibleWall.SetActive(true);
            SetAudioNarration(wrongAudioIndex);
            if (hennika != null) hennika.SetTrigger("Hmm");
            if (hennika != null && animations != null) hennika.Play(animations[7].name);

            yield return new WaitForSeconds(clip[wrongAudioIndex].length);
            invisibleWall.SetActive(false);
            /*if (choices != null)
            {
                foreach (Button button in choices)
                {
                    button.interactable = true;
                }
            }*/
        }

        else
        {
            invisibleWall.SetActive(true);
            SetAudioNarration(wrongAudIndex[_wrongIndex]);
            hennika.SetBool("isSpeakDone", false);
            if (hennika != null && animations != null) hennika.Play(animations[7].name);

            yield return new WaitForSeconds(clip[wrongAudioIndex].length);
            invisibleWall.SetActive(false);
            hennika.SetBool("isSpeakDone", true);

            _wrongIndex++;
        }
    }

    IEnumerator CheckAnswer() //check if clicked choice index == int correctAnswer
    {
        if (_currentValue == correctAnswer[_level]) //is correct
        {
            //  _canClick = true;
            if (choices != null)
            {
                choices[correctAnswer[_level]].image.color = Color.green;
                choices[correctAnswer[_level]].GetComponent<Animator>().Play("Choices_Bounce");
            }



            if (hennika != null) hennika.Play("Hennika_Idle");

            SetAudioNarration(correctAudioIndex);
            yield return new WaitForSeconds(.2f);

            if (nextSlideButton != null) nextSlideButton.gameObject.SetActive(true);
            nextSlideButton.onClick.AddListener(ActivateNextScene);
            _level++;
        }

        else                                       //is wrong
        {
            StartCoroutine(isWrong());
        }
    }

    public bool CheckAnswerDragDrop()
    {
        if (_isVisible)
        {
            HelpButton(); //close open panel after choice button clicked
        }
        // GameObject droppedObj = _slide40_sentence.GetComponent<Lesson_5_slide40_Drop>()._droppedObj;
        GameObject droppedObj = _s40._objectBeingDragged;
        if (droppedObj != null)
        {
            int index = _s40._draggedIndex;
            if (index == correctAnswer[_level]) //is correct
            {
                _correctBtn = choicesBtn[index];
                return true;
            }

            else                                //is wrong
            {
                return false;
            }
        }
        return false;
    }

    public IEnumerator CorrectAnswerDragDrop()
    {
        invisibleWall.SetActive(true);
        SetAudioNarration(correctAudioIndex);

        /*for (int i = 0; i < choicesBtn.Length; i++)
        {
            if (choicesBtn[i] != _correctBtn)
            {
                Debug.Log(choicesBtn[i]);
            }
        }*/

        _defaultScale = _correctBtn.transform.localScale;
        _correctBtn.GetComponent<Animator>().Play("Bottle_Shrink");

        yield return new WaitForSeconds(2f);
        invisibleWall.SetActive(false);
        if (nextSlideButton != null) nextSlideButton.gameObject.SetActive(true);
        nextSlideButton.onClick.AddListener(ActivateNextScene);
        _level++;
    }

    void HelpButton()
    {
        StartCoroutine(HelpButton_s35_Enum());
    }

    IEnumerator HelpButton_s35_Enum()
    {
        if (helpBtn != null)
        {
            _isVisible = !_isVisible;
            helpBtn.transform.Find("HelpPanel").gameObject.SetActive(_isVisible);
            if (_isVisible)
            {
                invisibleWall.SetActive(true);
                SetAudioNarration(helpAudioIndex);
                if (hennika != null) hennika.SetTrigger("Remember");
                yield return new WaitForSeconds(clip[helpAudioIndex].length);
                invisibleWall.SetActive(false);
            }
        }
    }

    void HelpButton_s40()
    {
        StartCoroutine(HelpButton_s40_Enum());
    }

    IEnumerator HelpButton_s40_Enum()
    {
        if (helpBtn != null)
        {
            _isVisible = !_isVisible;
            helpBtn.transform.Find("HelpPanel").gameObject.SetActive(_isVisible);
            if (_isVisible)
            {
                invisibleWall.SetActive(true);
                hennika.SetBool("isSpeakDone", false);
                SetAudioNarration(helpAudioIndex);
                if (hennika != null) hennika.SetTrigger("s40_anim");
                yield return new WaitForSeconds(clip[helpAudioIndex].length);
                hennika.SetBool("isSpeakDone", true);
                invisibleWall.SetActive(false);
            }
        }
    }

    #endregion

    void ActivateNextScene()
    {
        nextSlideButton.onClick.RemoveAllListeners();
        if (_level != 5) ResetScene();
        StartCurrentScene(_level);
    }

    IEnumerator Slide35_Sequence()
    {
        background.sprite = backgroundSprite[0];
        yield return new WaitForSeconds(1);

        StartCoroutine(ActivateSentence());

        yield return new WaitForSeconds(4); //Delay before buttons show up
        StartCoroutine(SetButtonsActive(() =>
        {
            ActivateQuestion();
        }));

        _animIndex++; _audioIndex++;
    }

    IEnumerator Slide40_Sequence()
    {
        yield return new WaitForSeconds(2); //wait x seconds before showing Slide40 UI
        hennika.SetBool("isSpeakDone", false);
        ActivateSentenceCrate();
        SetAudioNarration(_audioIndex);
        if (hennika != null && animations != null) hennika.Play(animations[7].name);
        waitSec = 4f;

        yield return new WaitForSeconds(waitSec);
        StartCoroutine(SetButtonsActive(() =>
        {
            ActivateQuestion();
        }));

        yield return new WaitForSeconds(clip[_audioIndex].length - waitSec); //show sentence -> x seconds -> show choices
        hennika.SetBool("isSpeakDone", true);
        _audioIndex++;
    }

    IEnumerator Slide45_Sequence()
    {
        yield return new WaitForSeconds(2); //wait x seconds before showing Slide40 UI
        hennika.SetBool("isSpeakDone", false);
        ActivateSentenceCrate();
        SetAudioNarration(_audioIndex); //read sentence
        if (hennika != null && animations != null) hennika.Play(animations[7].name);
        waitSec = 2f;

        yield return new WaitForSeconds(waitSec);
        StartCoroutine(SetButtonsActive(() =>
        {
            ActivateQuestion();
        }));

        yield return new WaitForSeconds(clip[_audioIndex].length - waitSec); //show sentence -> x seconds -> show choices
        hennika.SetBool("isSpeakDone", true);
        _audioIndex++;
    }

    IEnumerator FinalScene_s50_51()
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(Plain_transition());                            //fix

        yield return new WaitForSeconds(2); //camera transition on cover full screen, *adjust to fit*
        s35_canvas.SetActive(false);
        slide50.SetActive(true);
        slide40.SetActive(false);
        hennika.gameObject.SetActive(false);
        dolly.gameObject.SetActive(false);
        _audioIndex = 26;

        /*yield return new WaitForSeconds(1);
        SetAudioNarration(_audioIndex);
        slide50Anim.Play("Slide1-4_Speak");

        yield return new WaitForSeconds(clip[_audioIndex].length);
        _audioIndex++;
        SetAudioNarration(_audioIndex);

        yield return new WaitForSeconds(clip[_audioIndex].length);
        _audioIndex++;
        slide50Anim.SetBool("isSpeakDone", true);*/
    }
}
