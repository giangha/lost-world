using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playeratk : MonoBehaviour
{

    private bool attacking = false;
    public Collider2D atkTrigger;
    private Animator amin;
    private float attackTimer = 0;
    public float attackCd = .3f;

    void Awake()
    {
        amin = gameObject.GetComponent<Animator>();
        atkTrigger.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("z") && !attacking)
        {
            attacking = true;
            attackTimer = attackCd;
            atkTrigger.enabled = true;
        }

        if(attacking)
        { if(attackTimer>0)
            { attackTimer -= Time.deltaTime; }
            else
            { attacking = false;
                atkTrigger.enabled = false;
            }
        }
        amin.SetBool("atking", attacking);

    }
}
