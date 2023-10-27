using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Transition_Script : MonoBehaviour
{
    public Animator anim;
    private string animName = "Plain_Transition_Reverse";
    void Start()
    {
        StartCoroutine(Use_Transition());
    }
    private IEnumerator Use_Transition()
    {
        anim = GameObject.FindGameObjectWithTag("Transition").GetComponent<Animator>();
        anim.Play(animName);
        yield return new WaitForSeconds(2);
    }
    

    }


