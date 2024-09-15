using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible Item: MonoBehaviour
{
    void OnTriggerEnter(collider other)
    {
        if(other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}