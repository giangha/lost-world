using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mummy : MonoBehaviour {

	public float speed = 10f;
	Animator mummyAnimator;
	public int currentHealth = 4;
	public Slider mummySlider;
    public GameObject head_attack;

	//facing
	public GameObject mummyGraphic;
	bool canFlip = true;
	bool facingRight = false;
	float flipTime = 3f;
	float nextFlipChance = 0f;

	//attacking
	public float chargeTime;
	float startChargingTime;
	bool attacking;
	Rigidbody2D mummyRB;
    bool die = false;

	// Use this for initialization
	void Start() {

		mummyAnimator = GetComponent<Animator>();
		mummyRB = GetComponent<Rigidbody2D>();
		mummyGraphic = this.gameObject;

		mummySlider.maxValue = currentHealth;
		mummySlider.value = currentHealth;
	}

	// Update is called once per frame
	void Update() {
        mummyAnimator.SetBool("die", die);
		if (Time.time > nextFlipChance) {
			if (Random.Range(0, 10) >= 2)
				flipFacing();
			nextFlipChance = Time.time + flipTime;
		}

		mummyAnimator.SetFloat ("speed", Mathf.Abs (mummyRB.velocity.x));

		if(currentHealth <=0)
		{
            die = true;
            Stop();
        }

	}

    private void Stop()
    {
        speed = 0;
        canFlip = false;
        mummySlider.gameObject.SetActive(false);
        head_attack.gameObject.SetActive(false);
        //GameObject.Find("mummy_atk").GetComponent<mummy_atk>().damage = 0;
        Invoke("gone", 2);


    }

    private void gone()
    {
        Destroy(mummyGraphic);
    }

    void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "Player") {
			if (facingRight && other.transform.position.x < transform.position.x)
				flipFacing ();
			else if (!facingRight && other.transform.position.x > transform.position.x)
				flipFacing ();

			canFlip = false;
			attacking = true;

			startChargingTime = Time.time + chargeTime;
		} 

	}

	void OnTriggerStay2D(Collider2D other) {

		if (other.tag == "Player") {
			if (startChargingTime <= Time.time) {

				//attack behavior

				if (!facingRight) {
					if (Mathf.Abs (other.transform.position.x - this.transform.position.x) > 10.0f) {
						mummyRB.velocity = new Vector2 (-1, 0) * speed;
					} 

					else {
						mummyRB.velocity = new Vector2 (-1, 0.25f)*speed;
						mummyAnimator.SetBool ("atk", true);
					}
				}
				else {
					if (Mathf.Abs (other.transform.position.x - this.transform.position.x) > 10.0f) {
						mummyRB.velocity = new Vector2 (1, 0) * speed;
					} 

					else {
						mummyRB.velocity = new Vector2 (1, 0.25f)*speed;
						mummyAnimator.SetBool ("atk", true);
					}
				}

				mummyAnimator.SetFloat("speed", Mathf.Abs(mummyRB.velocity.x));  //change animation

			}
		}

	}

	void OnTriggerExit2D(Collider2D other) {

		if (other.tag == "Player") {
			canFlip = true;
			attacking = false;
			mummyRB.AddForce(new Vector2(0f, 0f));
			mummyAnimator.SetFloat("speed", Mathf.Abs(mummyRB.velocity.x));  //change animation
			mummyAnimator.SetBool("atk", attacking);
		}

	}

	void flipFacing() {

		if (!canFlip)
			return;
		float facingX = transform.localScale.x;
		facingX *= -1f;
		transform.localScale = new Vector3(facingX, mummyGraphic.transform.localScale.y, mummyGraphic.transform.localScale.z);
		facingRight = !facingRight;

	}

	public void Damage(int dmg)
	{ 
		mummySlider.gameObject.SetActive (true);
		currentHealth -= dmg;
		mummySlider.value = currentHealth;
	}

}
