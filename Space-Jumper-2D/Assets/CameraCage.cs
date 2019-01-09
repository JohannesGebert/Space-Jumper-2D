using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        // Den Spieler in dem Bereich der Kamera halten

        if (pos.y > Camera.main.orthographicSize)
        {
            pos.y = Camera.main.orthographicSize;
        }
    }
}
