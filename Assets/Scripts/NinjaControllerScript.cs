using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NinjaControllerScript : MonoBehaviour {

  public float MaxSpeed = 10f;
  private bool FacingRight = true;
  private bool Attack;
  float timer;

  //bool Grounded = false;
  //public Transform GroundCheck;
  //float GroundRadius = 0.2f;
  //public LayerMask WhatIsGround;
  //public float JumpForce = 700;

  //[Range(1, 40)]
  //public float JumpVelocity;

  public float fallMultiplier = 2.5f;
  public float lowJumpMultiplier = 2f;

  private int CoinCounter;
  public Text scoreText;
  public Text timeText;
  float WaitOneSecond = 1.0f;
  float Minute = 0.0f;

  Animator Animator;
  Rigidbody2D rb;

  // Use this for initialization
  void Start()
  {
    Animator = GetComponent<Animator>();

    CoinCounter = 0;

    timer = 0;
  }

  void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
  }

	// Update is called once per frame
	void FixedUpdate ()
  {
    //Grounded = Physics2D.OverlapCircle(GroundCheck.position, GroundRadius, WhatIsGround);
    //Animator.SetBool("Ground", Grounded);

    float Move = Input.GetAxis("Horizontal");

    if (!this.Animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
    {
      GetComponent<Rigidbody2D>().velocity = new Vector2(Move * MaxSpeed, GetComponent<Rigidbody2D>().velocity.y);
    }

    Animator.SetFloat("Speed", Mathf.Abs(Move));


    if (Move > 0 && !FacingRight)
    {
      flip();
    }
    else if (Move < 0 && FacingRight)
    {
      flip();
    }

    if (rb.velocity.y < 0)
    {
      rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
    }
    else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
    {
      rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
    }

    HandleAttacks();

    ResetValues();
  }

  void Update()
  {
    HandleInput();
    //  //if (Grounded && Input.GetKeyDown(KeyCode.Space))
    //  //{
    //  //  Animator.SetBool("Ground", false);
    //  //  GetComponent<Rigidbody2D>().AddForce(new Vector2(0, JumpForce));
    //  //}

    //  //if (Grounded && Input.GetKeyDown(KeyCode.Space))
    //  //{
    //  //  GetComponent<Rigidbody2D>().velocity = Vector2.up * JumpVelocity;
    //  //}

    Timer();
  }



  void flip()
  {
    FacingRight = !FacingRight;

    transform.Rotate(0f, 180f, 0f);
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Coin")
    {
      //Grafik deaktivieren
      other.gameObject.GetComponent<Renderer>().enabled = false;
      other.gameObject.GetComponent<Collider2D>().enabled = false;
      //Destroy(other.gameObject);
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

  public IEnumerator Knockback(float KnockDuration, float KnockbackPower, Vector3 KnockbackDirection)
  {
    float timer = 0;
    rb.velocity = new Vector3(rb.velocity.x, 0);
    while (KnockDuration < timer)
    {
      timer += Time.deltaTime;

      rb.AddForce(new Vector3(KnockbackDirection.x * -100, KnockbackDirection.y * KnockbackPower, transform.position.x));
    }
    yield return 0;
  }

  /*void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "Enemy")
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
  }*/

  private void HandleAttacks()
  {
    if (Attack && !this.Animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
    {
      Animator.SetTrigger("Attack");
      rb.velocity = Vector2.zero;
    }
  }

  private void HandleInput()
  {
    if (Input.GetKeyDown(KeyCode.Tab))
    {
      Attack = true;
    }
  }

  private void ResetValues()
  {
    Attack = false;
  }

  public void Damage(int damage)
  {
    GameController.health -= damage;
    Animator.SetTrigger("Hurt");
    transform.Translate(Vector2.left * 5);
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
    Debug.Log(timer);
  }
}
