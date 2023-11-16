using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Lesson_5_Slide1 : Audio_Narration
{
    [SerializeField] Animator mainCam, hennika;
    [SerializeField] AnimationClip camZoomIn, camZoomOut;
    [SerializeField] GameObject text1B, text2, glisten;
    [SerializeField] TextMeshProUGUI text1A;
    typewriterUI typewriterUI;
    string text1Atext;

    void Start()
    {
        text1Atext = text1A.text;
        text1A.text = "";
        typewriterUI = text1A.GetComponent<typewriterUI>();
        text1B.SetActive(false); text2.SetActive(false); glisten.SetActive(false);
        
        StartCoroutine(StartScene());
    }

    IEnumerator StartScene()
    {
        float wait1 = 5f, wait2 = 6.5f, wait3 = 14.5f;
        yield return new WaitForSeconds(3.5f);
        mainCam.Play(camZoomIn.name);

        yield return new WaitForSeconds(camZoomIn.length); //zoom in
        SetAudioNarration(0);
        hennika.Play("Slide1-4_Speak");

        yield return new WaitForSeconds(wait1); //audio 1
        text1A.text = text1Atext;
        StartCoroutine(typewriterUI.TypeWriterTMP());

        yield return new WaitForSeconds(wait2);
        text1B.SetActive(true); glisten.SetActive(true);

        yield return new WaitForSeconds(wait3);
        text1A.gameObject.SetActive(false); text1B.SetActive(false);
        text2.SetActive(true);
        invisibleWall.SetActive(true);

        yield return new WaitForSeconds(clip[0].length - (wait1 + wait2 + wait3)); //audio 3
        invisibleWall.SetActive(false);
        hennika.SetBool("isSpeakDone", true);
    }
}
