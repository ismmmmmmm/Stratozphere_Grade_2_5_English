using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Lesson_5_slide40_Drop : MonoBehaviour, IDropHandler
{
    Lesson_5_Slide40 _s40;
    Lesson_5_Slide35 _s35;
    [SerializeField] GameObject _s40_GO, _s35_GO;
    [HideInInspector] public GameObject _droppedObj;

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
            StartCoroutine(_s35.CorrectAnswerDragDrop());
        }
    }
}
