using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skeleton : MonoBehaviour {

    public float speed = 50f;
    Animator skeletonAnimator;
    public int currentHealth = 4;
	public Slider skeletonSlider;

    //facing
    public GameObject skeletonGraphic;
    bool canFlip = true;
    bool facingRight = false;
    float flipTime = 3f;
    float nextFlipChance = 0f;

    //attacking
    public float chargeTime;
    float startChargingTime;
    bool charging;
    Rigidbody2D skeletonRB;

    // Use this for initialization
    void Start() {

        skeletonAnimator = GetComponent<Animator>();
        skeletonRB = GetComponent<Rigidbody2D>();
        skeletonGraphic = this.gameObject;

		skeletonSlider.maxValue = currentHealth;
		skeletonSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update() {

        if (Time.time > nextFlipChance) {
            if (Random.Range(0, 10) >= 4)
                flipFacing();
            nextFlipChance = Time.time + flipTime;
        }

        if(currentHealth <=0)
        { Destroy(gameObject); }

    }

    void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "Player") {
            if (facingRight && other.transform.position.x < transform.position.x)
                flipFacing();
            else if (!facingRight && other.transform.position.x > transform.position.x)
                flipFacing();

            canFlip = false;
            charging = true;

            startChargingTime = Time.time + chargeTime;
        }

    }

    void OnTriggerStay2D(Collider2D other) {

        if (other.tag == "Player") {
            if (startChargingTime <= Time.time) {
                if (!facingRight)
                    skeletonRB.AddForce(new Vector2(-1, 0) * speed);
                else
                    skeletonRB.AddForce(new Vector2(1, 0) * speed);
                skeletonAnimator.SetBool("atking", charging);  //change animation

            }
        }

    }

    void OnTriggerExit2D(Collider2D other) {

        if (other.tag == "Player") {
            canFlip = true;
            charging = false;
            skeletonRB.velocity = new Vector2(0f, 0f);
            skeletonAnimator.SetBool("atking", charging);
        }

    }

    void flipFacing() {

        if (!canFlip)
            return;
        float facingX = transform.localScale.x;
        facingX *= -1f;
        transform.localScale = new Vector3(facingX, skeletonGraphic.transform.localScale.y, skeletonGraphic.transform.localScale.z);
        facingRight = !facingRight;

    }

    public void Damage(int dmg)
    { 
		skeletonSlider.gameObject.SetActive (true);
		currentHealth -= dmg;
		skeletonSlider.value = currentHealth;
	}


}
