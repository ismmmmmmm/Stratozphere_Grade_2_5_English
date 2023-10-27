using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lesson_4_Slide53 : Audio_Narration
{
    [SerializeField] public string[] animName;
    public GameObject[] canvas;
    public Button nextSlideButton;
    public Animator slide3Animation,transit;
    public int animCount;
    void Start()
    {
        nextSlideButton.gameObject.SetActive(false);
        nextSlideButton.onClick.AddListener(NextPage);
        StartCoroutine(StartScene());
    }

    private IEnumerator StartScene()
    {
        yield return new WaitForSeconds(4);
        for(int i = 0; i < animName.Length; i++)
        {
            SetAudioNarration(animCount);
            slide3Animation.Play(animName[i]);
            yield return new WaitForSeconds(clip[animCount].length+.2f);animCount++;
        }
        nextSlideButton.gameObject.SetActive(true);

    }
    public void NextPage()
    {
        StartCoroutine(NextPageCoroutine());
    }

    private IEnumerator NextPageCoroutine()
    {
        transit.Play("Plain_Transition");
        yield return new WaitForSeconds(1.5f);
        canvas[0].SetActive(false);
        canvas[1].SetActive(true);
        transit.Play("Plain_Transition_Reverse");        
        yield return new WaitForSeconds(1);
    }
}
