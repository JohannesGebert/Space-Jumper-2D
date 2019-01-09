using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

  public string NextPlanet;

  public void PlayGame()
  {
    SceneManager.LoadScene(1);
    Time.timeScale = 1f;
    GameController.health = 3;
  }

  public void QuitGame()
  {
    Application.Quit();
  }

  public void LoadMenu()
  {
    SceneManager.LoadScene("GameMenu");
  }
}
