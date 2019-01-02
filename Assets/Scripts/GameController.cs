using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

  public GameObject Life1, Life2, Life3, gameOver;

  public Text timeText;
  float WaitOneSecond = 1.0f;
  float Minute = 0.0f;
  float timer;
  public bool LevelCompleted = false;

  public static int health;

	// Use this for initialization
	void Start () {
    health = 3;

    timer = 0;
  }
	
	// Update is called once per frame
	void Update () {
    switch (health)
    {
      case 3:
        Life1.gameObject.SetActive(true);
        Life2.gameObject.SetActive(true);
        Life3.gameObject.SetActive(true);
        break;
      case 2:
        Life1.gameObject.SetActive(true);
        Life2.gameObject.SetActive(true);
        Life3.gameObject.SetActive(false);
        break;
      case 1:
        Life1.gameObject.SetActive(true);
        Life2.gameObject.SetActive(false);
        Life3.gameObject.SetActive(false);
        break;
      case 0:
        Life1.gameObject.SetActive(false);
        Life1.gameObject.SetActive(false);
        Life1.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(true);
        Time.timeScale = 0f;
        break;
    }
    if (!LevelCompleted)
    {
      Timer();
    }
  }

  public void Timer()
  {
      timer += Time.deltaTime;
      if (timer > WaitOneSecond)
      {
        if (WaitOneSecond < 60)
        {
          if (WaitOneSecond < 10)
          {
            timeText.text = Minute.ToString() + ":" + "0" + WaitOneSecond.ToString();
          }
          else if (WaitOneSecond >= 10)
          {
            timeText.text = Minute.ToString() + ":" + WaitOneSecond.ToString();
          }
        }

        if (WaitOneSecond == 59.0f)
        {
          timeText.text = Minute.ToString() + ":" + WaitOneSecond.ToString();
        }

        if (WaitOneSecond >= 60)
        {
          Minute += 1.0f;
          WaitOneSecond = 0.0f;
          if (WaitOneSecond < 10)
          {
            timeText.text = Minute.ToString() + ":" + "0" + WaitOneSecond.ToString();
          }
          else if (WaitOneSecond >= 10)
          {
            timeText.text = Minute.ToString() + ":" + WaitOneSecond.ToString();
          }
          timer = 0;
        }
        WaitOneSecond += 1.0f;
      }
  }
}
