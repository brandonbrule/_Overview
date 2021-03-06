﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Enemy")
        {
            if (other.gameObject.GetComponent<MoveTowards>())
            {
                other.gameObject.GetComponent<MoveTowards>().ActivateMove();
            }

            if (other.gameObject.GetComponent<MoveRandomly>())
            {
                //other.gameObject.GetComponent<MoveRandomly>().DectivateMove();
            }

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

        if (other.tag == "Enemy")
        {
            if (other.gameObject.GetComponent<MoveTowards>())
            {
                other.gameObject.GetComponent<MoveTowards>().DectivateMove();
            }

            if (other.gameObject.GetComponent<MoveRandomly>())
            {
                other.gameObject.GetComponent<MoveRandomly>().ActivateMove();
            }

            if (other.gameObject.GetComponent<Rigidbody2D>())
            {
                other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                other.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
            }
            
        }
    }
}