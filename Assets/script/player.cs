﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {
    public float speed = 50f;
    public float jumpPower = 150f;

    public bool grounded;
    private Rigidbody2D rb2d;
	void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
	}
     void Update()
    {
        
    }
    // Update is called once per frame
    void FixedUpdate () {
        float movementHorizontal = Input.GetAxis("Horizontal");
       
        rb2d.AddForce((Vector2.right * speed)*movementHorizontal);
	}
}
