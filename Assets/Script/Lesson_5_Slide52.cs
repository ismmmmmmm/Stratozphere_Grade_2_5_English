using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Lesson_5_Slide52 : Audio_Narration
{
    [SerializeField] string[] sentenceText;

    [SerializeField] TextMeshProUGUI sentenceTMP;
    [SerializeField] GameObject dollyLayoutG;
    [SerializeField] Sprite stableCloseDoor;
    [SerializeField] SpriteRenderer background;
    [SerializeField] Animator mainCam;
    [SerializeField] AnimationClip mainCamAnim;
    [SerializeField] Button[] choicesBtns;

    GameObject buttonLayoutG;
    Button nextSlideBtn;
    int _audioIndex, _level, _currentValue;

    void Start()
    {
        buttonLayoutG = choicesBtns[0].transform.parent.gameObject;
        buttonLayoutG.SetActive(false);
        dollyLayoutG.SetActive(false);
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
        yield return new WaitForSeconds(4);
        sentenceTMP.text = sentenceText[_level];
        sentenceTMP.gameObject.SetActive(true);
        SetAudioNarration(_audioIndex);

        yield return new WaitForSeconds(clip[_audioIndex].length);
        _audioIndex++;
        nextSlideBtn.onClick.AddListener(NextScene);
        nextButton.SetActive(true);
    }

    void NextScene()
    {
        StartCoroutine(NextScene_Sequence());
    }

    IEnumerator NextScene_Sequence() //s53
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(Plain_transition());

        yield return new WaitForSeconds(2);
        ResetScene();
        background.GetComponent<Animator>().enabled = false;
        Vector3 fitScale = new Vector3(.65f, .65f, 1);
        background.transform.localScale = fitScale;
        background.sprite = stableCloseDoor;
        dollyLayoutG.SetActive(true);

        yield return new WaitForSeconds(1);
        mainCam.Play(mainCamAnim.name); //cam zoom in -> go right

        yield return new WaitForSeconds(mainCamAnim.length);
        SetAudioNarration(_audioIndex); //read sentence
        sentenceTMP.horizontalAlignment = HorizontalAlignmentOptions.Center;
        sentenceTMP.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 95);
        SetSentence();

        yield return new WaitForSeconds(clip[_audioIndex].length);
        _audioIndex++;
        buttonLayoutG.SetActive(true); //clickable
    }

    void Sequence()
    {
        StartCoroutine(Sequence_Enum());
    }

    IEnumerator Sequence_Enum() //s54
    {
        yield return new WaitForSeconds(1);
        ResetScene();
        dollyLayoutG.SetActive(true);

        yield return new WaitForSeconds(1);
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
        dollyLayoutG.SetActive(false);
    }
    void CheckAnswer(int index)
    {
        StartCoroutine(CheckAnswer_Enum(index));
    }

    IEnumerator CheckAnswer_Enum(int index)
    {
        GameObject dollySprite;
        string verbAnimName;
        dollySprite = dollyLayoutG.transform.GetChild(index).gameObject;
        verbAnimName = dollySprite.name + "_Clicked";
        dollySprite.GetComponent<Animator>().Play(verbAnimName);
        invisibleWall.SetActive(true);

        yield return new WaitForSeconds(2);
        nextSlideBtn.gameObject.SetActive(true);
        nextSlideBtn.onClick.RemoveAllListeners();
        nextSlideBtn.onClick.AddListener(Sequence);
    }

    void SetSentence()
    {
        sentenceTMP.text = sentenceText[_level];
        sentenceTMP.gameObject.SetActive(true);
    }
}
