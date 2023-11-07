using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Slide10_16 : Audio_Narration
{
    public Animator[] book;
    public GameObject[] slide;
    public GameObject[] canvas;
    public Animator transit;
    public Button nextSlideButton;
    public int SceneCount = 4;
    private int animCount = 0;
    private int audioCount;

    // Start is called before the first frame update
    void Start()
    {
        nextSlideButton.onClick.AddListener(NextPage);
        StartCoroutine(StartScene());
    }

    public void NextPage()
    {
        nextButton.SetActive(false);
        StartCoroutine(Next());
    }

    private IEnumerator StartScene()
    {
        invisibleWall.SetActive(true);
        yield return new WaitForSeconds(1);
        slide[SceneCount].SetActive(true);
        SetAudioNarration(SceneCount);
        yield return new WaitForSeconds(clip[SceneCount].length);
        SceneCount++;
        invisibleWall.SetActive(false);
        nextSlideButton.gameObject.SetActive(true);

        // Check if SceneCount is equal to the length of the slide array
        if (SceneCount == slide.Length)
        {
            // Add FinalCount method as a listener to nextSlideButton
            nextSlideButton.onClick.AddListener(FinalCount);
        }
    }

    private IEnumerator Next()
    {
        invisibleWall.SetActive(true);
        Debug.Log(SceneCount);
        book[animCount].Play("SLide_10");
        animCount++;
        yield return new WaitForSeconds(2f);
        slide[SceneCount - 1].SetActive(false);
        slide[SceneCount].SetActive(true);
        SetAudioNarration(SceneCount);
        yield return new WaitForSeconds(clip[SceneCount].length);
        SceneCount++;
        invisibleWall.SetActive(false);
        nextSlideButton.gameObject.SetActive(true);

        // Check if SceneCount is equal to the length of the slide array
        if (SceneCount == slide.Length)
        {
            // Add FinalCount method as a listener to nextSlideButton
            nextSlideButton.onClick.AddListener(FinalCount);
        }
    }

    public void FinalCount()
    {
        StartCoroutine(CountPageCoroutine());
    }

    private IEnumerator CountPageCoroutine()
    {
        transit.Play("Transition_Plain");
        yield return new WaitForSeconds(1.35f);
        canvas[0].SetActive(false);
        canvas[1].SetActive(true);
        transit.Play("Transition_Plain_Reverse");
    }
}
