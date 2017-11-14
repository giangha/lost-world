using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_atk_trigger : MonoBehaviour {

    public GameObject Player;

    void OnTriggerEnter2D(Collider2D other)
    {

        float damage = 2;
        if (other.CompareTag("atk"))
          { other.SendMessageUpwards("Damage",damage); }

  
    }
    

}
