using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Lesson_5_Slide4 : Audio_Narration
{
    [TextArea(3, 10)]
    public string[] bookText;
    public AnimationClip[] animations, bookAnimations;
    int[] audioCount = {2, 1, 1, 1, 2, 1, 2, 1, 1}; //index 0 = slide 7

    public Animator leftScreen, rightScreen, mainCam;
    public GameObject glisten, bookBG;
    
    public AnimationClip cameraClip, bookFlip;
    public Button nextSlideButton, cowBtn;

    int _audioIndex = 1, _animIndex = 1, _bookAnimIndex = 0, _bookTextIndex, 
        _speedMultiplier = 10, _level, _audioFlipIndex = 15;
    bool _isFast = false, _audioHasFinished = false;
    GameObject _bookAnimator;
    TextMeshProUGUI _textTMP;

    void Start()
    {
        _bookAnimator = bookBG.transform.Find("BookAnimator").gameObject;
        mainCam.gameObject.transform.position = new Vector3(0, 0, -10);
        cowBtn.gameObject.SetActive(false);
        glisten.SetActive(false);
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

        yield return new WaitForSeconds(4.5f); //timing on "in the story, ..."
        glisten.SetActive(true);

        yield return new WaitForSeconds(2f); //wait before clickable
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
    }

    void ActivateNextScene()
    {
        ResetScene();
        StartCoroutine(Slide6_Bookflip());
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
        Debug.Log("Current Level: " + _level);
    }

    IEnumerator Slide6_Bookflip()
    {
        if (_level == 9)
        {
            LoadScene();
            yield return new WaitForSeconds(10);
        }

        yield return new WaitForSeconds(1);
        bookBG.GetComponent<Animator>().Play(bookFlip.name);
        _textTMP.gameObject.SetActive(false);
        SetAudioNarration(_audioFlipIndex);

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
        Debug.Log("Current Level: " + _level);
    }

    IEnumerator AudioCountReader()
    {
        var currentAudioCount = audioCount[_level];
        Debug.Log("Audio Count: " + currentAudioCount);

        for (int i = 0; i < currentAudioCount; i++)
        {
            SetAudioNarration(_audioIndex);
            yield return new WaitForSeconds(clip[_audioIndex].length);
            
            _audioIndex++;
        }
        _audioHasFinished = true;
    }
}
