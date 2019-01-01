using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : MonoBehaviour
{

  private NinjaControllerScript Player;

    // Start is called before the first frame update
    void Start()
    {
    Player = GameObject.FindGameObjectWithTag("Player").GetComponent<NinjaControllerScript>();
    }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      Player.Damage(1);

      //Player.transform.Translate(Vector2.right * 5);

      //StartCoroutine(Player.Knockback(1f, 350, transform.position));
    }
  }
}
