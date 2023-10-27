using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson_4_S1 : Audio_Narration
{
    // Start is called before the first frame update
    public Animator animSlide;
    public string[] animName;
    private int audioCount, animCount;
    void Start()
    {
        StartCoroutine(StartSceneCoroutine());
    }

    private IEnumerator StartSceneCoroutine()
    {
        yield return new WaitForSeconds(1);
        
        animSlide.Play(animName[animCount]);
        yield return new WaitForSeconds(clip[audioCount].length);animCount++;
        SetAudioNarration(audioCount);
        animSlide.Play(animName[animCount]);
        yield return new WaitForSeconds(clip[audioCount].length); audioCount++; animCount++;
        SetAudioNarration(audioCount);
        animSlide.Play(animName[animCount]);
        yield return new WaitForSeconds(clip[audioCount].length); audioCount++; animCount++;


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
