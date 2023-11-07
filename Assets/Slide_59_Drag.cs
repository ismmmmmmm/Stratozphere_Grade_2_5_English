using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class Slide_59_Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform initialParent; // Stores the initial parent of the drag object.
    public Image image; // Reference to the Image component attached to this GameObject.
    public CanvasGroup canvas; // Reference to the CanvasGroup component attached to this GameObject.
    public Vector3 initialPosition; // Stores the initial position of the drag object.
    private Transform originalParent; // Stores the original parent before dragging.

    public int answer; // An integer variable, possibly representing an answer or identifier.

    // This method is called when the script is initialized.
    private void Start()
    {
        initialPosition = transform.position; // Store the initial position.
        initialParent = transform.parent; // Store the initial parent.
        initialPosition = transform.position; // This appears to be redundant.
        canvas = GetComponent<CanvasGroup>(); // Get the CanvasGroup component.
        image = GetComponent<Image>(); // Get the Image component.
    }

    // This method is called when the user begins dragging the UI element.
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent; // Store the original parent of the UI element.
        transform.SetParent(transform.root); // Set the UI element's parent to the root of the hierarchy.
        transform.SetAsLastSibling(); // Ensure the UI element is drawn on top of other elements.
        image.raycastTarget = false; // Disable raycasting for the Image component.
    }

    // This method is called while the user is dragging the UI element.
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition; // Update the UI element's position to follow the mouse.
    }

    // This method is called when the user stops dragging the UI element.
    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true; // Re-enable raycasting for the Image component.
        if (transform.parent == initialParent)
        {
            transform.position = initialPosition; // Return the UI element to its initial position if not dropped in a drop zone.
            Debug.Log("true"); // Log a message indicating that the UI element returned to the initial position.
        }
        transform.position = initialPosition; // Reset the UI element's position to the initial position.
    }

    // Reset the UI element's position to the initial position.
    public void ResetPosition()
    {
        transform.position = initialPosition;
    }

    // Reset the UI element's parent to its original parent.
    public void ResetParentToOriginal()
    {
        transform.SetParent(originalParent);
    }
}