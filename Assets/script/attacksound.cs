using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attacksound : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("x"))
        {
            AudioSource special = GetComponent<AudioSource>();
            special.Play();
        }
		
	}
}
