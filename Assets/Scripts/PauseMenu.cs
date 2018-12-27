using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

  public static bool GameIsPaused = false;

  public GameObject PauseMenuUI;
  

  bool mute = false;
	
	// Update is called once per frame
	void Update () {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      if (GameIsPaused)
      {
        Resume();
      }
      else
      {
        Pause();
      }
    }
	}

  public void Resume()
  {
    PauseMenuUI.SetActive(false);
    Time.timeScale = 1f;
    GameIsPaused = false;

    if(mute == false)
      AudioListener.pause = false;
  }

  void Pause()
  {
    PauseMenuUI.SetActive(true);
    Time.timeScale = 0f;
    GameIsPaused = true;

    //AudioListener.pause = true;
  }

  public void LoadMenu()
  {
    Debug.Log("Loading menu...");
    SceneManager.LoadScene(0);
    Time.timeScale = 1f;
    PauseMenuUI.SetActive(false);
    GameIsPaused = false;

    AudioListener.pause = false;
  }

  public void QuitGame()
  {
    Debug.Log("Quiting game...");
    Application.Quit();
  }

  public void Mute()
  {
    if (mute == false)
    {
      mute = true;
      AudioListener.volume = 0f;
    }
    else if (mute == true)
    {
      mute = false;
      AudioListener.volume = 1f;
    }
  }
}
