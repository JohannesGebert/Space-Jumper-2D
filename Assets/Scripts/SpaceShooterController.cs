using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpaceShooterController : MonoBehaviour
{
  public GameObject Life1, Life2, Life3;//, gameOver;
  public string NextPlanet;

  public Text timeText;
  float WaitOneSecond = 60.0f;
  float Minute = 1.0f;
  float timer;
  public bool LevelCompleted = false;

  public GameObject LevelCompletedUI;

  public static int health;

  // Use this for initialization
  void Start()
  {
    health = 3;

    timer = 60;
  }

  // Update is called once per frame
  void Update()
  {
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
        //gameOver.gameObject.SetActive(true);
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
    timer -= Time.deltaTime;
    if (timer <= WaitOneSecond)
    {
      if (WaitOneSecond <= 59.0f)
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

      if (WaitOneSecond <= 0)
      {
        timeText.text = Minute.ToString() + ":0" + WaitOneSecond.ToString();
        timer = 0;
        LevelCompleted = true;
        Time.timeScale = 0f;
        LevelCompletedUI.SetActive(true);
      }

      if (WaitOneSecond == 60.0f)
      {
        timeText.text = Minute.ToString() + ":00";
        Minute -= 1.0f;
      }
      WaitOneSecond -= 1.0f;
    }
  }

  public void NextLevel()
  {
    SceneManager.LoadScene(NextPlanet + "Scene");
  }
}
