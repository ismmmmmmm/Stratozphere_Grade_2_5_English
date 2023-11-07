using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slide_59 : Audio_Narration
{
    // Start is called before the first frame update
    public Animator donald, hennika;
    public GameObject[] corn, canvas;
    public GameObject Drop, frame;
    public GameObject[] parentCorn;
    public AudioSource[] buttonAudio;
    public AudioClip[] cornAudio;
    public Image[] cornImage;
    public Sprite[] cornSprite;
    public string[] text;
    public int slideNumber = 0;
    public int audioCount = 4;
    public Slide_59_Drop dropScript;
    public TextMeshProUGUI textMeshProUGUI;
    private int spriteCount;
    public Button nextSlideButton;
    void Start()
    {
        nextSlideButton.onClick.AddListener(NextSlide);
        StartCoroutine(GoPlay());

    }

    private IEnumerator GoPlay()
    {
        for (int i = 0; i < 60; i++)
        {
            buttonAudio[i].clip = cornAudio[i];
        }

        Debug.Log("GoPlay");
        for (int i = 0; i < 60; i++)
        {
            cornImage[spriteCount].sprite = cornSprite[spriteCount]; spriteCount++;
        }
        SetAudioNarration(0);
        hennika.Play("Hennika_Speaking");
        yield return new WaitForSeconds(clip[0].length);
        hennika.Play("Hennika_Idle");
        yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(1);
        donald.Play("Slide_59");
        yield return new WaitForSeconds(9.3f);
        canvas[slideNumber].SetActive(true);
        SetAudioNarration(1);
        hennika.Play("Hennika_Speaking");
        yield return new WaitForSeconds(clip[1].length);
        hennika.Play("Hennika_Idle");
        yield return new WaitForSeconds(1);
        SetAudioNarration(2);
        hennika.Play("Hennika_Speaking");
        yield return new WaitForSeconds(clip[2].length);
        hennika.Play("Hennika_Idle");
        yield return new WaitForSeconds(1);
        SetAudioNarration(3);
        hennika.Play("Hennika_Speaking");
        yield return new WaitForSeconds(clip[3].length);
        hennika.Play("Hennika_Idle");
        yield return new WaitForSeconds(1);
        invisibleWall.SetActive(false);
        frame.SetActive(true);
        Drop.SetActive(true);
    }
    public void NextSlide()

    {

        slideNumber++;
        Debug.Log("slidenumber: " + slideNumber);
        dropScript.DestroyChildObjects();

        frame.SetActive(false);
        Drop.SetActive(false);
        canvas[slideNumber - 1].SetActive(false);
        Debug.Log("NextSlide");
        StartCoroutine(StartScene());
    }

    private IEnumerator StartScene()
    {
        textMeshProUGUI.text = text[slideNumber];
        Debug.Log("StartScene");
        invisibleWall.SetActive(true);
        nextButton.SetActive(false);
        frame.SetActive(false);
        donald.Play("Slide_59");
        yield return new WaitForSeconds(9.3f);
        canvas[slideNumber].SetActive(true);
        hennika.Play("Hennika_Speaking");
        yield return new WaitForSeconds(clip[audioCount].length);
        SetAudioNarration(audioCount); audioCount++;
        hennika.Play("Hennika_Idle");
        yield return new WaitForSeconds(1);
        invisibleWall.SetActive(false);
        frame.SetActive(true);
        Drop.SetActive(true);
        slideNumber++;
        slideChecker();
    }
    public void slideChecker()
    {
        if (slideNumber > canvas.Length - 2)
        {
            nextSlideButton.onClick.RemoveAllListeners();
            nextSlideButton.onClick.AddListener(LoadScene);
        }
    }
    public void Correct()
    {
        Debug.Log("true");
        StartCoroutine(CorrectAnswer());
    }
    private IEnumerator CorrectAnswer()
    {
        //SetAudioNarration(1); 
        yield return new WaitForSeconds(clip[0].length + 2);
        nextButton.SetActive(true);
    }
    public void Wrong()
    {
        Debug.Log("false");

        StartCoroutine(WrongAnswer());
    }
    private IEnumerator WrongAnswer()
    {
        //SetAudioNarration(0);
        yield return new WaitForSeconds(clip[1].length);
    }
    private void ResetDragObjectsParents()
    {
        Slide_59_Drag[] dragObjects = FindObjectsOfType<Slide_59_Drag>();
        foreach (Slide_59_Drag dragObject in dragObjects)
        {
            dragObject.ResetParentToOriginal();
        }
    }

}