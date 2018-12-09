using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

  [Range(1, 20)]
  public float jumpVelocity;

  public Transform GroundCheck;
  float GroundRadius = 0.2f;
  public LayerMask WhatIsGround;
  bool Grounded = false;

  Animator Animator;

  void Start()
  {
    Animator = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetButtonDown("Jump") && Grounded)
    {
      GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity;
    }
    Grounded = Physics2D.OverlapCircle(GroundCheck.position, GroundRadius, WhatIsGround);
    Animator.SetBool("Ground", Grounded);
  }
}
