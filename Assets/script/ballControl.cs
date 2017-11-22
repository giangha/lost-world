using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballControl : MonoBehaviour
{
       
    public float speed = 100f;
    GameObject player;
    private Rigidbody2D rb2d;
    // Use this for initialization
    void Start () {
       // player = GameObject.FindGameObjectWithTag("Player");
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

       rb2d.AddForce(Vector2.right * speed);
	}
}
	