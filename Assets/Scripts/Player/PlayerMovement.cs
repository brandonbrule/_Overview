﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	private GameObject PlayerStateManager;
	private GameObject GameController;
    private GameObject SoundController;
    private Rigidbody2D rbody;
	private Animator anim;
	public float playerSpeed = 1f;
	private float originalPlayerSpeed;
	public char direction;
    private char last_direction;


	// Use this for initialization
	void Start () {
		PlayerStateManager = GameObject.Find("Player/PlayerStateManager");
		GameController = GameObject.Find("GameController");
        SoundController = GameObject.Find("SoundController");
		rbody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		originalPlayerSpeed = playerSpeed;
		direction = GameController.GetComponent<GameController>().PlayerDirection;

        // Set Player Direction on Scene Load
        // East
        if (direction == 'e'){
			anim.SetFloat("input_x", -1);
			anim.SetFloat("input_y", 0);
		
		// West
		} else if (direction == 'w'){
			anim.SetFloat("input_x", 1);
			anim.SetFloat("input_y", 0);
		
		// North
		} else if (direction == 'n'){
			anim.SetFloat("input_x", 0);
			anim.SetFloat("input_y", 1);
		
		// South
		} else {
			anim.SetFloat("input_x", 0);
			anim.SetFloat("input_y", -1);
        }


        


    }

	void setDirection(float x, float y){
		char newDirection = 'a';

		

		// East
		if(x == -1 && y == 0){
			newDirection = 'e';

		// West
		} else if(x == 1 && y == 0){
			newDirection = 'w';

		// North
		} else if(x == 0 && y == 1){
			newDirection = 'n';

		// South
		} else if(x == 0 && y == -1){
			newDirection = 's';
		} else {
			newDirection = '-';
		}

		
		// Update State Direction If Changed
		if(newDirection != direction){
			direction = newDirection;
			PlayerStateManager.GetComponent<PlayerState>().updateDirection(direction);
		}

		
	}

    void sprint()
    {
        if(last_direction == direction)
        {
            Debug.Log("Same Direction");
        }
        last_direction = direction;
    }

    void MovementConditions() {

        if (anim.GetBool("isattacking"))
        {
            //playerSpeed = originalPlayerSpeed / 2;
        }
        if (Input.GetKey(KeyCode.LeftShift) || anim.GetBool("isattacking"))
        {
            //playerSpeed = originalPlayerSpeed / 2;
        }
        else
        {
        	playerSpeed = originalPlayerSpeed;
            //sprint();
        }
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		Vector2 movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        var force = transform.position;

       

        if (Input.anyKeyDown)
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                rbody.AddRelativeForce(Vector3.forward * 100.0f);
                rbody.AddForce(transform.up * 100.0f);
                sprint();
                //this.gameObject.GetComponent<Rigidbody2D>().AddForce(force * 100);
                Debug.Log("A key or mouse click has been detected");
            }
           
        }
        
        
        // If Player Is Walking
		if (movement_vector != Vector2.zero){
            SoundController.GetComponent<SoundController>().playSound("Walking");
            MovementConditions();

            if (anim.GetBool("isattacking") == false)
            {
                anim.SetBool("iswalking", true);
                anim.SetFloat("input_x", movement_vector.x);
                anim.SetFloat("input_y", movement_vector.y);
                setDirection(movement_vector.x, movement_vector.y);
            }
            

            // If Player Stops Walking
        } else {
            SoundController.GetComponent<SoundController>().stopSound("Walking");
            anim.SetBool("iswalking", false);
		}


        // Stop Walking If Attacking
        /*
        if (anim.GetBool("isattacking") == false)
        {
            rbody.MovePosition(rbody.position + ((movement_vector * Time.deltaTime) * playerSpeed));
        }
        */

        rbody.MovePosition(rbody.position + ((movement_vector * Time.deltaTime) * playerSpeed));


    }
}
