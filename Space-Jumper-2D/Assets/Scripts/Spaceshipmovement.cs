using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceshipmovement : MonoBehaviour {

    float MaxSpeed = 5f;
    float shipBounrdaryRad = 0.5f;

    void Start ()
    {

    }
    // Update is called once per frame
    void Update()
    {

       
        Vector3 pos = transform.position;
   
        Vector3 Velocity = new Vector3(Input.GetAxis("Horizontal") * MaxSpeed * Time.deltaTime,
                Input.GetAxis("Vertical") * MaxSpeed * Time.deltaTime, 0);
       
        pos += Velocity;

        if (pos.y + shipBounrdaryRad > Camera.main.orthographicSize)
        {
            pos.y = Camera.main.orthographicSize - shipBounrdaryRad;
        }
        if (pos.y - shipBounrdaryRad < -Camera.main.orthographicSize)
        {
            pos.y = -Camera.main.orthographicSize + shipBounrdaryRad;
        }
        
        transform.position = pos;
    }
}
