using UnityEngine;
using UnityEngine.EventSystems;

public class Slide_59_Drop : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private int[] _correctAnswer; // An array to store correct answers for each slide.
    [SerializeField]
    private int _boolValue; // A boolean value (integer) that may have specific meaning in your code.
    public Slide_59 slide59; // Reference to the Slide_59 script.

    // This method is called when a UI element is dropped onto this GameObject.
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag; // Get the GameObject being dropped.
        Slide_59_Drag itemScriptRef = dropped.GetComponent<Slide_59_Drag>(); // Get the 'Slide_59_Drag' script component from the dropped object.

        // Check if this drop area is empty (no child object).
        if (transform.childCount == 0)
        {
            Debug.Log("OutRight"); // Log a message to indicate a successful drop.
            itemScriptRef.transform.SetParent(transform); // Set the dropped object as a child of the drop area.
            itemScriptRef.image.raycastTarget = false; // Disable raycasting on the dropped object.

            // Check if the dropped object's answer matches the correct answer for the current slide.
            if (itemScriptRef.answer == _correctAnswer[slide59.slideNumber])
            {
                Debug.Log("right"); // Log a message for the correct answer.
                slide59.nextButton.SetActive(true); // Activate the 'nextButton' in the 'slide59' script.
            }
            else
            {
                slide59.nextButton.SetActive(true); // Activate the 'nextButton' in the 'slide59' script for other cases.
            }
        }
        else
        {
            Debug.Log("wrong"); // Log a message indicating that the drop area is not empty.
            slide59.nextButton.SetActive(true); // Activate the 'nextButton' in the 'slide59' script.
            slide59.NextSlide(); // Call the 'NextSlide' method in the 'slide59' script.
        }
    }

    // Reset the positions of draggable objects.
    public void ResetDragObjectsPositions()
    {
        Slide_59_Drag[] dragObjects = FindObjectsOfType<Slide_59_Drag>(); // Find all 'Slide_59_Drag' objects in the scene.
        foreach (Slide_59_Drag dragObject in dragObjects)
        {
            dragObject.ResetPosition(); // Call the 'ResetPosition' method on each 'Slide_59_Drag' object.
        }
    }

    // Destroy all child objects within this GameObject.
    public void DestroyChildObjects()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject); // Destroy each child object within this GameObject.
        }
    }
}