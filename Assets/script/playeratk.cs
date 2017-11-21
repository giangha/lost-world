using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playeratk : MonoBehaviour
{

    private bool attacking = false;
    private bool specialAtk = false;
    public Collider2D atkTrigger;
    private Animator amin;
    private float attackTimer = 0;
    public float attackCd = .3f;
    public float specialAtkCD = 1.9f;

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
            AudioSource splash = GetComponent<AudioSource>();
            splash.Play();
         
            
        }
        
        

        if (Input.GetKeyDown("x") && !specialAtk)
        {
            specialAtk = true;
            attackTimer = specialAtkCD;
            atkTrigger.enabled = true;
                    }

        if (attacking)
        { if(attackTimer>0)
            { attackTimer -= Time.deltaTime; }
            else
            { attacking = false;
                atkTrigger.enabled = false;
                  
            }
        }

        if (specialAtk)
        {
            if (attackTimer > 0)
            { attackTimer -= Time.deltaTime; }
            else
            {
                specialAtk = false;
                atkTrigger.enabled = false;
            }
        }

        amin.SetBool("atking", attacking);
        amin.SetBool("specialAtk",specialAtk);

    }
}
