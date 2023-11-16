using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Lesson_5_Slide57 : Audio_Narration
{
    int _audioIndex, _index, _level;
    [SerializeField] GameObject sentenceTMP, link;
    [SerializeField] GameObject[] panels, backgrounds;
    Button nextSlideBtn;

    void Start()
    {
        foreach (GameObject go in panels) { go.SetActive(true); }
        nextSlideBtn = nextButton.GetComponent<Button>();
        sentenceTMP.SetActive(false);
        link.SetActive(false);

        foreach (GameObject go in backgrounds)
        {
            go.transform.GetChild(0).gameObject.SetActive(false);
        }

        backgrounds[0].transform.GetChild(0).gameObject.SetActive(true); //re-enable s57 canvas 
        nextSlideBtn.onClick.AddListener(NextScene);
        StartCoroutine(StartScene());
    }

    IEnumerator StartScene()
    {
        yield return new WaitForSeconds(2);
        SetAudioNarration(_audioIndex);

        yield return new WaitForSeconds(clip[_audioIndex].length);
        _audioIndex++;
        sentenceTMP.SetActive(true);
        link.SetActive(true);
        _level++;
    }

    public void OnClickTMP()
    {
        foreach (GameObject go in panels) { go.SetActive(false); }
        nextButton.SetActive(true);
    }

    void NextScene()
    {
        nextButton.SetActive(false);
        if (_level > backgrounds.Length - 1) LoadScene(); //next slide 
        else StartCoroutine(NextSceneEnum());
    }

    IEnumerator NextSceneEnum()
    {
        yield return new WaitForSeconds(1);
        foreach (GameObject go in panels) { go.SetActive(true); } //set all links clickable
        backgrounds[_index].SetActive(false);
        _index++;
        backgrounds[_index].SetActive(true);
        SetAudioNarration(_audioIndex);

        yield return new WaitForSeconds(clip[_audioIndex].length);
        backgrounds[_index].transform.GetChild(0).gameObject.SetActive(true); //texts
        _audioIndex++; _level++;
    }
}
