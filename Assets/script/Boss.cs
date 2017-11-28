﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour {

	const int LEFT = -1;
	const int RIGHT = 1;

	public float speed = 0f;
	Animator bossAnimator;
	public int currentHealth = 4;
	public Slider bossSlider;

	//facing
	public GameObject bossGraphic;
	bool canFlip = true;
	bool facingRight = false;
	float facing;
	float flipTime = 2f;
	float nextFlipChance = 0f;

	//attacking
	public float chargeTime;
	float startAttack = 0f;
	float startChargingTime;
	bool attacking;
	bool attacking2 = false;
	bool enableAtk2 = false;
	Rigidbody2D bossRB;
	bool die = false;
	EdgeCollider2D x;

	// Use this for initialization
	void Start() {

		facing = LEFT;
		bossAnimator = GetComponent<Animator>();
		bossRB = GetComponent<Rigidbody2D>();
		bossGraphic = this.gameObject;

		bossSlider.maxValue = currentHealth;
		bossSlider.value = currentHealth;

		x = bossGraphic.GetComponentInChildren<EdgeCollider2D>();
		x.enabled = false;
	}

	// Update is called once per frame
	void Update() {

		//testing variable set
		bossAnimator.SetBool ("atk2", attacking2);
		bossAnimator.SetBool ("atk", attacking);
		bossAnimator.SetFloat ("speed", Mathf.Abs (bossRB.velocity.x));
		bossAnimator.SetBool("die", die);

		// set forward velocity
		facing = this.transform.localScale.x / Mathf.Abs (this.transform.localScale.x);
		bossRB.velocity = new Vector2 (-facing * speed, bossRB.velocity.y);

		// match animation speed with character speed
		if (bossRB.velocity.x != 0f && !attacking && !attacking2)
			bossAnimator.speed = speed / 8f;
		else if (attacking2 && !attacking2)
			bossAnimator.speed = speed / 50f;

		if (!attacking)
			x.enabled = false;
		else
			x.enabled = true;

		// random flip when not near player
		if (Time.time > nextFlipChance) {
			if (Random.Range(0, 10) >= 2)
				flipFacing();
			nextFlipChance = Time.time + flipTime;
		}

		// when boss dies
		if(currentHealth <=0)
		{
			die = true;
			Stop();
		}

	}

	private void Atk2()
	{
		if (enableAtk2) 
		{
			startAttack = Time.time;
			attacking2 = true;

			enableAtk2 = false;
				
				if (Time.time - startAttack > 0.25f && Time.time - startAttack < 0.8f)
					speed = 50f;
				else if (Time.time - startAttack > 0.8f && Time.time - startAttack < 2f) {
					speed = 0f;
					attacking2 = false;
				}
		}
		 

		else if (Time.time - startAttack > 10f) {
			enableAtk2 = true;
		}
	}

	private void Stop()
	{
		speed = 0;
		canFlip = false;
		bossSlider.gameObject.SetActive(false);
		Invoke("gone", 2);


	}

	private void gone()
	{
		Destroy(bossGraphic);
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "Player") {
			if (facingRight && other.transform.position.x < transform.position.x)
				flipFacing ();
			else if (!facingRight && other.transform.position.x > transform.position.x)
				flipFacing ();

			canFlip = false;

			startChargingTime = Time.time + chargeTime;
		} 

	}

	void OnTriggerStay2D(Collider2D other) {

		if (other.tag == "Player") {
			if (startChargingTime <= Time.time) {

				//attack behavior

				if (Mathf.Abs (other.transform.position.x - this.transform.position.x) >= 30.0f) {
					attacking = false;
				} else if (Mathf.Abs (other.transform.position.x - this.transform.position.x) >= 5.0f) {
					speed = 10f;
					attacking = false;
				} else {
					speed = 0f;
					attacking = true;
				}
			}
		}

	}

	void OnTriggerExit2D(Collider2D other) {

		if (other.tag == "Player") {
			canFlip = true;
			attacking = false;
			attacking2 = false;
			bossRB.velocity = new Vector2(0f, 0f);
			speed = 0f;
		}

	}

	void flipFacing() {

		if (!canFlip)
			return;
		float facingX = bossGraphic.transform.localScale.x;
		facingX *= -1f;
		transform.localScale = new Vector3(facingX, bossGraphic.transform.localScale.y, bossGraphic.transform.localScale.z);
		transform.localPosition = new Vector3 (this.transform.localPosition.x + 1.2f * -facingX, transform.localPosition.y, transform.localPosition.z);
		facingRight = !facingRight;

	}

	public void Damage(int dmg)
	{ 
		bossSlider.gameObject.SetActive (true);
		currentHealth -= dmg;
		bossSlider.value = currentHealth;
	}

}