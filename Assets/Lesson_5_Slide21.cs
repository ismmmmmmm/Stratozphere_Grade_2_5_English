using System.Collections;
using UnityEngine;

public class Lesson_5_Slide21 : Audio_Narration
{
    public Animator animator;
    public AnimationClip[] animations;
    int _audioCount, _speedMultiplier = 3;
    bool _isFast = false;

    void Start()
    {
        StartCoroutine(StartScene());
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
            animator.speed = _speedMultiplier;
            source.pitch = _speedMultiplier;
            _isFast = true;
        }

        else if (Input.GetKeyUp(KeyCode.Space) && _isFast)
        {
            Time.timeScale = 1f;
            animator.speed = 1f;
            source.pitch = 1f;
            _isFast= false;
        }
    }

    IEnumerator StartScene()
    {
        yield return new WaitForSeconds(2);
        for (int i = 0; i < animations.Length; i++)
        {
            SetAudioNarration(_audioCount);
            animator.Play(animations[i].name);
            yield return new WaitForSeconds(clip[_audioCount].length);
            _audioCount++;
        }
    }
}
