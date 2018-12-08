﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 20f;
    public int damage = 100;
    public Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb.velocity = transform.right * speed;
	}
	
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        EnemyControllerScript enemy = hitInfo.GetComponent<EnemyControllerScript>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Debug.Log(hitInfo.name);
        Destroy(gameObject);
    }
	
}
