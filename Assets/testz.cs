using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class testz : Audio_Narration
{
   
    void Start()
    {
        StartCoroutine(Plain_transition());
        
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F)) { StartCoroutine(Plain_transition()); }
    }
}
