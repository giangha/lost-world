using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayonSpaceBar : MonoBehaviour {
    public AudioSource jumpsound;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            jumpsound.Play();
        }
    }
}
