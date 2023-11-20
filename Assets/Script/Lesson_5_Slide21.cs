using System.Collections;
using UnityEngine;

public class Lesson_5_Slide21 : Audio_Narration
{
    public Slide_20_Draw s20Draw;
    public GameObject s20, s20Sentence;
    public Animator animator, s20Hennika, s21Hennika;
    public AnimationClip[] animations;
    int _audioIndex, _speedMultiplier = 20;
    bool _isFast = false;

    void Start()
    {
        s20Sentence.SetActive(false);
        s21Hennika.gameObject.SetActive(false);
        StartCoroutine(StartSceneS20());
    }

    void Update()
    {
        //FastForward();
    }

    void FastForward()
    {
        if (Input.GetKeyUp(KeyCode.Space) && !_isFast)
        {
            Time.timeScale = _speedMultiplier;
            animator.speed = _speedMultiplier;
            source.pitch = _speedMultiplier;
            _isFast = true;
        }

        else if (Input.GetKeyUp(KeyCode.Space) && _isFast)
        {
            Time.timeScale = 1f;
            animator.speed = 1f;
            source.pitch = 1f;
            _isFast = false;
        }
    }

    public void StartS21()
    {
        StartCoroutine(StartS21Enum());
    }

    IEnumerator StartS21Enum()
    {
        StartCoroutine(Plain_transition());

        yield return new WaitForSeconds(2);
        s20Draw.ClearLines();
        s21Hennika.gameObject.SetActive(true);
        s20.gameObject.SetActive(false);
        StartCoroutine(StartSceneS21());
    }

    IEnumerator StartSceneS20()
    {
        invisibleWall.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        SetAudioNarration(_audioIndex);
        s20Hennika.Play("Slide20_Speak");

        float waitTime = 4f;

        yield return new WaitForSeconds(waitTime);
        s20Sentence.SetActive(true);

        yield return new WaitForSeconds(clip[_audioIndex].length - waitTime);
        _audioIndex++;
        s20Hennika.SetBool("isSpeakDone", true);
        invisibleWall.SetActive(false);
    }

    IEnumerator StartSceneS21()
    {
        yield return new WaitForSeconds(2);
        for (int i = 0; i < animations.Length; i++)
        {
            SetAudioNarration(_audioIndex);
            animator.Play(animations[i].name);
            yield return new WaitForSeconds(clip[_audioIndex].length);
            _audioIndex++;
        }

        nextButton.SetActive(true);
    }
}
