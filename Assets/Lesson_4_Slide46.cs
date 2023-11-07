using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Lesson_4_Slide46 : Audio_Narration
{
    // Start is called before the first frame update
    [Header("Multiple")]
    public GameObject[] canvas,parentDrop;
    public Animator[] DragComponent,canvasAnim;
    public Sprite[] eggSprite;
    public Image[] eggImage;

    [Header("Single")]
    public Button nextSlideButton;
    public int levelCount;
    private int correctCount,wrongCount=6,audioCount;
    
    void Start()
    {
        nextSlideButton.onClick.AddListener(NextPage);
        StartCoroutine(StartSceneCoroutine());
    }

    private IEnumerator StartSceneCoroutine()
    {
        invisibleWall.SetActive(true);
        yield return new WaitForSeconds(3);
        eggImage[levelCount].sprite = eggSprite[levelCount];
        SetAudioNarration(audioCount);
        yield return new WaitForSeconds(clip[audioCount].length - 2);audioCount++;
        DragComponent[0].Play("Lesson_4_Slide46");
        yield return new WaitForSeconds(4.15f);
        DragComponent[0].Play("Lesson_4_Slide47");
        canvas[levelCount].gameObject.SetActive(true);
        SetAudioNarration(audioCount);
        yield return new WaitForSeconds(clip[audioCount].length - 2); audioCount++;
        invisibleWall.SetActive(false);
    }

    public void Correct()
    {
        StartCoroutine(CorrectCoroutine());
    }

    private IEnumerator CorrectCoroutine()
    {
        invisibleWall.SetActive(true);
        SetAudioNarration(11);
        canvasAnim[levelCount].Play("Lesson_4_Slide46_Egg");
        yield return new WaitForSeconds(1);
        canvas[levelCount].gameObject.SetActive(true);
        yield return new WaitForSeconds(clip[11].length);
        DragComponent[0].Play("Lesson_4_Slide47_Reverse");
        DestroyAllChildren();
        levelCount++;
        CheckSlideCount();
        nextSlideButton.gameObject.SetActive(true);
        invisibleWall.SetActive(false);
    }
    public void Wrong()
    {
        StartCoroutine(WrongCoroutine());
    }

    private IEnumerator WrongCoroutine()
    {
        invisibleWall.SetActive(true);
        SetAudioNarration(wrongCount);
        yield return new WaitForSeconds(clip[wrongCount].length);
        invisibleWall.SetActive(false); ;
    }

    public void NextPage()
    {
        StartCoroutine(NextPageCoroutine());
    }

    private IEnumerator NextPageCoroutine()
    {
        wrongCount++;
        eggImage[levelCount].sprite = eggSprite[levelCount];
        nextSlideButton.gameObject.SetActive(false);
        invisibleWall.SetActive(true);
        DragComponent[0].Play("Lesson_4_Slide47_Reverse");
        yield return new WaitForSeconds(2);
        DragComponent[0].Play("Lesson_4_Slide47");

        canvas[levelCount].gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        invisibleWall.SetActive(false);
    }
    public void DestroyAllChildren()
    {
        
            foreach (Transform child in parentDrop[0].transform)
            {
                Destroy(child.gameObject);
            }
        foreach (Transform child in parentDrop[1].transform)
        {
            Destroy(child.gameObject);
        }


    }
    private void CheckSlideCount()
    {
        if (levelCount == 5)
        {
            nextSlideButton.onClick.RemoveAllListeners();
            nextSlideButton.onClick.AddListener(NextSlidePage);
        }
    }

    public void NextSlidePage()
    {
        canvas[5].SetActive(false);
        canvas[6].SetActive(true);
    }
}
