using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Lesson_4_Slide22 : Audio_Narration
{
    public Animator slideAnimation;
    public string[] animName;
    private int[] animCount = { 1, 1, 1, 1, 1, 1, 2, 2, 3, 1, 2, 3 };
    private int currentAnim = 0, audioCount;

    void Start()
    {
        StartCoroutine(StartSceneCoroutine());
    }

    private IEnumerator StartSceneCoroutine()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < animCount.Length; i++)
        {
            SetAudioNarration(audioCount);
            for (int j = 0; j < animCount[i]; j++)
            {
                if (currentAnim < animName.Length)
                {
                    slideAnimation.Play(animName[currentAnim]);
                    // Get the animation length
                    float animationLength = GetAnimationLength(animName[currentAnim]);
                    // Wait for the animation length
                    yield return new WaitForSeconds(animationLength);
                    currentAnim++;
                }
            }
            audioCount++;
        }
        nextButton.SetActive(true);
    }

    // Function to get the animation length
    private float GetAnimationLength(string animationName)
    {
        AnimationClip clip = slideAnimation.runtimeAnimatorController.animationClips
            .FirstOrDefault(animClip => animClip.name == animationName);

        if (clip != null)
        {
            return clip.length;
        }
        return 0f;
    
    }
}
