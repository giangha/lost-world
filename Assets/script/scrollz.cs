using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class scrollz : MonoBehaviour {
    public float scrollSpeed = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.position;
        Vector3 localVectorUp = transform.TransformDirection (1,0,0);
        
        pos += localVectorUp * scrollSpeed * Time.deltaTime;
        transform.position = pos;
		
	}
}
