using System.Collections;
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

	// Use this for initialization
	void Start () {
    
    spriteRenderer = GetComponent<SpriteRenderer>();
    animator = GetComponent<Animator>();
    resetMoveSpeed = MoveSpeed;
  }


  // Update is called once per frame
  void Update () {

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
      } else
      {
        MoveSpeed = resetMoveSpeed;
      }
    } else
    {
      if (Patrol)
      {
        distanceToHome = Vector3.Distance(PatrolPosition.position, transform.position);
        if(distanceToHome > homeRange)
        {
          GoHome();
        }
      }
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
    if (other.tag == "Obstacle" || other.tag == "Enemy")
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

  void DieLeft()
  {
    velocity.x = 0;
    new WaitForSeconds(0.1f);
    animator.SetBool("Dead", true);
    new WaitForSeconds(0.4f);
    //destroy gameobject
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
