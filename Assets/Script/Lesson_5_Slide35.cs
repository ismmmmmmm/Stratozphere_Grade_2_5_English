using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86;

public class Lesson_5_Slide35 : Audio_Narration
{
    [Header("Multiple")]
    public GameObject[] choicesBtn;
    Button[] choices;
    [SerializeField] Sprite[] btnSprite, backgroundSprite;
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
    [SerializeField] GameObject btnLayoutGroup, slide40, slide40_sentence;

    GameObject _correctBtn;
    typewriterUI _typewriterUI_S, _typewriterUI_Q;
    int _currentValue, _level, _btnCount, _animIndex = 1, _audioIndex = 1, called, _speedMultiplier = 10;
    bool _isVisible = false, _canClick = true, _isFast;
    Transform _slide40Canvas; Vector3 _layoutGroupDefaultSize;
    Lesson_5_Slide40 _s40;

    void Awake()
    {
        _s40 = slide40.GetComponent<Lesson_5_Slide40>();
        _slide40Canvas = slide40.transform.GetChild(0);
    }

    void Start()
    {
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
            _canClick = true;
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
        slide40_sentence.SetActive(false);
        StartCoroutine(Plain_transition());

        yield return new WaitForSeconds(2); //camera transition on cover full screen, *adjust to fit*
        background.sprite = backgroundSprite[1];
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
        choices = choices.Take(choices.Length - 2).ToArray();
        if (slide40 != null) slide40.SetActive(true);
        if (sentenceTMP != null) sentenceTMP.gameObject.SetActive(false);

        StartCoroutine(Slide40_Sequence());
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
        if (questionTMP != null) questionTMP.text = questionText[_level];
        // typewriterUI_Q = questionTMP.gameObject.GetComponent<typewriterUI>();
        // StartCoroutine(typewriterUI_Q.TypeWriterTMP()); //text animation
    }

    void StartCurrentScene(int currentLevel)
    {
        if (currentLevel < questionText.Length)
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
            Debug.Log("Current Level: " + currentLevel);
        }
        else Debug.Log("Level out of range");
    }

    void ResetScene() //deactivate objects
    {
        if (questionTMP != null) questionTMP.gameObject.SetActive(false);
        if (sentenceTMP != null) sentenceTMP.gameObject.SetActive(false);
        if (nextSlideButton != null) nextSlideButton.gameObject.SetActive(false);
        if (helpBtn != null) helpBtn.gameObject.SetActive(false);

        for (int i = 0; i < choices.Length; i++)
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
        if (_canClick) //wait for x seconds before clicking again
        {
            _currentValue = index;

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

            StartCoroutine(CheckAnswer());
            //         StartCoroutine(EnableClickAfterDelay(3f));
        }
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
        yield return new WaitForSeconds(2f); //start delay before button.interactable
        if (_level <= 4)
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
            _canClick = true;
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
            if (hennika != null) hennika.SetTrigger("Hmm");
            SetAudioNarration(wrongAudioIndex);
            StartCoroutine(EnableClickAfterDelay(3f));
        }
    }

    public bool CheckAnswerDragDrop()
    {
        GameObject droppedObj = slide40_sentence.GetComponent<Lesson_5_slide40_Drop>()._droppedObj;
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
        for (int i = 0;  i < choicesBtn.Length; i++)
        {
            if (choicesBtn[i] != _correctBtn)
            {
                Debug.Log(choicesBtn[i]);
            }
        }

        Vector3 defaultScale = _correctBtn.transform.localScale;
        Debug.Log(defaultScale);
        _correctBtn.GetComponent<Animator>().Play("Bottle_Shrink");
        _correctBtn.transform.position = slide40_sentence.transform.position;

        yield return new WaitForSeconds(2f);
        if (nextSlideButton != null) nextSlideButton.gameObject.SetActive(true);
        nextSlideButton.onClick.AddListener(ActivateNextScene);
        _level++;
    }

    void HelpButton()
    {
        if (helpBtn != null)
        {
            _isVisible = !_isVisible;
            helpBtn.transform.Find("HelpPanel").gameObject.SetActive(_isVisible);
            if (_isVisible)
            {
                SetAudioNarration(helpAudioIndex);
                if (hennika != null) hennika.SetTrigger("Remember");
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

        slide40_sentence.SetActive(true);
        yield return new WaitForSeconds(1.5f); //show sentence -> x seconds -> show choices

        btnLayoutGroup.GetComponent<RectTransform>().anchoredPosition = new Vector3(-40, -95, 0);
        yield return new WaitForEndOfFrame();

        StartCoroutine(SetButtonsActive(() =>
        {
            ActivateQuestion();
        }));

        _animIndex++; _audioIndex++;
    }

}
