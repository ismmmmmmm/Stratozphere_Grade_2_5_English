using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Lesson_5_Slide40 : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject[] _choices;
    
    [HideInInspector] public GameObject _objectBeingDragged;
    [HideInInspector] public int _draggedIndex;
    [HideInInspector] public bool _isCorrect;
    GameObject[] choicesBtn;
    [HideInInspector] public RectTransform _objectRectTransform;
    Vector2 _startPosition, _startOffset;
    Lesson_5_Slide35 _s35;
    [SerializeField] GameObject _Slide_35;
    Image _image, _sentenceImage;
    
    void Awake()
    {
        
        _s35 = _Slide_35.GetComponent<Lesson_5_Slide35>();
        _sentenceImage = transform.GetChild(0).Find("Sentence").gameObject.GetComponent<Image>();
        choicesBtn = _s35.choicesBtn;
        
    }
    void Start()
    {
       // _s35 = _Slide_35.GetComponent<Lesson_5_Slide35>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _objectBeingDragged = GetDraggableObject(eventData);
        if (_objectBeingDragged != null)
        {
            _draggedIndex = Array.IndexOf(choicesBtn, _objectBeingDragged);
            _image = _objectBeingDragged.GetComponent<Image>();
            _image.raycastTarget = false;
            _objectRectTransform = _objectBeingDragged.GetComponent<RectTransform>();
            _startPosition = _objectRectTransform.position;
            _startOffset = _startPosition - eventData.position;
        }
        _isCorrect = _s35.CheckAnswerDragDrop();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_objectBeingDragged != null)
        {
            _objectRectTransform.position = eventData.position + _startOffset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        bool isDropped = IsObjectDropped(eventData);
        _image.raycastTarget = true;
        if (_objectBeingDragged != null)
        {
            if (_isCorrect == true && isDropped == true)
            {
                _objectBeingDragged = null;
            }
            else _objectRectTransform.position = _startPosition;
        }
    }

    public bool IsObjectDropped(PointerEventData eventData)
    {
        if (eventData.pointerEnter != null)
        {
            GameObject droppedObj = eventData.pointerEnter;
            Image droppedImg = droppedObj.GetComponent<Image>();
            if (droppedImg != null && droppedImg == _sentenceImage)
            {
                return true;
            }
            else return false;
        }
        return false;
    }

    private GameObject GetDraggableObject(PointerEventData eventData)
    {
        foreach (GameObject obj in _choices)
        {
            RectTransform rectTransform = obj.GetComponent<RectTransform>();
            if (rectTransform != null && RectTransformUtility.RectangleContainsScreenPoint(rectTransform, eventData.position))
            {
                return obj;
            }
        }
        return null;
    }
}
