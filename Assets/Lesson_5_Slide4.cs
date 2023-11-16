using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lesson_5_Slide4 : Audio_Narration
{
    [TextArea(3, 10)]
    public string[] bookText;
    public AnimationClip[] animations, bookAnimations;
    int[] audioCount = { 2, 1, 1, 1, 2, 1, 2, 1, 1 }, //index 0 = slide 7
        correctAud = { 23, 24 }, wrongAud = { 25, 26 };
    // public int[] correctAnswer;
    public Sprite[] choice1Sprite, choice2Sprite;

    public Animator leftScreen, rightScreen, mainCam;
    public GameObject glisten, bookBG, hennika;
    public Sprite cowStableClosedDoor, check, cross;
    public Image checkA, checkB;

    public AnimationClip cameraClip, bookFlip;
    public Button nextSlideButton, cowBtn, zogleber, choice1, choice2;

    int _audioIndex = 1, _animIndex = 1, _bookAnimIndex = 0, _bookTextIndex,
        _speedMultiplier = 20, _level, _bookFlipAudio, _choiceIndex;
    bool _isFast = false, _audioHasFinished = false;
    GameObject _bookAnimator;
    TextMeshProUGUI _textTMP;
    Animator hennikaAnim;
    string urlToOpen = "https://www.zogleber.com", animToPlay = "Slide17_Hennika_Speak";

    void Start()
    {
        if (hennika != null) hennikaAnim = hennika.GetComponent<Animator>();
        _bookFlipAudio = clip.Length - 1; //last index of audio clip
        _bookAnimator = bookBG.transform.Find("BookAnimator").gameObject;
        mainCam.gameObject.transform.position = new Vector3(0, 0, -10);
        if (cowBtn != null) cowBtn.gameObject.SetActive(false);
        if (glisten != null) glisten.SetActive(false);
        if (hennika != null) hennika.SetActive(false);
        if (zogleber != null) zogleber.gameObject.SetActive(false);
        StartCoroutine(InitialScene());
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
            //   animator.speed = _speedMultiplier;
            source.pitch = _speedMultiplier;
            _isFast = true;
        }

        else if (Input.GetKeyUp(KeyCode.Space) && _isFast)
        {
            Time.timeScale = 1f;
            //     animator.speed = 1f;
            source.pitch = 1f;
            _isFast = false;
        }
    }

    IEnumerator InitialScene()
    {
        yield return new WaitForSeconds(2.2f);
        if (mainCam != null) mainCam.Play("Camera_Go_Right");

        yield return new WaitForSeconds(cameraClip.length);
        SetAudioNarration(0);
        rightScreen.Play(animations[0].name);

        yield return new WaitForSeconds(clip[0].length);

        if (nextSlideButton != null)
        {
            if (nextSlideButton != null) nextSlideButton.gameObject.SetActive(true);
            nextSlideButton.onClick.AddListener(Slide5);
        }
    }

    void Slide5()
    {
        ResetScene();
        StartCoroutine(Slide5Enum());
    }

    IEnumerator Slide5Enum()
    {
        SetAudioNarration(_audioIndex);
        rightScreen.Play(animations[_animIndex].name);

        yield return new WaitForSeconds(clip[_audioIndex].length);//timing on "in the story, ..."
        glisten.SetActive(true);

        //yield return new WaitForSeconds(2f); //wait before clickable
        cowBtn.gameObject.SetActive(true);
        cowBtn.onClick.AddListener(CowBtn);
        _audioIndex++; _animIndex++;
    }

    void ResetScene()
    {
        if (nextSlideButton != null)
        {
            nextSlideButton.onClick.RemoveAllListeners();
            nextSlideButton.gameObject.SetActive(false);
        }

        if (cowBtn != null && glisten != null)
        {
            cowBtn.gameObject.SetActive(false);
            cowBtn.onClick.RemoveAllListeners();
            glisten.SetActive(false);
        }

        if (choice1 != null && choice2 != null)
        {
            if (choice1.enabled && choice2.enabled)
            {
                choice1.onClick.RemoveAllListeners();
                choice2.onClick.RemoveAllListeners();
                choice1.onClick.AddListener(() => CheckAnswer(choice1.gameObject));
                choice2.onClick.AddListener(() => CheckAnswer(choice2.gameObject));
            }
        }
        invisibleWall.SetActive(false);
        choice1.interactable = false; choice2.interactable = false;
        choice1.gameObject.SetActive(false); choice2.gameObject.SetActive(false);
        checkA.gameObject.SetActive(false); checkB.gameObject.SetActive(false);
    }

    void LevelCounter()
    {
        Debug.Log("Current Level: " + _level);

        if (_level <= 13) //s6-s19
        {
            if (_level == 9)
            {
                StartCoroutine(FinalScene_s16());
            }

            else if (_level == 10) //s17
            {
                StartCoroutine(Slide_17_Initial());
            }

            else if (_level < 9)
            {
                StartCoroutine(Slide6_Bookflip());
            }

            else if (_level == 12)  //s19
            {
                StartCoroutine(FinalScene_s19());
            }
        }
        else Debug.Log("OUT OF LEVELS");
    }

    void ActivateNextScene()
    {
        ResetScene();
        LevelCounter();
    }

    void CowBtn()
    {
        ResetScene();
        StartCoroutine(InitialSlide6_Bookflip());
    }

    IEnumerator InitialSlide6_Bookflip()
    {
        ResetScene();
        StartCoroutine(Plain_transition());

        yield return new WaitForSeconds(2); //half stratoz anim
        /*mainCam.enabled = false;
        mainCam.GetComponent<Camera>().orthographicSize = _camDefaultViewSize;
        var camPos = mainCam.transform.position;
        mainCam.transform.position = new Vector3(19.38f, 0.07f, camPos.z);*/
        mainCam.Play("Cam_Slide6_Zoom");

        if (leftScreen != null && rightScreen != null)
        {
            leftScreen.gameObject.SetActive(false);
            rightScreen.gameObject.SetActive(false);
        }
        if (bookBG != null) bookBG.SetActive(true);

        _textTMP = bookBG.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        if (_textTMP != null)
        {
            _textTMP.text = bookText[_bookTextIndex]; _bookTextIndex++;
        }
        yield return new WaitForSeconds(2.5f); //plain transition
        SetAudioNarration(_audioIndex);

        yield return new WaitForSeconds(clip[_audioIndex].length);
        if (nextSlideButton != null)
        {
            nextSlideButton.gameObject.SetActive(true);
            nextSlideButton.onClick.AddListener(ActivateNextScene);
        }
        _audioIndex++; _animIndex++;
        //Debug.Log("Current Level: " + _level);
    }

    IEnumerator Slide6_Bookflip()
    {
        /*if (_level == 9)
        {
            StartCoroutine(FinalScene());
        }

        if (_level == 10)
        {
            LoadScene();
            yield return new WaitForSeconds(10);
        }*/

        yield return new WaitForSeconds(1);
        bookBG.GetComponent<Animator>().Play(bookFlip.name);
        _textTMP.gameObject.SetActive(false);
        SetAudioNarration(_bookFlipAudio);

        yield return new WaitForSeconds(.5f);
        _bookAnimator.SetActive(false);

        yield return new WaitForSeconds(bookFlip.length - 1f); //next page
        if (_bookAnimator != null) _bookAnimator.SetActive(true);
        if (_textTMP != null)
        {
            _textTMP.gameObject.SetActive(true);
            _textTMP.text = bookText[_bookTextIndex];
        }
        _bookAnimator.GetComponent<Animator>().Play(bookAnimations[_bookAnimIndex].name);
        _audioHasFinished = false;
        StartCoroutine(AudioCountReader());
        while (_audioHasFinished == false)
        {
            yield return null;
        }
        //yield return new WaitForSeconds(clip[_audioIndex].length + 1f);
        if (nextSlideButton != null)
        {
            nextSlideButton.gameObject.SetActive(true);
            nextSlideButton.onClick.AddListener(ActivateNextScene);
        }

        _bookAnimIndex++; _level++; _bookTextIndex++;
        //Debug.Log("Current Level: " + _level);
    }

    IEnumerator AudioCountReader()
    {
        var currentAudioCount = audioCount[_level];

        for (int i = 0; i < currentAudioCount; i++)
        {
            SetAudioNarration(_audioIndex);
            yield return new WaitForSeconds(clip[_audioIndex].length);

            _audioIndex++;
        }
        _audioHasFinished = true;
    }

    IEnumerator FinalScene_s16()
    {
        yield return new WaitForSeconds(1);
        rightScreen.GetComponent<SpriteRenderer>().sprite = cowStableClosedDoor;
        rightScreen.GetComponent<Animator>().enabled = false;
        if (hennika != null) hennika.SetActive(true);
        bookBG.SetActive(false);
        rightScreen.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);
        SetAudioNarration(_audioIndex);
        hennikaAnim.Play("Slide16_Hennika_Speak'");
        yield return new WaitForSeconds(12);
        if (zogleber != null) zogleber.gameObject.SetActive(true);
        //yield return new WaitForSeconds(clip[_audioIndex].length-wait);
        _audioIndex++;
    }

    public void GoToZogleber2()
    {
        StartCoroutine(GoToZogleberEnum());
    }

    IEnumerator GoToZogleberEnum()
    {
        Application.OpenURL(urlToOpen);                         
        yield return new WaitForSeconds(2f);
        nextSlideButton.onClick.RemoveAllListeners();
        nextSlideButton.onClick.AddListener(ActivateNextScene);
        if (nextSlideButton != null) nextSlideButton.gameObject.SetActive(true);
        if (zogleber != null) zogleber.gameObject.SetActive(false);
        _animIndex = 3; _level++;
    }

    void Slide_17()
    {
        StartCoroutine(Slide_17_Enum());
    }

    IEnumerator Slide_17_Initial()
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(Plain_transition());                         //fix
        nextSlideButton.onClick.RemoveAllListeners();
        nextSlideButton.onClick.AddListener(Slide_17);
        StartCoroutine(Slide_17_Enum());
    }
    IEnumerator Slide_17_Enum()
    {
        Debug.Log("Current Level: " + _level);
        yield return new WaitForSeconds(1);
        ResetScene();
        choice1.image.sprite = choice1Sprite[_choiceIndex]; choice2.image.sprite = choice2Sprite[_choiceIndex];
        _choiceIndex++;

        SetAudioNarration(_audioIndex); //sentence
        hennikaAnim.Play(animToPlay);

        yield return new WaitForSeconds(clip[_audioIndex].length); //choice1
        _audioIndex++;
        // _animIndex = 3;
        choice1.gameObject.SetActive(true);

        yield return new WaitForSeconds(.3f);
        SetAudioNarration(_audioIndex);
        hennikaAnim.Play(animations[_animIndex].name);

        yield return new WaitForSeconds(clip[_audioIndex].length); //choice2
        _audioIndex++; _animIndex++;
        choice2.gameObject.SetActive(true);

        yield return new WaitForSeconds(.3f);
        SetAudioNarration(_audioIndex);
        hennikaAnim.Play(animations[_animIndex].name);

        yield return new WaitForSeconds(clip[_audioIndex].length);
        _audioIndex++; _animIndex++;
        choice1.interactable = true; choice2.interactable = true;
        animToPlay = "Slide18_Hennika_Speak";
    }

    IEnumerator FinalScene_s19()
    {
        yield return new WaitForSeconds(1);
        SetAudioNarration(_audioIndex);
        hennikaAnim.Play("Slide19_Hennika_Speak");
        yield return new WaitForSeconds(3);
        if (zogleber != null) zogleber.gameObject.SetActive(true);
        _audioIndex++;
    }

    void CheckAnswer(GameObject choice)
    {
        StartCoroutine(CheckAnswerEnum(choice));
    }

    IEnumerator CheckAnswerEnum(GameObject choice)
    {
        if (_level == 10)
        {
            if (choice == choice2.gameObject)//is correct
            {
                Debug.Log("Correct");
                checkB.sprite = check;
                checkB.gameObject.SetActive(true);

                SetAudioNarration(correctAud[0]);
                hennikaAnim.Play("Slide17_Hennika_Correct");
                invisibleWall.SetActive(true);

                yield return new WaitForSeconds(clip[correctAud[0]].length);
                nextSlideButton.onClick.RemoveAllListeners();
                nextSlideButton.onClick.AddListener(Slide_17);
                nextSlideButton.gameObject.SetActive(true);
                _level++;
            }

            else                             //is wrong
            {
                Debug.Log("Wrong");
                checkA.sprite = cross;
                checkA.gameObject.SetActive(true);

                SetAudioNarration(wrongAud[0]);
                hennikaAnim.Play("Slide17_Hennika_Wrong");
                invisibleWall.SetActive(true);

                yield return new WaitForSeconds(clip[wrongAud[0]].length);
                invisibleWall.SetActive(false);
                /*nextSlideButton.onClick.RemoveAllListeners();
                nextSlideButton.onClick.AddListener(Slide_17);
                nextSlideButton.gameObject.SetActive(true);
                _level++;*/
            }
        }

        else if (_level == 11)
        {
            if (choice == choice1.gameObject)//is correct
            {
                Debug.Log("Correct");
                checkA.sprite = check;
                checkA.gameObject.SetActive(true);

                SetAudioNarration(correctAud[1]);
                hennikaAnim.Play("Slide18_Hennika_Correct");
                invisibleWall.SetActive(true);

                yield return new WaitForSeconds(clip[correctAud[1]].length);
                nextSlideButton.onClick.RemoveAllListeners();
                nextSlideButton.onClick.AddListener(ActivateNextScene);
                nextSlideButton.gameObject.SetActive(true);
                _level++;
            }

            else                             //is wrong
            {
                Debug.Log("Wrong");
                checkB.sprite = cross;
                checkB.gameObject.SetActive(true);

                SetAudioNarration(wrongAud[1]);
                hennikaAnim.Play("Slide18_Hennika_Wrong");
                invisibleWall.SetActive(true);

                yield return new WaitForSeconds(clip[wrongAud[1]].length);
                invisibleWall.SetActive(false);
                /*nextSlideButton.onClick.RemoveAllListeners();
                nextSlideButton.onClick.AddListener(ActivateNextScene);
                nextSlideButton.gameObject.SetActive(true);
                _level++;*/
            }
        }
        else Debug.Log("OUT OF INDEX");
    }
}
