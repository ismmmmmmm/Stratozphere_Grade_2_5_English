// DropItem_Script.cs
using UnityEngine;
using UnityEngine.EventSystems;

public class DropItem_Script : MonoBehaviour, IDropHandler
{
    [SerializeField] private int _correctAnswer;
    public Lesson_4_Slide46 CurrentScene;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DragItem_Script itemScriptRef = dropped.GetComponent<DragItem_Script>();

        
            itemScriptRef.topOfObject = transform;

            if (itemScriptRef.answer== _correctAnswer)
            {
                CurrentScene.Correct();
            }
            else if (itemScriptRef.answer == 10)
            {
                // Handle special case
            }
            else
            {
                CurrentScene.Wrong();
            }
        }
    
}
