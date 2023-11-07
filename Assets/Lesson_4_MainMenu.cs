using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lesson_4_MainMenu : Audio_Narration
{
    // Start is called before the first frame update
    public Button start;
    void Start()
    {
        start.onClick.AddListener(StartScene);
    }

    public void StartScene()
    {
        LoadScene();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
