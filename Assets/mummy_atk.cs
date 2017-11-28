using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mummy_atk : MonoBehaviour {


 
    public int damage = 5;

    float attackRate = 1;
    float nextAttack = 0;
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("playerGotAtk"))
        {
            other.SendMessageUpwards("Damage2", damage);
            nextAttack = Time.time + attackRate;
        }


    }
    void OnTriggerStay2D(Collider2D other)
    {

        if (other.CompareTag("playerGotAtk"))
        {
            if (Time.time > nextAttack)
            {
                other.SendMessageUpwards("Damage2", damage);
                nextAttack = Time.time + attackRate;
            }
        }

    }
    void OnTriggerExit2D(Collider2D other)
    {

        //reset counter

    }
}


