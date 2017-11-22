using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playeratk : MonoBehaviour
{
    
    private bool attacking = false;
    private bool specialAtk = false;
    public Collider2D atkTrigger;
    public Collider2D atkTrigger2;
    private Animator amin;
    private float attackTimer = 0;
    public float attackCd = .3f;
    public float specialAtkCD = 1.9f;
    

    void Awake()
    {
        amin = gameObject.GetComponent<Animator>();
        atkTrigger.enabled = false;
        atkTrigger2.enabled = false;
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
            Invoke("delaySpecialAtk", 1.9f);
         
          
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
               
            }
        }

        amin.SetBool("atking", attacking);
        amin.SetBool("specialAtk",specialAtk);

    }

    void delaySpecialAtk()
    {   
        atkTrigger2.enabled = true;
        Invoke("delayDmg", .3f);
    }

    void delayDmg()
    {
        atkTrigger2.enabled = false;
    }
   
}
