using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {
    public float speed = 50f;
    public float jumpPower = 150f;

    public bool grounded;
    private Rigidbody2D rb2d;
    private Animator anim;
	void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
	}
     void Update()
    {       
        anim.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
        // direction facing
        if(Input.GetAxis("Horizontal")< -0.1f)
        { transform.localScale = new Vector3(-1, 1, 1); }

        if (Input.GetAxis("Horizontal") > 0.1f)
        { transform.localScale = new Vector3(1, 1, 1); }
    }
    // Update is called once per frame
    void FixedUpdate () {
        float movementHorizontal = Input.GetAxis("Horizontal");
       
        rb2d.AddForce((Vector2.right * speed)*movementHorizontal);
	}
}
