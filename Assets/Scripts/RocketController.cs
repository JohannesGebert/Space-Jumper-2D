using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  void OnTriggerEnter2D(Collider2D other)
  {
    /*if (other.tag == "Obstacle")
    {
      enemyState = EnemyStates.moveStop;
      new WaitForSeconds(0.4f);
      if (isRight)
      {
        enemyState = EnemyStates.moveLeft;
      }
      else
      {
        enemyState = EnemyStates.moveRight;
      }
    }*/
  }
}
