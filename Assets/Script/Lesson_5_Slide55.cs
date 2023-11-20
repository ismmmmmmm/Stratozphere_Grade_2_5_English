using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Lesson_5_Slide55 : Audio_Narration
{
    int _audioIndex, _level;
    [SerializeField] GameObject sentenceText1, sentenceText2, ABC, glisten, button, slide57;
    [SerializeField] Animator A, B, C;

    float ABy;
    HorizontalLayoutGroup abcHL;

    void Start()
    {
        abcHL = glisten.GetComponent<HorizontalLayoutGroup>();
        abcHL.spacing = -.25f;
        ABy = A.GetComponent<RectTransform>().anchoredPosition.y;
        ABC.transform.position = new Vector3 (34.6f, -39.2f, 0.0f);
        foreach (var obj in new[] { button, glisten, sentenceText1, sentenceText2, ABC, button}) obj.SetActive(false);
        StartCoroutine(StartScene());
    }

    IEnumerator StartScene()
    {
        yield return new WaitForSeconds(2);
        SetAudioNarration(_audioIndex);

        yield return new WaitForSeconds(clip[_audioIndex].length);
        _audioIndex++;
        sentenceText1.SetActive(true);

        yield return new WaitForSeconds(2);
        ABC.SetActive(true);
        ABC.GetComponent<HorizontalLayoutGroup>().enabled = false;

        yield return new WaitForSeconds(2); //form CAB
        B.Play("CAB_B");

        yield return new WaitForSeconds(.5f);
        C.Play("CAB_C");

        yield return new WaitForSeconds(.3f);
        A.Play("CAB_A");

        yield return new WaitForSeconds(1.5f);
        SetAudioNarration(_audioIndex);

        yield return new WaitForSeconds(clip[_audioIndex].length);
        _audioIndex++;
        glisten.SetActive(true);
        button.SetActive(true);
    }


    public void OnClickCheckLevel()
    {
        button.SetActive(false);

        if (_level < 1)
        {
            StartCoroutine(NextScene());
        }

        else
        {
            StartCoroutine(GoTransition());
        }
    }

    IEnumerator NextScene() //s56
    {
        yield return new WaitForSeconds(1);
        ResetScene();

        yield return new WaitForSeconds(1);
        CompressABC();
        sentenceText2.SetActive(true);
        SetAudioNarration(_audioIndex);

        yield return new WaitForSeconds(clip[_audioIndex].length);
        _audioIndex++;
        glisten.SetActive(true);
        button.SetActive(true);
        _level++;
    }

    void CompressABC() //compress A,B,C space
    {
        Vector3 ABCpos = ABC.transform.position;
        ABC.transform.position = new Vector3(31.6f, ABCpos.y+1.2f, ABCpos.z);

        float minus = 1.2f;
        foreach (var obj in new[] { A, B })
        {
            var ABpos = obj.GetComponent<RectTransform>().anchoredPosition;

            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(ABpos.x - minus, ABy);
            minus += 1.7f;
        }

        abcHL.spacing = -3.25f;
        ABC.SetActive(true);
    }

    void ResetScene()
    {
        foreach (var obj in new[] { button, glisten, sentenceText1, sentenceText2, ABC, button }) 
            obj.SetActive(false);
    }

    IEnumerator GoTransition()
    {
        StartCoroutine(Plain_transition());

        yield return new WaitForSeconds(2);
        slide57.SetActive(true);
        gameObject.SetActive(false);
    }
}
