using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShooterWeapon : MonoBehaviour
{
  public Transform firePoint;
  public GameObject bulletPrefab;

  float timer = 0;

  // Update is called once per frame
  void Update()
  {
    if (Input.GetButtonDown("Fire1") && PauseMenu.GameIsPaused == false)
    {
      Shoot();
    }
  }

  void Shoot()
  {
    //shooting Logic
    Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
  }
}
