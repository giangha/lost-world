using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeleton_atk : MonoBehaviour
{
   

    public int damage = 2;
    float attackRate = 1;
    float nextAttack = 0;
   

  

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


}
