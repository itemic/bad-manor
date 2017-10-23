﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fungus;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Characters
{
	/// <summary>
	/// Not implemented for prototype.</summary>
	/// 
	/// Source - youtube - gamesplusjames - Unity RPG Tutorial 26 & 27
	/// Have an empty walk area with an empty box collider & is trigger
	/// NPC - Have a box collider and is trigger
	/// Rigid Body - Freeze Rotation Z
	/// NPC Script - Walkzone - empty walk area component.
	/// 
	class NPC : Character
	{

		public float moveSpeed;
		private Rigidbody2D myRigidbody;
		public bool isWalking;
		public float walkTime;
		public float waitTime;
		private float walkCounter;
		private float waitCounter;
		private int walkDirection;
		private Animator _animator;
		public Collider2D walkzone; 
		public AnimatorControllerParameter[] parameters;
		/// <summary>
		/// Bottom left and top right corners of the walking area
		/// </summary>
		private Vector2 minwalkpoint;
		private Vector2 maxwalkpoint;
		public Flowchart dialogueFlowchart;




		void Start(){
			_animator = GetComponent<Animator>();
			myRigidbody = GetComponent<Rigidbody2D> ();
			waitCounter = waitTime;	
			walkCounter = walkTime; 
			chooseDirection ();
			minwalkpoint = walkzone.bounds.min;
			maxwalkpoint = walkzone.bounds.max;

		}

	//	void Update(){
		void FixedUpdate()
		{

			bool inConversation = dialogueFlowchart.GetBooleanVariable("IN_CONVERSATION");
			if (inConversation)
			{
				isWalking = false;
			}


		 if (isWalking) {
				Debug.Log ("Is walking");

				walkCounter = walkCounter - Time.deltaTime;
				if (walkCounter < 0) {
					isWalking = false;
					waitCounter = waitTime;


				}

				switch (walkDirection) {
				case 0:
					myRigidbody.velocity = new Vector2 (0, moveSpeed);
					if (transform.position.y > maxwalkpoint.y) {
						isWalking = false;
						waitCounter = waitTime;

					}
		/*			_animator.SetBool("WalkRight",false);
					_animator.SetBool("Idle",false);
					_animator.SetBool("WalkDown",false);
					_animator.SetBool("WalkUp",true);
					_animator.SetBool("WalkLeft",false);
					_animator.SetBool("IdleUp",false);*/
					updateanimator ("WalkUp");

					break;
				case 1:
					Debug.Log ("Should be animated for right");
				/*	_animator.SetBool("WalkRight",true);
					_animator.SetBool("Idle",false);
					_animator.SetBool("WalkDown",false);
					_animator.SetBool("WalkUp",false);
					_animator.SetBool("WalkLeft",false);
					_animator.SetBool("IdleUp",false);*/

					updateanimator ("WalkRight");


		/*			foreach (AnimatorControllerParameter p in _animator) {
						_animator.SetBool (p.name, false);
					
					}
					_animator.SetBool("WalkRight",true);*/
					myRigidbody.velocity = new Vector2 (moveSpeed,0);


				if (transform.position.x > maxwalkpoint.x) {
						isWalking = false;
						waitCounter = waitTime;

					}

					break;
				case 2:
					myRigidbody.velocity = new Vector2 (0, -moveSpeed);

					/*_animator.SetBool ("Idle", false);
					_animator.SetBool ("IdleUp", false);
					_animator.SetBool ("WalkUp", false);
					_animator.SetBool ("WalkLeft", false);
					_animator.SetBool ("WalkDown", true);
					_animator.SetBool ("WalkRight", false);
*/
					updateanimator ("WalkDown");
					if (transform.position.y < minwalkpoint.y) {
						isWalking = false;
						waitCounter = waitTime;

					}

					break;
				case 3:

				/*	_animator.SetBool ("Idle", false);
					_animator.SetBool ("IdleUp", false);
					_animator.SetBool ("WalkUp", false);
					_animator.SetBool ("WalkLeft", true);
					_animator.SetBool ("WalkDown", false);
					_animator.SetBool ("WalkRight", false);*/

					updateanimator ("WalkLeft");
					myRigidbody.velocity = new Vector2 (-moveSpeed,0 );

					if (transform.position.x < minwalkpoint.x) {
						isWalking = false;
						waitCounter = waitTime;

					}

					break;
				}
		
				if (walkCounter < 0) {
					isWalking = false;
					waitCounter = waitTime;
				//	updateanimator ();


				}
			} else {
				if (_animator.GetBool ("WalkUp")) {
					updateanimator ("IdleUp");
				} else {
					updateanimator ("Idle");
				}
				waitCounter = waitCounter - Time.deltaTime;
				myRigidbody.velocity = Vector2.zero;

				if (waitCounter < 0) {
					chooseDirection ();

				}
			
			}
			Debug.Log (Input.GetAxisRaw ("Horizontal"));
	//		_animator.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
//			_animator.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
		
		}



		public void chooseDirection ()
		{
			Debug.Log ("Choosing Direction");
		
			walkDirection = Random.Range (0, 4);
		
			isWalking = true;
			walkCounter = walkTime;
		
		}

	/*	void FixedUpdate(){
		_animator.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
		_animator.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
		
		
		}*/





		void updateanimator(String state){
		/*	if (_animator.GetBool ("WalkUp")) {
				_animator.SetBool ("Idle", false);

				_animator.SetBool ("WalkUp", false);
				_animator.SetBool ("WalkLeft", false);
				_animator.SetBool ("WalkDown", false);
				_animator.SetBool ("WalkRight", false);
				_animator.SetBool ("IdleUp", true);
			}else {
*/

			_animator.SetBool ("Idle", false);
			_animator.SetBool ("IdleUp", false);
			_animator.SetBool ("WalkUp", false);
			_animator.SetBool ("WalkLeft", false);
			_animator.SetBool ("WalkDown", false);
			_animator.SetBool ("WalkRight", false);

			_animator.SetBool (state, true);

		}



	}
}
