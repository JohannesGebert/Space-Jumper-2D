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
  }
}
