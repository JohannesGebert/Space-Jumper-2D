using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destoyable : MonoBehaviour
{
    int health = 7;

    void OnTriggerEnter2D()
    {
        Debug.Log("Trigger!");

        health--;

        if (health <= 0)
        {
            Die();
        }

    }
    void Die()
    {
        Destroy(gameObject);
    }
}
