using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_atk_trigger : MonoBehaviour {

    public int damage = 2;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("atk"))
          { other.SendMessageUpwards("Damage",damage); }

  
    }
    

}
