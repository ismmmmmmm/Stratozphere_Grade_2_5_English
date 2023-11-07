using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lesson_4_Slide55 : Audio_Narration
{
    [SerializeField] public string[] animName;
    public GameObject[] canvas;
    public Button nextSlideButton;
    public Animator slide3Animation, transit;
    public int animCount;
    private int[] audioAvailable= {3,1,1,1,1,1,1,3,3};
    void Start()
    {
        nextSlideButton.gameObject.SetActive(false);
        nextSlideButton.onClick.AddListener(LoadScene);
        StartCoroutine(StartScene());
    }

    private IEnumerator StartScene()
    {
        yield return new WaitForSeconds(4);
        for (int i = 0; i < animName.Length; i++)
        {

            slide3Animation.Play(animName[i]);
            for (int j = 0; j < audioAvailable[i]; j++) { 
            SetAudioNarration(animCount);
            yield return new WaitForSeconds(clip[animCount].length + .2f); animCount++;
            }
        }

        nextSlideButton.gameObject.SetActive(true);


    }
  
}
