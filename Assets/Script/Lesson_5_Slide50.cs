using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lesson_5_Slide50 : Audio_Narration
{
    [SerializeField] Animator hennika;
    [TextArea(3, 10)]
    [SerializeField] string[] sentenceText;
    [SerializeField] TextMeshProUGUI TextTMP;
    [SerializeField] Button zogleberBtn, nextSlideButton;
    Animator _sentence;
    string urlToOpen = "https://www.zogleber.com";
    int _audioIndex, _level, _speedMultiplier = 10;
    bool _isFast;

    void Awake()
    {
        if (TextTMP != null) _sentence = TextTMP.GetComponent<Animator>();
        if (zogleberBtn != null) zogleberBtn.gameObject.SetActive(false);
        if (nextSlideButton != null) nextSlideButton.gameObject.SetActive(false);
    }

    void Start()
    {
        if (nextSlideButton != null) nextSlideButton.onClick.AddListener(StartScene);
        StartCoroutine(InitialSceneSequence_Slide50());
    }

    void StartScene()
    {
        nextButton.SetActive(false);
        StartCoroutine(InitialSceneSequence_Slide50());
        nextSlideButton.onClick.RemoveAllListeners();
        nextSlideButton.onClick.AddListener(LoadScene);
    }
    IEnumerator InitialSceneSequence_Slide50()
    {
        yield return new WaitForSeconds(1f); //initial
        ResetScene();
        SetAudioNarration(_audioIndex);
        hennika.Play("Slide1-4_Speak");

        yield return new WaitForSeconds(clip[_audioIndex].length);
        ActivateSentence();
        hennika.SetBool("isSpeakDone", true);
        _audioIndex++;

        yield return new WaitForSeconds(2f);
        hennika.SetBool("isSpeakDone", false);
        hennika.Play("Slide1-4_Speak");
        SetAudioNarration(_audioIndex);
        if (zogleberBtn != null) zogleberBtn.gameObject.SetActive(true);
        invisibleWall.SetActive(true);

        yield return new WaitForSeconds(clip[_audioIndex].length);
        hennika.SetBool("isSpeakDone", true);
        invisibleWall.SetActive(false);
        _audioIndex++; _level++;
    }

    public void GoToZogleberT()
    {
        StartCoroutine(GoToZogleberEnum());
    }

    IEnumerator GoToZogleberEnum()
    {
        if (nextSlideButton != null) nextSlideButton.gameObject.SetActive(true);
        if (zogleberBtn != null) zogleberBtn.gameObject.SetActive(false);
        zogleberBtn.gameObject.SetActive(false);
        Application.OpenURL(urlToOpen);
        yield return new WaitForSeconds(2f);
    }

    void Update()
    {
        //FastForward();
    }

    void FastForward()
    {
        if (Input.GetKeyUp(KeyCode.Space) && !_isFast)
        {
            Time.timeScale = _speedMultiplier;
            hennika.speed = _speedMultiplier;
            source.pitch = _speedMultiplier;
            _isFast = true;
        }

        else if (Input.GetKeyUp(KeyCode.Space) && _isFast)
        {
            Time.timeScale = 1f;
            hennika.speed = 1f;
            source.pitch = 1f;
            _isFast = false;
        }
    }

    void ActivateSentence()
    {
        if (TextTMP != null && sentenceText[_level] != null) TextTMP.text = sentenceText[_level];
        TextTMP.gameObject.SetActive(true);
    }

    void ResetScene()
    {
        nextSlideButton.gameObject.SetActive(false);
        TextTMP.gameObject.SetActive(false);
        if (TextTMP != null) TextTMP.text = "";
        hennika.SetBool("isSpeakDone", false);
    }
}
