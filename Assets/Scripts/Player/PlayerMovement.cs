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

	void Run(){
		// Run (Shift)
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerSpeed = 2;
        } else {
        	playerSpeed = originalPlayerSpeed;
        }
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		Vector2 movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));



        // If Player Is Walking
		if (movement_vector != Vector2.zero){
            SoundController.GetComponent<SoundController>().playSound("Walking");
            anim.SetBool("iswalking", true);
			anim.SetFloat("input_x", movement_vector.x);
			anim.SetFloat("input_y", movement_vector.y);
			Run();
			setDirection(movement_vector.x, movement_vector.y);

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
