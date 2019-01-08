using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NinjaControllerScript : MonoBehaviour {

  public float MaxSpeed = 10f;
  private bool FacingRight = true;
  private bool Attack;

  public float fallMultiplier = 2.5f;
  public float lowJumpMultiplier = 2f;

  private int CoinCounter;
  public Text scoreText;

  Animator Animator;
  Rigidbody2D rb;

  // Use this for initialization
  void Start()
  {
    Animator = GetComponent<Animator>();

    CoinCounter = 0;
  }

  void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
  }

	void FixedUpdate ()
  {
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

  //public IEnumerator Knockback(float KnockDuration, float KnockbackPower, Vector3 KnockbackDirection)
  //{
  //  float timer = 0;
  //  rb.velocity = new Vector3(rb.velocity.x, 0);
  //  while (KnockDuration < timer)
  //  {
  //    timer += Time.deltaTime;

  //    rb.AddForce(new Vector3(KnockbackDirection.x * -100, KnockbackDirection.y * KnockbackPower, transform.position.x));
  //  }
  //  yield return 0;
  //}

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
    if(GameController.health > 1)
    {
      Animator.SetTrigger("Hurt");
      transform.Translate(Vector2.left * 5);
    }
    GameController.health -= damage;
  }
}
