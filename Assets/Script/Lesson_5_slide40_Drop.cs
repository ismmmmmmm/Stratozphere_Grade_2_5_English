using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Lesson_5_slide40_Drop : MonoBehaviour, IDropHandler
{
    Lesson_5_Slide40 _s40;
    Lesson_5_Slide35 _s35;
    [SerializeField] GameObject _s40_GO, _s35_GO;
    [SerializeField] Animator hennika;
    int[] _singularPluralAudio = { 12, 11, 11, 12, 11 };
    [HideInInspector] public GameObject _droppedObj;

    int _level = 0;
    bool isS40AnimPlaying;

    void Awake()
    {
        _s35 = _s35_GO.GetComponent<Lesson_5_Slide35>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        _s40 = _s40_GO.GetComponent<Lesson_5_Slide40>();
        _droppedObj = _s40._objectBeingDragged;

        bool isCorrect = _s40._isCorrect;
        if (isCorrect == true)
        {

            Debug.Log("Wrong audio: " + _singularPluralAudio[_level]);
            StartCoroutine(_s35.CorrectAnswerDragDrop());
            _level++;
        }
        else
        {
            Debug.Log("Wrong");
            StartCoroutine(WrongAnswer());
        }
    }

    IEnumerator WrongAnswer()
    {
        _s35.invisibleWall.SetActive(true);
        hennika.SetBool("isSpeakDone", false);

        Debug.Log("Wrong audio: "+_singularPluralAudio[_level]);
        _s35.SetAudioNarration(_singularPluralAudio[_level]);


        if (hennika != null) hennika.SetTrigger("s40_anim");
        yield return new WaitForSeconds(_s35.clip[11].length);
        hennika.SetBool("isSpeakDone", true);
        _s35.invisibleWall.SetActive(false);
    }
}
