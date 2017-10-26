using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeleton_atk : MonoBehaviour
{

    public int damage = 2;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("playerGotAtk"))
        { other.SendMessageUpwards("Damage2", damage); }

    }
}
