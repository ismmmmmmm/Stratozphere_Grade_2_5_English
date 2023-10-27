using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class DragItem_Script : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform topOfObject;
    public Image image;
    public CanvasGroup canvas;
    public Vector3 initialPosition;
    public int SlideCount = 0;
    public int answer;

    private void Start()
    {
        topOfObject = transform.parent;
        initialPosition = transform.position;
        canvas = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(topOfObject);
        transform.SetAsLastSibling();
        image.raycastTarget = false;


    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(topOfObject);
        transform.position = topOfObject.position;
    }
}