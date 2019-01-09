using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceshipmovement : MonoBehaviour {

    float speed = 5f;

    // Update is called once per frame
    void Update() {

        transform.Translate(new Vector3(
            Input.GetAxis("Horizontal") * speed,
            Input.GetAxis("Vertical") * speed,
            0) * Time.deltaTime);

        // Den spieler in dem Blickfeld der Camera halten

       
    }
}
