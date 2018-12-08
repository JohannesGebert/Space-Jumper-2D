﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControllerScript : MonoBehaviour {

  enum EnemyStates { moveLeft = 0, moveRight = 1, moveStop = 2, jumpAir = 3, die = 4, goHome = 5 }
  public float MoveSpeed = 20.0f;
  public float AttackMoveSpeed = 20.0f;
  public bool Invincible = false;
  public float attackRange = 1.0f;

  public bool CanChaseTarget = false;

  public bool DieOnJump = false;
  float bounceRange = 10.0f;

  EnemyStates enemyState = EnemyStates.moveLeft;

  public Transform ChaseTarget;

  public float searchRange = 3.0f;
  float resetMoveSpeed = 0.0f;
  float distanceToTarget = 0.0f;
  float reactionTimeDistance = 2.0f;

  public float homeRange = 1.0f;
  float distanceToHome = 0.0f;
  public bool Patrol = false;
  public Transform PatrolPosition;

  Vector3 velocity = Vector3.zero;
  
  bool isRight = false;
  SpriteRenderer spriteRenderer;
  Animator animator;

  Rigidbody2D rigidBody;

  // Use this for initialization
  void Start () {
    
    spriteRenderer = GetComponent<SpriteRenderer>();
    animator = GetComponent<Animator>();
    resetMoveSpeed = MoveSpeed;
    rigidBody = GetComponent<Rigidbody2D>();
  }


  // Update is called once per frame
  void Update () {

    if (enemyState != EnemyStates.die)
    {
      CheckIfChaseIsOn();
      CheckIfPatrolIsOn();
      
    }
    

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
      case EnemyStates.die:
        if (isRight)
        {
          DieRight();
        }
        else
        {
          DieLeft();
        }
        break;
      case EnemyStates.goHome:
        if (isRight)
        {
          DieRight();
        }
        else
        {
          DieLeft();
        }
        break;
    }
    
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

    if (other.gameObject.name == "Feet")
    {
      Debug.Log("Hit Player Feeet ");
      GameObject playerLink = GameObject.FindGameObjectWithTag("Player");
      Rigidbody2D playerRigidBody = playerLink.GetComponent<Rigidbody2D>();
      playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, bounceRange);
      Debug.Log("Hit Player Feeet ");
      if (DieOnJump)
      {
        Debug.Log("die on jump");
        if (rigidBody)
        {
          Debug.Log("setDie ");
          enemyState = EnemyStates.die;
        }
      }
    }
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.tag == "Enemy")
    {
      Debug.Log("Hit Enemy: ");
      {
        Debug.Log("Hit Enemy: Sleep ");
        Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
      }
    }
    if (enemyState == EnemyStates.die && collision.gameObject.tag == "Player")
    {
      Debug.Log("Hit Player: ");
      {
        Debug.Log("Hit Player: ignore ");
        Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
      }
    }
  }

  void CheckIfPatrolIsOn()
  {
    if (Patrol)
    {
      distanceToHome = Vector3.Distance(PatrolPosition.position, transform.position);
      if (distanceToHome > homeRange)
      {
        GoHome();
      }
    }
  }
  void CheckIfChaseIsOn()
  {

    distanceToTarget = Vector3.Distance(ChaseTarget.position, transform.position);
    if (distanceToTarget <= searchRange)
    {
      if (CanChaseTarget)
      {
        ChasePlayer();
      }

      if (distanceToTarget <= attackRange)
      {
        MoveSpeed = AttackMoveSpeed;
      }
      else
      {
        MoveSpeed = resetMoveSpeed;
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
    animator.SetBool("Dead", true);
    if (gameObject.tag == "Enemy")
    {
      Destroy(gameObject.GetComponent<Collider2D>());
      Destroy(gameObject.GetComponentInChildren<Collider2D>());
    }
  }

  void DieLeft()
  {
    velocity.x = 0;
    animator.SetBool("Dead", true);
    if (gameObject.tag == "Enemy")
    {
      Destroy(gameObject.GetComponent<Collider2D>());
      Destroy(gameObject.GetComponentInChildren<Collider2D>());
    }
  }

  void GoHome()
  {
    if (transform.position.x <= PatrolPosition.position.x)
    {
      enemyState = EnemyStates.moveRight;
    }
    else if (transform.position.x >= PatrolPosition.position.x)
    {
      enemyState = EnemyStates.moveLeft;
    }
  }

  void ChasePlayer()
  {
    if (transform.position.x <= ChaseTarget.position.x - reactionTimeDistance)
    {
      enemyState = EnemyStates.moveRight;
    } else if (transform.position.x >= ChaseTarget.position.x + reactionTimeDistance)
    {
      enemyState = EnemyStates.moveLeft;
    }
  }

  private void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, attackRange);

    if(CanChaseTarget)
    {
      Gizmos.color = Color.blue;
      Gizmos.DrawWireSphere(transform.position, searchRange);
    }
    
    if(Patrol && PatrolPosition)
    {
      Gizmos.color = Color.green;
      Gizmos.DrawWireSphere(PatrolPosition.position, homeRange);
    }
  }
}
