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
    public float x;
    public float y;
    public float z;

    void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        ///load game
        /*
        x = PlayerPrefs.GetFloat("x");
        y = PlayerPrefs.GetFloat("y");
        z = PlayerPrefs.GetFloat("z");
        player.transform.position.x = x;
        player.transform.position.y = y;
        player.transform.position.z = z;
        */
        ///

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

        PlayerPrefs.SetFloat("Health", curHealth);
        PlayerPrefs.SetFloat("Speed", speed);
        PlayerPrefs.SetFloat("JumpPower", jumpPower);

        x = transform.position.x;
        PlayerPrefs.SetFloat("x", x);
        y = transform.position.y;
        PlayerPrefs.SetFloat("y", y);
        z = transform.position.z;
        PlayerPrefs.SetFloat("z", z);

    }

    public void Damage2(int dmg)
    { curHealth -= dmg; }
}
