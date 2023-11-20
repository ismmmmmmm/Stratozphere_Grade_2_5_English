using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Lesson_5_Slide2 : Audio_Narration
{
    Slide_20_Draw s20Draw;
    [SerializeField] Animator hennikaS2, hennikaS3;
    Animator hennika;
    TextMeshProUGUI text1A;
    [SerializeField] GameObject drawingS2, drawingS3;
    typewriterUI typewriterUI;
    GameObject text1B, glisten, drawing;
    string text1text;
    int _audioIndex;
    float wait1 = 0f, wait2 = 7.5f, wait3 = 10.5f, startWait = 3.5f;
    Button saveBtn;

    void Start()
    {
        s20Draw = drawingS2.transform.Find("Hennika").GetComponent<Slide_20_Draw>();
        drawing = drawingS2;
        hennika = hennikaS2;
        text1A = hennikaS2.transform.Find("Sentence1A").GetComponent<TextMeshProUGUI>();
        text1B = hennikaS2.transform.Find("Sentence1B").gameObject;
        text1text = text1A.text;
        text1A.text = string.Empty;
        glisten = text1A.transform.GetChild(0).gameObject;
        glisten.SetActive(false); text1B.SetActive(false);
        text1A.gameObject.SetActive(true); drawing.SetActive(false);
        typewriterUI = text1A.GetComponent<typewriterUI>();

        saveBtn = drawing.transform.Find("Hennika").gameObject.transform.Find("Buttons").transform.Find("Save").GetComponent<Button>();
        saveBtn.onClick.AddListener(Slide3);

        StartCoroutine(StartScene());
    }

    IEnumerator StartScene()
    {
        yield return new WaitForSeconds(startWait);
        SetAudioNarration(_audioIndex);
        hennika.Play("Slide1-4_Speak_Image");

        yield return new WaitForSeconds(wait1);
        text1A.text = text1text;
        StartCoroutine(typewriterUI.TypeWriterTMP());

        yield return new WaitForSeconds(wait2);
        text1B.SetActive(true); glisten.SetActive(true);

        yield return new WaitForSeconds(wait3);
        drawing.SetActive(true);
        invisibleWall.SetActive(true);
        hennika.gameObject.SetActive(false);

        hennika = drawing.transform.Find("Hennika").GetComponent<Animator>();
        hennika.Play("Drawing_Speak");
        yield return new WaitForSeconds(clip[_audioIndex].length - (wait1 + wait2 + wait3));
        hennika.SetBool("isSpeakDone", true);
        invisibleWall.SetActive(false);
        _audioIndex++;
    }

    public void Slide3()
    {
        Slide3Initial();
    }

    void Slide3Initial()
    {
        s20Draw.ClearLines();

        drawing.SetActive(false);
        wait2 = 8.5f; wait3 = 4.5f; startWait = 1f;
        drawing = drawingS3;
        s20Draw = drawingS3.transform.Find("Hennika").GetComponent<Slide_20_Draw>();
        hennika = hennikaS3;
        text1A = hennikaS3.transform.Find("Sentence1A").GetComponent<TextMeshProUGUI>();
        text1B = hennikaS3.transform.Find("Sentence1B").gameObject;
        text1text = text1A.text;
        text1A.text = string.Empty;
        glisten = text1A.transform.GetChild(0).gameObject;
        glisten.SetActive(false); text1B.SetActive(false);
        text1A.gameObject.SetActive(true); drawing.SetActive(false);
        typewriterUI = text1A.GetComponent<typewriterUI>();

        saveBtn = drawing.transform.Find("Hennika").gameObject.transform.Find("Buttons").transform.Find("Save").GetComponent<Button>();
        saveBtn.onClick.RemoveAllListeners();
        saveBtn.onClick.AddListener(() => { text1A.gameObject.SetActive(false); LoadScene(); });

        hennika.gameObject.SetActive(true);

        StartCoroutine(StartScene());
    }
}
