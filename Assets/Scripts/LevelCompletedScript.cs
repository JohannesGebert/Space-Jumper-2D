using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletedScript : MonoBehaviour
{
  public GameObject LevelCompletedUI;
  public RocketController rocket;

  bool nextLevelClicked = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    if (nextLevelClicked)
    {
      Time.timeScale = 1f;
    }
  }

  public void NextLevel()
  {
    LevelCompletedUI.SetActive(false);
    nextLevelClicked = true;

    rocket.NextLevel();
  }
}
