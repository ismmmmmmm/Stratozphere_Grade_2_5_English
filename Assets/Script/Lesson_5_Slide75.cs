using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//S75-78
public class Lesson_5_Slide75 : Audio_Narration
{
    [SerializeField] GameObject[] slideTexts;
    [SerializeField] GameObject page1, page2, nextPageBtn, slide78, bag;
    int _level;
    bool _isClicked;
    Button nextSlideBtn;

    void Start()
    {
        slideTexts[0].SetActive(false); nextPageBtn.SetActive(false);
        TMPLinkHandler.OnClickedOnLinkEvent = null;
        TMPLinkHandler.OnClickedOnLinkEvent += LinkClickHandler;
        nextSlideBtn = nextButton.GetComponent<Button>();
        //nextSlideBtn.onClick.AddListener(() => { StartCoroutine(LoadPage()); });
        nextSlideBtn.onClick.AddListener(LoadPage);
        // StartCoroutine(InitialScene());
        StartCoroutine(FinalScene());
    }

    IEnumerator InitialScene()
    {
        yield return new WaitForSeconds(3.5f);
        slideTexts[0].SetActive(true); nextPageBtn.SetActive(true);
        //LoadPage();
    }

    void LoadPage()
    {
        StartCoroutine(LoadPageEnum());
    }

    IEnumerator LoadPageEnum() //S76-77
    {
        _level++;
        nextButton.SetActive(false);

        if (_level <= slideTexts.Length-1)
        {
            nextPageBtn.SetActive(slideTexts[_level].transform.childCount > 0);
            slideTexts[_level].SetActive(true);
            slideTexts[_level-1].SetActive(false);

            yield return new WaitForSeconds(2);
            nextButton.SetActive(true);
        }

        else
        {
            StartCoroutine(FinalScene());
            //nextSlideBtn.onClick.RemoveAllListeners();
            //nextSlideBtn.onClick.AddListener(() => { StartCoroutine(FinalScene()); });
        }
    }

    void LinkClickHandler()
    {
        nextButton.SetActive(true);
    }

    public void NextPage()
    {
        _isClicked = !_isClicked;
        page2.SetActive(_isClicked);
        page1.SetActive(!_isClicked);   
    }

    IEnumerator FinalScene() //s78
    {
        StartCoroutine(Plain_transition());

        yield return new WaitForSeconds(2);
        bag.SetActive(false);
        slide78.SetActive(true);
        transform.Find("Canvas").gameObject.SetActive(false);

        yield return new WaitForSeconds(1);
        SetAudioNarration(0);
        float waitSec1 = 6; float waitSec2 = 11;

        yield return new WaitForSeconds(waitSec1);
        bag.SetActive(true);

        yield return new WaitForSeconds(waitSec2);
        bag.GetComponent<Animator>().Play("open");

        yield return new WaitForSeconds(clip[0].length - (waitSec1+waitSec2));
        nextSlideBtn.onClick.RemoveAllListeners();
        nextSlideBtn.onClick.AddListener(() => { nextButton.SetActive(false); RestartScene(0); });
        nextButton.SetActive(true);
    }
}
