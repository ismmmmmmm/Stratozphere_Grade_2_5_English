using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teachers_Button : Audio_Narration
{
    public void GoToPage66()
    {
        StartCoroutine(LoadSceneCoroutineS66());
    }

    public void GoToPage75()
    {
        StartCoroutine(LoadSceneCoroutineS75());
    }

    private IEnumerator LoadSceneCoroutineS66()
    {
        source.enabled = false;
        transition.Play("Slide_5_Transition_Out");
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("Slide_66");
    }

    private IEnumerator LoadSceneCoroutineS75()
    {
        source.enabled = false;
        transition.Play("Slide_5_Transition_Out");
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("Slide_75");
    }
}
