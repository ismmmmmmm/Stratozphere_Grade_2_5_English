using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonShowWall : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject targetObject, inviwall;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (targetObject.activeSelf)
        {
            inviwall.SetActive(true);
        }
        else
        {
            inviwall.SetActive(false);
        }
    }
}
