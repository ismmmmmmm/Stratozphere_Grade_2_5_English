using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//S66 - S74
public class Lesson_5_slide66 : Audio_Narration
{
    int _audioIndex, _level, _textIndex = 0, _speedMultiplier = 10;
    float _glistenWait = 15f, _startWait = 3.5f;
    bool _isFast;
    [SerializeField] GameObject _glisten, _slide66, _slide68, _slide71;
    [SerializeField] GameObject[] _text;
    public Animator animator;
    Button[] _buttons;
    Button nextSlideBtn;

    void Start()
    {
        TMPLinkHandler.OnClickedOnLinkEvent = null;
        TMPLinkHandler.OnClickedOnLinkEvent = LinkOnClick;
        _slide68.transform.Find("Canvas").gameObject.SetActive(true);
        nextSlideBtn = nextButton.GetComponent<Button>();
        _buttons = _glisten.GetComponentsInChildren<Button>();
        foreach (Button button in _buttons) { button.onClick.AddListener(NextScene); }
        _glisten.SetActive(false);
        StartCoroutine(StartScene());
    }

    void Update()
    {
        FastForward();
    }
    void FastForward()
    {
        if (Input.GetKeyUp(KeyCode.Space) && !_isFast)
        {
            Time.timeScale = _speedMultiplier;
            animator.speed = _speedMultiplier;
            source.pitch = _speedMultiplier;
            _isFast = true;
        }

        else if (Input.GetKeyUp(KeyCode.Space) && _isFast)
        {
            Time.timeScale = 1f;
            animator.speed = 1f;
            source.pitch = 1f;
            _isFast = false;
        }
    }

    IEnumerator StartScene()
    {
        _level++; _glisten.SetActive(false);
        yield return new WaitForSeconds(_startWait);
        SetAudioNarration(_audioIndex);
        //invisibleWall.SetActive(true);


        yield return new WaitForSeconds(_glistenWait);
        _glisten.SetActive(true);

        yield return new WaitForSeconds(clip[_audioIndex].length - _glistenWait);
        _audioIndex++; _glistenWait = 11; _startWait = 1;
        //invisibleWall.SetActive(false);
    }

    void NextScene()
    {
        if (_level > 1) //slide68
            StartCoroutine(Slide68Initial());
        else StartCoroutine(StartScene());
    }

    IEnumerator Slide68Initial()
    {
        _glisten.SetActive(false);
        StartCoroutine(Plain_transition());

        yield return new WaitForSeconds(2);
        _slide66.SetActive(false);
        _slide68.SetActive(true);
        StartCoroutine(Slide68Enum());
    }

    IEnumerator Slide68Enum()
    {
        SetAudioNarration(_audioIndex);
        invisibleWall.SetActive(true);

        yield return new WaitForSeconds(clip[_audioIndex].length);
        invisibleWall.SetActive(false);
        _text[_textIndex].SetActive(true); Debug.Log(_textIndex);
        _audioIndex++; _textIndex++;
    }

    void LinkOnClick()
    {
        nextSlideBtn.onClick.RemoveAllListeners();
        if (_textIndex > 1)
            nextSlideBtn.onClick.AddListener(() => { StartCoroutine(Slide71Transition()); });
        
        else
            nextSlideBtn.onClick.AddListener(() => { StartCoroutine(Slide69()); });
        
        nextButton.SetActive(true);
    }

    IEnumerator Slide69() //s69-70
    {
        nextButton.SetActive(false);
        invisibleWall.SetActive(true);
        yield return new WaitForSeconds(1);
        _text[_textIndex - 1].SetActive(false);
        _text[_textIndex].SetActive(true);
        SetAudioNarration(_audioIndex);

        yield return new WaitForSeconds(clip[_audioIndex].length);
        invisibleWall.SetActive(false); Debug.Log(_textIndex);
        _audioIndex++; _textIndex++; 
    }

    IEnumerator Slide71Transition()
    {
        nextButton.SetActive(false);
        StartCoroutine(Plain_transition());

        yield return new WaitForSeconds(2);
        _slide71.SetActive(true); _slide68.SetActive(false);
    }
}
