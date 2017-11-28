using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class player : MonoBehaviour {


    public float damage = 2;
    public float speed = 50f;
    public float jumpPower = 150f;
    public float maxspeed = 3;
    public int curHealth = 100;
  //  public float jumpPower = 150f;
    public bool grounded;
    private Rigidbody2D rb2d;
    private Animator anim;
    public float x;
    public float y;
    public float z;
    public static bool load_settings = false;
    bool die = false;
    

	//HUD Variables
	public Slider playerHealthSlider;

    void Start () {
        curHealth = 100;
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();

		playerHealthSlider.maxValue = curHealth;
		playerHealthSlider.value = curHealth;
        GameObject.Find("attackTrigger").GetComponent<player_atk_trigger>().damage = damage;
        ///load game
        if (load_settings)
        {
            x = PlayerPrefs.GetFloat("x");
            y = PlayerPrefs.GetFloat("y");
            z = PlayerPrefs.GetFloat("z");
            Vector3 starting_position = new Vector3(x, y, z);
            transform.position = starting_position;
            curHealth = PlayerPrefs.GetInt("Health", 100);
            speed = PlayerPrefs.GetFloat("Speed", 0);
            jumpPower = PlayerPrefs.GetFloat("JumpPower", 0);
            GameObject.Find("attackTrigger").GetComponent<player_atk_trigger>().damage = damage;
        }
        ///

    }
     void Update()
    {       
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        anim.SetBool("die", die);
        // direction facing
        if(Input.GetAxis("Horizontal")< -0.1f)
        { transform.localScale = new Vector3(-1, 1, 1); }

        if (Input.GetAxis("Horizontal") > 0.1f)
        { transform.localScale = new Vector3(1, 1, 1); }

        if(Input.GetButtonDown("Jump"))
        { rb2d.AddForce(Vector2.up * jumpPower);
            


        }

        if(curHealth<=0)
        {
            die = true;
            Invoke("Dying",3);
             }

        int current_scene_index = SceneManager.GetActiveScene().buildIndex;
        if (transform.position.x>=96 & transform.position.y<-60 & current_scene_index==4)
        {
            SceneManager.LoadScene(5);
        }
        if (transform.position.x <= -72 & transform.position.y < -2 & current_scene_index == 3)
        {
            SceneManager.LoadScene(4);
        }



    }

    void Dying()
    {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void FixedUpdate () {
        float movementHorizontal = Input.GetAxis("Horizontal");
       
        rb2d.AddForce((Vector2.right * speed)*movementHorizontal);

        if(rb2d.velocity.x > maxspeed)
        { rb2d.velocity = new Vector2(maxspeed, rb2d.velocity.y); }

        if (rb2d.velocity.x < -maxspeed)
        { rb2d.velocity = new Vector2(-maxspeed, rb2d.velocity.y); }


        PlayerPrefs.SetInt("Health", curHealth);
        PlayerPrefs.SetFloat("Speed", speed);
        PlayerPrefs.SetFloat("JumpPower", jumpPower);

        x = transform.position.x;
        PlayerPrefs.SetFloat("x", x);
        y = transform.position.y;
        PlayerPrefs.SetFloat("y", y);
        z = transform.position.z;
        PlayerPrefs.SetFloat("z", z);
        PlayerPrefs.Save();

        if (transform.position.y < -75) curHealth = 0;

    }

    public void Damage2(int dmg)
    {
       
        curHealth -= dmg; 
		playerHealthSlider.value = curHealth;
	}


    static public void Set_continue_function(bool _load_settings)
    {
        load_settings = _load_settings;
   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("spike"))
        {
            curHealth -= 5;
            playerHealthSlider.value = curHealth;
            //transform.localScale = new Vector3(1, -5, 1);
            // rb2d.AddForce(Vector2.down * -100);

        }
           
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
        if (other.gameObject.CompareTag("shoe"))
        {
            other.gameObject.SetActive(false);
            //rb2d.AddForce (rb2d.velocity*3);
            speed += 50;
        }

        if (other.gameObject.CompareTag("heart"))
        {
            other.gameObject.SetActive(false);
            
            curHealth += 10;
            playerHealthSlider.value = curHealth;
        }

        if (other.gameObject.CompareTag("sword"))
        {
            other.gameObject.SetActive(false);
          
           damage += 1;
            //   GameObject thePlayer = GameObject.Find("attackTrigger");
            //  PlayerScript thePlayer = thePlayer.GetComponent<player_atk_trigger>();

            GameObject.Find("attackTrigger").GetComponent<player_atk_trigger>().damage = damage;
            // attacktrigger.s
        }

     
    }



}
