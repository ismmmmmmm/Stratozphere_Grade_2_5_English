using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawPrefab : MonoBehaviour
{
    public GameObject spawnPrefab;

    public void SpawnPrefab()
    {
        if (spawnPrefab != null)
        {
            GameObject newObject = Instantiate(spawnPrefab, transform.position, Quaternion.identity);
            newObject.transform.parent = transform; // Make the spawned object a child of this object
        }
    }
}
