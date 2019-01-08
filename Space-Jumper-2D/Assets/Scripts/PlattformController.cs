using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlattformController : MonoBehaviour {

  enum PlattformState { moveLeft, moveRight, moveUp, moveDown };
  [StringInList("LeftToRight", "RightToLeft", "TopToDown" , "DownToUp")] public int Movement;
  public float MoveSpeed = 10.0f;
  public float PatrolRange = 5.0f;

  PlattformState currentState;
  Vector3 startPosition;
  Vector3 velocity = Vector3.zero;

  // Use this for initialization
  void Start () {
    if (Movement == 0)
    {
      currentState = PlattformState.moveLeft;
    }
    else if (Movement == 1)
    {
      currentState = PlattformState.moveRight;
    }
    else if (Movement == 2)
    {
      currentState = PlattformState.moveUp;
    }
    else if (Movement == 3)
    {
      currentState = PlattformState.moveDown;
    }

      startPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {

    float distanceToStartPosition = Vector3.Distance(startPosition, transform.position);
    if (distanceToStartPosition > PatrolRange)
    {
      if (Movement == 0)
      {
        ChangeDirectionX();
      } else
      {
        ChangeDirectionY();
      }
    }

    if (Movement == 0)
    {
      if (currentState == PlattformState.moveLeft)
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
      if(currentState == PlattformState.moveUp)
      {
        MoveUp();
      }
      else
      {
        MoveDown();
      }
    }
    GetComponent<Rigidbody2D>().velocity = velocity;
  }

  void ChangeDirectionX()
  {
    if (transform.position.x <= startPosition.x)
    {
      currentState = PlattformState.moveRight;
    }
    else if (transform.position.x >= startPosition.x)
    {
      currentState = PlattformState.moveLeft;
    }
  }

  void ChangeDirectionY()
  {
    if (transform.position.y <= startPosition.y)
    {
      currentState = PlattformState.moveUp;
    }
    else if (transform.position.y >= startPosition.y)
    {
      currentState = PlattformState.moveDown;
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

  private void OnCollisionEnter2D(Collision2D collision)
  {
    
    if (collision.gameObject.tag == "Player")
    {
      //Debug.Log("Set Player");
      //collision.otherRigidbody.transform.SetParent(transform);
      //collision.collider.transform.SetParent(transform);
      //collision.rigidbody.transform.SetParent(transform);
    }
  }

  private void OnCollisionStay2D(Collision2D collision)
  {
    Debug.Log("Set Player: " + collision.gameObject.tag);
    if (collision.gameObject.tag == "Player")
    {
      //Debug.Log("Set Player");
      //collision.otherRigidbody.transform.SetParent(transform);
      //collision.collider.transform.SetParent(transform);
      //collision.rigidbody.transform.SetParent(transform);
    }
  }

  private void OnCollisionExit2D(Collision2D collision)
  {
    if (collision.gameObject.tag == "Player")
    {
     // Debug.Log("Unset Player");
      //collision.collider.transform.SetParent(null);
      //collision.otherRigidbody.transform.SetParent(null);
      //collision.otherCollider.transform.SetParent(null);
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
