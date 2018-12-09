using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

  public GameObject Life1, Life2, Life3, gameOver;
  public static int health;

	// Use this for initialization
	void Start () {
    health = 3;
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
        break;
    }
  }
}
