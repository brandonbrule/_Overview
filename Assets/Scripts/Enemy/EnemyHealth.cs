﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour{
    private GameObject SoundController;
    public AudioSource AudioSpawn;
    public bool destroyable = true;
    public int health;
    private int damageTaken;
    private bool pushed_back = false;

    // Use this for initialization
    void Start()
    {
        SoundController = GameObject.Find("SoundController");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void isAttacked()
    {
        health = health - damageTaken;

        if (destroyable == true && health <= 0)
        {
            SoundController.GetComponent<SoundController>().playSound("EnemyDeath");
            Destroy(this.gameObject);
        }
    }

    public void resetPushBack()
    {
        pushed_back = false;


        if (this.gameObject.GetComponent<Rigidbody2D>()) { 
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            this.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
        }


        if (this.gameObject.GetComponent<MoveTowards>())
        {
            this.gameObject.GetComponent<MoveTowards>().ActivateMove();
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("ActiveItem"))
        {
            damageTaken = other.gameObject.GetComponent<ActiveItemDamage>().damage;
            isAttacked();
            pushed_back = true;

            if (pushed_back)
            {
                var force = transform.position - other.transform.position;
                force.Normalize();

                if (this.gameObject.GetComponent<MoveTowards>())
                {
                    this.gameObject.GetComponent<MoveTowards>().DectivateMove();
                }

                if (this.gameObject.GetComponent<Rigidbody2D>())
                {
                    this.gameObject.GetComponent<Rigidbody2D>().AddForce(force * 100);
                }
                Invoke("resetPushBack", 0.25f);
            }

        }

    }
}