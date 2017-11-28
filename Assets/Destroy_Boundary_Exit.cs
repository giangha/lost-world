using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Boundary_Exit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionExit2D(Collision2D other)
    {
        Destroy(other.gameObject);
    }
}
