using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMonsterScript : MonoBehaviour
{
  enum MovementState { moveLeft, moveRight, moveUp, moveDown, die };
  [StringInList("LeftToRight", "RightToLeft", "TopToDown", "DownToUp")] public int Movement;
  public float MoveSpeed = 10.0f;
  public float PatrolRange = 5.0f;

  MovementState currentState;
  Vector3 startPosition;
  Vector3 velocity = Vector3.zero;

  float bounceRange = 20.0f;

  SpriteRenderer spriteRenderer;
  Animator animator;
  Rigidbody2D rigidBody;

  // Use this for initialization
  void Start()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
    animator = GetComponent<Animator>();
    rigidBody = GetComponent<Rigidbody2D>();

    if (Movement == 0)
    {
      currentState = MovementState.moveLeft;
    }
    else if (Movement == 1)
    {
      currentState = MovementState.moveRight;
    }
    else if (Movement == 2)
    {
      currentState = MovementState.moveUp;
    }
    else if (Movement == 3)
    {
      currentState = MovementState.moveDown;
    }

    startPosition = transform.position;
  }

  // Update is called once per frame
  void Update()
  {

    if (currentState != MovementState.die)
    {
      float distanceToStartPosition = Vector3.Distance(startPosition, transform.position);
      if (distanceToStartPosition > PatrolRange)
      {
        if (Movement == 0)
        {
          ChangeDirectionX();
        }
        else
        {
          ChangeDirectionY();
        }
      }

      if (Movement == 0)
      {
        if (currentState == MovementState.moveLeft)
        {
          MoveLeft();
        }
        else
        {
          MoveRight();
        }
      }
      else
      {
        if (currentState == MovementState.moveUp)
        {
          MoveUp();
        }
        else
        {
          MoveDown();
        }
      }
    } else
    {
      Die();
    }

    GetComponent<Rigidbody2D>().velocity = velocity;
  }

  void ChangeDirectionX()
  {
    if (transform.position.x <= startPosition.x)
    {
      currentState = MovementState.moveRight;
    }
    else if (transform.position.x >= startPosition.x)
    {
      currentState = MovementState.moveLeft;
    }
  }

  void ChangeDirectionY()
  {
    if (transform.position.y <= startPosition.y)
    {
      currentState = MovementState.moveUp;
    }
    else if (transform.position.y >= startPosition.y)
    {
      currentState = MovementState.moveDown;
    }
  }

  void MoveLeft()
  {
    velocity.x = -MoveSpeed * Time.deltaTime;
  }

  void MoveRight()
  {
    velocity.x = MoveSpeed * Time.deltaTime;
  }

  void MoveUp()
  {
    velocity.y = MoveSpeed * Time.deltaTime;
  }

  void MoveDown()
  {
    velocity.y = -MoveSpeed * Time.deltaTime;
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.name == "Feet" && other.attachedRigidbody.velocity.y < 0)
    {
      GameObject playerLink = GameObject.FindGameObjectWithTag("Player");
      Rigidbody2D playerRigidBody = playerLink.GetComponent<Rigidbody2D>();
      playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, bounceRange);

      if (rigidBody)
      {
        currentState = MovementState.die;
      }
    }
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    
    if (collision.gameObject.tag == "Enemy")
    {
      Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
    }

    if (currentState == MovementState.die && collision.gameObject.tag == "Player")
    {
      Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
    }
  }

  void Die()
  {
    velocity.x = 0;
    animator.SetBool("Dead", true);
    if (gameObject.tag == "Enemy")
    {
      Destroy(gameObject.GetComponent<Collider2D>());
      Destroy(gameObject.GetComponentInChildren<Collider2D>());
    }
  }

  private void OnDrawGizmos()
  {
    Gizmos.color = Color.blue;
    //(startPosition)
    {
      Gizmos.DrawWireSphere(transform.position, PatrolRange);
    }
  }
}
