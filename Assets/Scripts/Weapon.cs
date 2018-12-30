using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public Transform firePoint;
    public GameObject bulletPrefab;

  private float timeBtwAttack;
  public float startTimeBtwAttack;

  public Transform attackPosition;
  public LayerMask whatIsEnemy;
  public float attackRange;
  public int Damage;

	// Update is called once per frame
	void Update () {
    if (Input.GetButtonDown("Fire1") && PauseMenu.GameIsPaused == false)
    {
      Shoot();
    }

    if (Input.GetKey(KeyCode.LeftShift))
    {
      timeBtwAttack = startTimeBtwAttack;
      Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, whatIsEnemy);
      for (int i = 0; i < enemiesToDamage.Length; i++)
      {
        enemiesToDamage[i].GetComponent<EnemyControllerScript>().TakeDamage(Damage);
      }
    }
	}

  void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(attackPosition.position, attackRange);
  }

  void Shoot()
  {
     //shooting Logic
    Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
  }
}
