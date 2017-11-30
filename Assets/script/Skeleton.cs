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
    public GameObject sword;
   // public Collider2D atkTrigger;

    //animation
    private float attackTimer = 1;
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

    BoxCollider2D m_ObjectCollider;

    private void Awake()
    {
        //atkTrigger.enabled = false;
    }


    // Use this for initialization
    void Start() {
        m_ObjectCollider = GetComponent<BoxCollider2D>();
        skeletonAnimator = GetComponent<Animator>();
        skeletonRB = GetComponent<Rigidbody2D>();
        skeletonGraphic = this.gameObject;

		skeletonSlider.maxValue = currentHealth;
		skeletonSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update() {

      

        skeletonAnimator.SetFloat("speed", Mathf.Abs(skeletonRB.velocity.x));
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
            m_ObjectCollider.isTrigger = true;
            m_ObjectCollider.enabled = false;
            Stop();

        
      
            }

        
    }

    private void Stop()
    {
        speed = 0;
        canFlip = false;

        sword.gameObject.SetActive(false);
        skeletonSlider.gameObject.SetActive(false);
        //GameObject.Find("skeleton_atk").GetComponent<skeleton_atk>().damage = 0;
        Invoke("gone", 5);


    }

    private void gone()
    {
        Destroy(skeletonGraphic);
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
            
            if (startChargingTime <= Time.time)
            {

                if (!facingRight)
                {
                    if (Mathf.Abs(other.transform.position.x - this.transform.position.x) > 5f)
                    {
                        skeletonRB.AddForce(new Vector2(-.3f, 0) * speed);
                        atk = false;
                    }
                    else
                        atk = true;
                }
                else
                    if (Mathf.Abs(other.transform.position.x - this.transform.position.x) > 5f)
                {
                    skeletonRB.AddForce(new Vector2(.3f, 0) * speed);
                    atk = false;
                }
                else
                    atk = true;



                    //atk = false;
                    // skeletonRB.MovePosition(new Vector2(other.transform.position.x, other.transform.position.y));

                    // target = GameObject.FindWithTag("Player").transform;
                    //skeletonRB.MovePosition(transform.position - target.position * speed);
                    //skeletonRB.velocity = new Vector3(-speed, 0, 0);

              
                //skeletonRB.MovePosition(new Vector2(other.transform.position.x, other.transform.position.y));
                
            //skeletonRB.MovePosition(transform.position + target.position * speed);
              // skeletonRB.velocity = new Vector3(speed, 0, 0);
            //skeletonAnimator.SetBool("atking", charging);  //change animation
               // skeletonAnimator.SetFloat("speed", speed);
            }
        }
    
    }

    

    void OnTriggerExit2D(Collider2D other) {
        atk = false;
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
