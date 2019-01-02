using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketController : MonoBehaviour {

  Animator animator;

  public bool LevelCompleted = false;
  bool levelCompletedCanvas = false;

  public LevelCompletedScript levelCompleted;
  public GameController StopTimer;

  float timer;
  bool timerStarted = false;
  bool secondTimerStarted = false;
  bool liftOff = false;

  // Use this for initialization
  void Start () {
    animator = GetComponent<Animator>();

    timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
    if (timerStarted)
    {
      timer += Time.deltaTime;
    }
    if (LevelCompleted && !levelCompletedCanvas && timer >= 0.5f)
    {
      levelCompleted.LevelCompletedUI.SetActive(true);
      levelCompletedCanvas = true;
      Time.timeScale = 0f;
      StopTimer.LevelTime();
      timerStarted = false;
      timer = 0;
    }
    if (secondTimerStarted)
    {
      timer += Time.deltaTime;
    }

    if (liftOff && timer >= 2.5f)
    {
      LiftOff();
    }
	}

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      LevelCompleted = true;
      
      timerStarted = true;
      StopTimer.LevelCompleted = true;
    }
  }

  public void NextLevel()
  {
    animator.SetBool("Close", true);

    secondTimerStarted = true;
    liftOff = true;
  }

  void LiftOff()
  {
    //GetComponent<Rigidbody2D>().velocity = transform.up * 10f;
    GetComponent<Rigidbody2D>().velocity += Vector2.up * Physics2D.gravity.y * (-1 * Time.deltaTime);
  }
}
