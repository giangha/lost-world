using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_atk_trigger : MonoBehaviour {

    public GameObject Player;

    public float damage;
    void OnTriggerEnter2D(Collider2D other)
    {
        
        
        if (other.CompareTag("atk"))
          { other.SendMessageUpwards("Damage",damage); }

  
    }
   
}
