using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControllerScript : MonoBehaviour {

  enum EnemyStates { moveLeft = 0, moveRight = 1, moveStop = 2, jumpAir = 3, enemyDie = 4, goHome = 5 }
  public float MoveSpeed = 20.0f;

  EnemyStates enemyState = EnemyStates.moveLeft;

  Transform homePosition;
  //float deathForce = 3.0f;

  EnemyStates currentState;
  Vector3 velocity = Vector3.zero;
  
  bool isRight = false;

  Vector3 myTransform;
  //float resetMoveSpeed = 0.0f;

  SpriteRenderer spriteRenderer;
  Animator animator;


  public bool CanJump = false;
	// Use this for initialization
	void Start () {
    Vector3 myTransform = transform.position;
    
    spriteRenderer = GetComponent<SpriteRenderer>();
    animator = GetComponent<Animator>();
  }


  // Update is called once per frame
  void Update () {
    //if(controller.isGrounded)
    {
      switch(enemyState)
      {
        case EnemyStates.moveLeft:
          PatrolLeft();
          break;
        case EnemyStates.moveRight:
          PatrolRight();
          break;
        case EnemyStates.moveStop:
          if(isRight)
          {
            IdleRight();
          }
          else
          {
            IdleLeft();
          }
          
          break;
      }
    }

    
    //aniPlay.SetFloat("Speed", Mathf.Abs(Move));
    //velocity.y -= gravity * Time.deltaTime;
  

    GetComponent<Rigidbody2D>().velocity = velocity;
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Obstacle")
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
    }
  }

  void PatrolRight()
  {
    velocity.x = MoveSpeed * Time.deltaTime;
    animator.SetFloat("Walk", Mathf.Abs(MoveSpeed));
    spriteRenderer.flipX = !isRight;
    isRight = true;
  }
  void PatrolLeft()
  {
    velocity.x = -MoveSpeed * Time.deltaTime;
    animator.SetFloat("Walk", Mathf.Abs(MoveSpeed));
    spriteRenderer.flipX = !isRight;
    isRight = false;
  }

  void IdleRight()
  {
    velocity.x = 0;
    animator.SetFloat("Walk", Mathf.Abs(0));
    isRight = true;
  }
  void IdleLeft()
  {
    velocity.x = 0;
    animator.SetFloat("Walk", Mathf.Abs(0));
    isRight = false;
  }

  void DieRight()
  {
    velocity.x = 0;
    new WaitForSeconds(0.1f);
    animator.SetBool("Dead", true);
    new WaitForSeconds(0.4f);
    //destroy gameobject
  }
}
