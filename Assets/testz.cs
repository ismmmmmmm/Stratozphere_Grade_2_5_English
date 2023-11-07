using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class testz : MonoBehaviour
{
    int[] nums = { 1, 2, 3 };
    public GameObject go;
    void Start()
    {
        nums.Take(nums.Count() - 2).ToArray();
        
    }

    void Update()
    {
        
    }
}
