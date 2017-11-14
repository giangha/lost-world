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

	//HUD Variables
	public Slider playerHealthSlider;

    void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();

		playerHealthSlider.maxValue = curHealth;
		playerHealthSlider.value = curHealth;
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
        }
        ///

    }
     void Update()
    {       
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        // direction facing
        if(Input.GetAxis("Horizontal")< -0.1f)
        { transform.localScale = new Vector3(-1, 1, 1); }

        if (Input.GetAxis("Horizontal") > 0.1f)
        { transform.localScale = new Vector3(1, 1, 1); }

        if(Input.GetButtonDown("Jump"))
        { rb2d.AddForce(Vector2.up * jumpPower); }

        if(curHealth<=0)
        { SceneManager.LoadScene(1); }

        float inner_x = 96;
      //  float outer_x = 106;
        y = -60;
        if(transform.position.x>=inner_x & transform.position.y<y)
        {
            SceneManager.LoadScene(3);
        }



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
        }

        if (other.gameObject.CompareTag("sword"))
        {
            other.gameObject.SetActive(false);
          
           damage += 10;
        }

     
    }

    public float get_attack_stat()
    {
        return damage;
    }

}
