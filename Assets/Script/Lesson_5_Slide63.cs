using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class Lesson_5_Slide63 : Audio_Narration
{
    [SerializeField] Animator hennika;
    [SerializeField] GameObject s64_canvas, teacherBtn;
    GameObject s64Text, s65Text, page1, page2;
    Button nextSlideBtn;

    int _audioIndex, _level;
    bool _isClicked;

    void Start()
    {
        teacherBtn.SetActive(false);
        s64Text = s64_canvas.transform.GetChild(0).gameObject;
        s65Text = s64_canvas.transform.GetChild(1).gameObject;
        page1 = s65Text.transform.GetChild(0).gameObject;
        page2 = s65Text.transform.GetChild(1).gameObject;

        hennika.gameObject.SetActive(true);
        s64_canvas.SetActive(false);
        page1.SetActive(true); page2.SetActive(false);
        s64Text.SetActive(true); s65Text.SetActive(false);
        
        nextSlideBtn = nextButton.GetComponent<Button>();
        nextSlideBtn.onClick.RemoveAllListeners();
        nextSlideBtn.onClick.AddListener(NextScene);
        StartCoroutine(StartScene());
        TMPLinkHandler.OnClickedOnLinkEvent = null;
        TMPLinkHandler.OnClickedOnLinkEvent += LinkClickHandler;
    }

    IEnumerator StartScene()
    {
        yield return new WaitForSeconds(1f);
        SetAudioNarration(_audioIndex);
        hennika.Play("Hennika");

        yield return new WaitForSeconds(clip[_audioIndex].length);
        _audioIndex++;
        nextButton.SetActive(true);
    }

    void NextScene()
    {
        // CheckLevel();
        StartCoroutine(S64());
    }


    IEnumerator S64()
    {
        nextButton.SetActive(false);
        invisibleWall.SetActive(true);

        yield return new WaitForSeconds(1);
        invisibleWall.SetActive(false);
        hennika.gameObject.SetActive(false);
        s64_canvas.SetActive(true);
        if (_level > 0)
        {
            if (_level == 2) LoadScene(); //load s66
            else s65Text.SetActive(true); s64Text.SetActive(false); //s65
        }
        _level++;
    }

    void LinkClickHandler()
    {
        Debug.Log("Slide 63");
        nextButton.SetActive(true);
    }

    public void NextPage()
    {
        _isClicked = !_isClicked;
        page2.SetActive(_isClicked);
        page1.SetActive(!_isClicked);
    }
}
