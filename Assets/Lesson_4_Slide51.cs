using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lesson_4_Slide51 : Audio_Narration
{
    // Start is called before the first frame update
    public Button nextSlideButton;
    public Animator animator;
    void Start()
    {
        nextSlideButton.gameObject.SetActive(false);
        StartCoroutine(StartSceneCoroutine());
    }
    
    private IEnumerator StartSceneCoroutine()
    {
        yield return new WaitForSeconds(1);

        animator.Play("Lesson_4_Slide51");
        yield return new WaitForSeconds(4);
        SetAudioNarration(0);
        animator.Play("Lesson_4_Slide51_2");
        yield return new WaitForSeconds(clip[0].length);
        SetAudioNarration(1);
        animator.Play("Lesson_4_Slide51_3");
        yield return new WaitForSeconds(clip[1].length);
        nextSlideButton.onClick.AddListener(GoToZogle);
        nextSlideButton.gameObject.SetActive(true);
    }
    
    public void GoToZogle()
    {
        Application.OpenURL("www.zogleber.com");
        LoadScene();
    }
}
