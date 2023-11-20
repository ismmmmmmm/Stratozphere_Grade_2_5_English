using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Lesson_5_Slide60 : Audio_Narration
{
    [SerializeField] string[] sentenceText;

    [SerializeField] TextMeshProUGUI sentenceTMP;
    [SerializeField] GameObject threeLayoutG, slide63;
    [SerializeField] Animator mainCam;
    [SerializeField] AnimationClip mainCamAnim;
    [SerializeField] Button[] choicesBtns;

    delegate void nextSceneDelegate();
    nextSceneDelegate nextFunc;
    GameObject buttonLayoutG, threeSprite;
    Button nextSlideBtn;
    int _audioIndex, _level;

    void Start()
    {
        nextFunc = NextScene;
        buttonLayoutG = choicesBtns[0].transform.parent.gameObject;
        buttonLayoutG.SetActive(false);
        nextSlideBtn = nextButton.GetComponent<Button>();
        sentenceTMP.gameObject.SetActive(false);
        SetButtonsValue();

        StartCoroutine(InitialSequence());
    }

    void SetButtonsValue()
    {
        for (int i = 0; i < choicesBtns.Length; i++)
        {
            int index = i;
            choicesBtns[i].onClick.AddListener(() => CheckAnswer(index));
        }
    }

    IEnumerator InitialSequence()
    {
        yield return new WaitForSeconds(3);
        StartCoroutine(NextScene_Sequence());
    }

    void NextScene()
    {
        ResetScene();
        if (_level > sentenceText.Length - 1) StartCoroutine(LoadSlide63()); 
        else StartCoroutine(NextScene_Sequence());
    }

    IEnumerator NextScene_Sequence() //s60
    {
        yield return new WaitForSeconds(1);
        SetAudioNarration(_audioIndex);
        
        yield return new WaitForSeconds(clip[_audioIndex].length);
        _audioIndex++;
        mainCam.Play(mainCamAnim.name); //cam zoom in -> go right

        yield return new WaitForSeconds(mainCamAnim.length);
        SetAudioNarration(_audioIndex); //read sentence
        SetSentence();

        yield return new WaitForSeconds(clip[_audioIndex].length);
        _audioIndex++;
        buttonLayoutG.SetActive(true); //clickable
    }

    void ResetScene()
    {
        _level++;
        nextButton.SetActive(false);
        sentenceTMP.gameObject.SetActive(false);
        sentenceTMP.text = "";
        invisibleWall.SetActive(false);
        buttonLayoutG.SetActive(false);

        threeSprite.GetComponent<Animator>().Play("New State");
    }

    void CheckAnswer(int index)
    {
        StartCoroutine(CheckAnswer_Enum(index));
    }

    IEnumerator CheckAnswer_Enum(int index)
    {
        string verbAnimName;
        threeSprite = threeLayoutG.transform.GetChild(index).gameObject;
        verbAnimName = threeSprite.name + "_Clicked";
        threeSprite.GetComponent<Animator>().Play(verbAnimName);
        invisibleWall.SetActive(true);

        yield return new WaitForSeconds(2);
        nextSlideBtn.gameObject.SetActive(true);
        nextSlideBtn.onClick.RemoveAllListeners();
        nextSlideBtn.onClick.AddListener(() => nextFunc());
    }

    void SetSentence()
    {
        sentenceTMP.text = sentenceText[_level];
        sentenceTMP.gameObject.SetActive(true);
    }

    IEnumerator LoadSlide63()
    {
        StartCoroutine(Plain_transition());

        yield return new WaitForSeconds(2);
        slide63.SetActive(true);
        gameObject.SetActive(false);
    }
}
