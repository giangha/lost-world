﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossattacksound : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {



			AudioSource special = GetComponent<AudioSource>();
			special.Play();
	

	}
}


