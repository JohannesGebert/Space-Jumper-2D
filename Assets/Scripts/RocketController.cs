using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour {

  Animator animator;

  // Use this for initialization
  void Start () {
    animator = GetComponent<Animator>();		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      animator.SetBool("Close", true);
    }
  }
}
