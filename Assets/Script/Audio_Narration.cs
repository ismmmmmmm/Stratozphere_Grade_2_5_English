using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Audio_Narration : MonoBehaviour
{
    public AudioSource source;
    public AudioClip[] clip;
    public Animator transition;
    public Animator wilbur;
    public GameObject invisibleWall;
    public GameObject nextButton;

    private void Start()
    {
        //StartCoroutine(Transition_Out());

    }
    public void SetAudioNarration(int audioClip)
    {
        source.clip = clip[audioClip];
        source.Play();
    }

    public void RestartScene(int buildIndex)
    {
        StartCoroutine(NextSceneCoroutine(buildIndex));
    }
    
    public IEnumerator NextSceneCoroutine(int buildIndex)
    {
        transition.Play("Slide_5_Transition_Out");
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(buildIndex);
    }

  
  public IEnumerator Transition_Out()
    {
        yield return new WaitForSeconds(1);
        transition.Play("Plain_Transition_Reverse");
        yield return new WaitForSeconds(2.2f);
    }

    public IEnumerator Transition_In()
    {
        yield return new WaitForSeconds(1);
        transition.Play("Transition_Mud");
        yield return new WaitForSeconds(4);
    }

    public IEnumerator Plain_transition()
    {
        yield return new WaitForSeconds(1);
        transition.Play("Plain_Transition");
        yield return new WaitForSeconds(1.4f);
    }

    public IEnumerator Plain_transition_Reverse()
    {
        yield return new WaitForSeconds(1);
        transition.Play("Plain_Transition_Reverse");
        yield return new WaitForSeconds(1.4f);
    }
    public IEnumerator Vine_transitionOut()
    {
        yield return new WaitForSeconds(1);
        transition.Play("Transition_Door");
        yield return new WaitForSeconds(1.4f);
    }

    public IEnumerator Vine_transitionIn()
    {
        yield return new WaitForSeconds(1);
        transition.Play("Transition_Door_Reverse");
        yield return new WaitForSeconds(1.4f);
    }
    public void LoadScene()
    {
       StartCoroutine(LoadSceneCoroutine());
    }

    private IEnumerator LoadSceneCoroutine()
    {
        transition.Play("Slide_5_Transition_Out");
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadPlain()
    {
        StartCoroutine(LoadScenePlain());
    }

    private IEnumerator LoadScenePlain()
    {
        transition.Play("Transition_Plain");
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToZogleber()
    {
        StartCoroutine(GoToZogleberCoroutine());
    }

    private IEnumerator GoToZogleberCoroutine()
    {
        Application.OpenURL("https://zogleber.com");
        yield return new WaitForSeconds(2);
        LoadScene();
    }
}
