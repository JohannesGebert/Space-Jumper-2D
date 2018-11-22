using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NinjaControllerScript : MonoBehaviour {

  public float MaxSpeed = 10f;
  private bool FacingRight = true;

  bool Grounded = false;
  public Transform GroundCheck;
  float GroundRadius = 0.2f;
  public LayerMask WhatIsGround;
  public float JumpForce = 700;

  private int CoinCounter;
  public Text scoreText;

  Animator Animator;

	// Use this for initialization
	void Start () {
    Animator = GetComponent<Animator>();

    CoinCounter = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
  {
    Grounded = Physics2D.OverlapCircle(GroundCheck.position, GroundRadius, WhatIsGround);
    Animator.SetBool("Ground", Grounded);

    //Animator.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

    float Move = Input.GetAxis("Horizontal");

    Animator.SetFloat("Speed", Mathf.Abs(Move));

    GetComponent<Rigidbody2D>().velocity = new Vector2(Move * MaxSpeed, GetComponent<Rigidbody2D>().velocity.y);

    if (Move > 0 && !FacingRight)
    {
      flip();
    }
    else if (Move < 0 && FacingRight)
    {
      flip();
    }
  }

  void Update()
  {
    if (Grounded && Input.GetKeyDown(KeyCode.Space))
    {
      Animator.SetBool("Ground", false);
      GetComponent<Rigidbody2D>().AddForce(new Vector2(0, JumpForce));
    }
  }

  void flip()
  {
    FacingRight = !FacingRight;
    Vector3 theScale = transform.localScale;
    theScale.x *= -1;
    transform.localScale = theScale;
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Coin")
    {
      //Grafik deaktivieren
      other.gameObject.GetComponent<Renderer>().enabled = false;
      other.gameObject.GetComponent<Collider2D>().enabled = false;
      //Sound der Münze abspielen
      AudioSource Audio = other.gameObject.GetComponent<AudioSource>();
      Audio.Play();
      //Münze zerstören
      Destroy(other.gameObject, Audio.clip.length);

      CoinCounter++;

      scoreText.text = CoinCounter.ToString();
      Debug.Log("Score: " + CoinCounter);
    }
    else if (other.tag == "BigCoin")
    {
      //Grafik deaktivieren
      other.gameObject.GetComponent<Renderer>().enabled = false;
      other.gameObject.GetComponent<Collider2D>().enabled = false;
      //Sound der Münze abspielen
      AudioSource Audio = other.gameObject.GetComponent<AudioSource>();
      Audio.Play();
      //Audio.ignoreListenerPause = true;     ---> ignoriert Tonsperre z.B. im Pause-Menu
      //Münze zerstören
      Destroy(other.gameObject, Audio.clip.length);

      CoinCounter += 5;

      scoreText.text = CoinCounter.ToString();
      Debug.Log("Score: " + CoinCounter);
    }
  }
}
