using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//s73-s74
public class Lesson_5_Slide73 : Audio_Narration
{
    [SerializeField] GameObject slide74, s73BG;
    int _audioIndex;
    Button nextSlideBtn;

    void Start()
    {
        s73BG.SetActive(true);
        slide74.SetActive(false);
        TMPLinkHandler.OnClickedOnLinkEvent = null;
        TMPLinkHandler.OnClickedOnLinkEvent = LinkOnClick;

        nextSlideBtn = nextButton.GetComponent<Button>();
        nextSlideBtn.onClick.AddListener(() => { StartCoroutine(LoadSlide74()); });
        StartCoroutine(StartScene());
    }

    IEnumerator StartScene()
    {
        yield return new WaitForSeconds(1);
        SetAudioNarration(_audioIndex);

        yield return new WaitForSeconds(clip[_audioIndex].length);
        _audioIndex++;
        nextButton.SetActive(true);
    }

    IEnumerator LoadSlide74()
    {
        nextButton.SetActive(false);

        yield return new WaitForSeconds(1);
        slide74.SetActive(true);
        s73BG.SetActive(false);
    }

    void LinkOnClick()
    {
        nextButton.SetActive(true);
        nextSlideBtn.onClick.AddListener(LoadScene);
    }
}
