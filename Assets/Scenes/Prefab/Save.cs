using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Save : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Scene", SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Current Saved Scene is: " + PlayerPrefs.GetInt("Scene"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
