using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lesson_5_Slide4 : Audio_Narration
{
    public Animator leftScreen, rightScreen, mainCam;
    public GameObject glisten;
    public AnimationClip[] animations;
    public AnimationClip cameraClip;
    public Button nextSlideButton, cowBtn;
    int _audioIndex = 1, _animIndex = 1, _speedMultiplier = 10, _level;
    bool _isFast = false;

    void Start()
    {
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
        yield return new WaitForSeconds(4f);
        glisten.SetActive(true);
        cowBtn.gameObject.SetActive(true);
        cowBtn.onClick.AddListener(CowBtn);
        _audioIndex++; _animIndex++;
    }

    void ResetScene()
    {
        nextSlideButton.onClick.RemoveAllListeners();
        if (nextSlideButton != null) nextSlideButton.gameObject.SetActive(false);
    }

    void ActivateNextScene()
    {
        ResetScene();
        StartCoroutine(SceneSequence());
    }

    void CowBtn()
    {
        Debug.Log("Clicked");
    }

    IEnumerator SceneSequence()
    {
        yield return new WaitForSeconds(2f);

        SetAudioNarration(_audioIndex);
        rightScreen.Play(animations[_animIndex].name);
        yield return new WaitForSeconds(clip[_audioIndex].length);
        _audioIndex++; _animIndex++;
    }
}
