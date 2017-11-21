using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skeleton : MonoBehaviour {
    public Transform target;
    public float speed = 50f;
    private Animator skeletonAnimator;
    public int currentHealth = 4;
	public Slider skeletonSlider;
    public Collider2D atkTrigger;

    //animation
    private float attackTimer = 0;
    public float attackCd = .3f;
    private bool attacking = false;

    //facing
    public GameObject skeletonGraphic;
    bool canFlip = true;
    bool facingRight = false;
    float flipTime = 1f;
    float nextFlipChance = 0f;

    //attacking
    public float chargeTime;
    float startChargingTime;
    bool charging;
    Rigidbody2D skeletonRB;
    bool die;
    bool atk;

  


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
        skeletonAnimator.SetBool("die", die);
        skeletonAnimator.SetBool("atking", atk);
        if (Time.time > nextFlipChance) {
            if (UnityEngine.Random.Range(0, 10) >= 2)
                flipFacing();
            nextFlipChance = Time.time + flipTime;
        }

        if(currentHealth <=0)
        {
            AudioSource die2 = GetComponent<AudioSource>();
            die2.Play();
           
            die = true;

            Stop();

        
      
            }

    }

    private void Stop()
    {
        speed = 0;
        canFlip = false;
        skeletonSlider.gameObject.SetActive(false);
        
        //skeletonRB.isKinematic = true;
      
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
            atk = true;
            if (startChargingTime <= Time.time)
            {
              if (!facingRight)
               {
                    skeletonAnimator.SetFloat("speed", speed);

                    skeletonRB.AddForce(new Vector2(-1, 0) * speed);
                    // target = GameObject.FindWithTag("Player").transform;
                    //skeletonRB.MovePosition(transform.position - target.position * speed);
                    //skeletonRB.velocity = new Vector3(-speed, 0, 0);
                    
            }
            else
                skeletonRB.AddForce(new Vector2(1, 0) * speed);
            //skeletonRB.MovePosition(transform.position + target.position * speed);
              // skeletonRB.velocity = new Vector3(speed, 0, 0);
            //skeletonAnimator.SetBool("atking", charging);  //change animation
                skeletonAnimator.SetFloat("speed", speed);
            }
        }
    
    }

    

    void OnTriggerExit2D(Collider2D other) {

        if (other.tag == "Player") {
            canFlip = true;
            charging = false;
            skeletonRB.velocity = new Vector2(0f, 0f);
            
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
