﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItemDamage : MonoBehaviour {
    private GameObject SoundController;
    public int damage;

    void Start()
    {
        SoundController = GameObject.Find("SoundController");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        // Retract Attack when with any collission for now.
        if (other.gameObject.CompareTag("Enemy"))
        {
            SoundController.GetComponent<SoundController>().playSound("Damage");
        } else
        {
            SoundController.GetComponent<SoundController>().playSound("PlayerSwordCollision");
        }

    }
}
