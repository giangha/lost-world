using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {
    public float speed = 50f;
    public float jumpPower = 150f;
    public float maxspeed = 3;
    public int curHealth;
  //  public float jumpPower = 150f;
    public bool grounded;
    private Rigidbody2D rb2d;
    private Animator anim;
	void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
	}
     void Update()
    {       
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        // direction facing
        if(Input.GetAxis("Horizontal")< -0.1f)
        { transform.localScale = new Vector3(-1, 1, 1); }

        if (Input.GetAxis("Horizontal") > 0.1f)
        { transform.localScale = new Vector3(1, 1, 1); }

        if(Input.GetButtonDown("Jump"))
        { rb2d.AddForce(Vector2.up * jumpPower); }

        if(curHealth<=0)
        { Destroy(gameObject); }
    }
    // Update is called once per frame
    void FixedUpdate () {
        float movementHorizontal = Input.GetAxis("Horizontal");
       
        rb2d.AddForce((Vector2.right * speed)*movementHorizontal);

        if(rb2d.velocity.x > maxspeed)
        { rb2d.velocity = new Vector2(maxspeed, rb2d.velocity.y); }

        if (rb2d.velocity.x < -maxspeed)
        { rb2d.velocity = new Vector2(-maxspeed, rb2d.velocity.y); }
    }

    public void Damage2(int dmg)
    { curHealth -= dmg; }
}
