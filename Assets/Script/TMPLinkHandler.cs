using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TMP_Text))]
public class TMPLinkHandler : MonoBehaviour, IPointerClickHandler
{
    TMP_Text _tmpTextBox;
    Canvas _canvasToCheck;
    [SerializeField] Camera cameraToUse;

    //public delegate void ClickOnLinkEvent(string keyword);
    public delegate void ClickOnLinkEvent();
    public static ClickOnLinkEvent OnClickedOnLinkEvent;

    void Awake()
    {
        _tmpTextBox = GetComponent<TMP_Text>();
        _canvasToCheck = GetComponentInParent<Canvas>();

        if (_canvasToCheck.renderMode == RenderMode.ScreenSpaceOverlay)
            cameraToUse = null;
        else
            cameraToUse = _canvasToCheck.worldCamera;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        Vector3 mousePos = new(eventData.position.x, eventData.position.y, 0);
        var linkTaggedText = TMP_TextUtilities.FindIntersectingLink(_tmpTextBox, mousePos, cameraToUse);

        if (linkTaggedText == -1) return;

        TMP_LinkInfo linkInfo = _tmpTextBox.textInfo.linkInfo[linkTaggedText];

        string linkID = linkInfo.GetLinkID();
        if (linkID.Contains("www"))
        {
            //Application.OpenURL(linkID);
            return;
        }
        //OnClickedOnLinkEvent?.Invoke(linkInfo.GetLinkText());
        OnClickedOnLinkEvent?.Invoke();
        //OnClickedOnLinkEvent();
    }
}
