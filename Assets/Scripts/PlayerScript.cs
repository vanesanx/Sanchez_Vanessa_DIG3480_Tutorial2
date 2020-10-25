using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;
    
    public Text winText;

    public Text lives;
    private bool facingRight = true;

    private int livesValue = 3;

    private int scoreValue = 0;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioSource musicSource;

    Animator anim;
    
    public bool isGrounded;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask WhatIsGround;

    // Start is called before the first frame update
    void Start()
    {
        
        rd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        score.text = scoreValue.ToString();
        winText.text = "";
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    
    {
        isGrounded = Physics2D.OverlapCircle (groundcheck.position, checkRadius,WhatIsGround);
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }

         if (Input.GetKeyDown(KeyCode.A))
        {

          anim.SetInteger("State", 1);

        }

     if (Input.GetKeyUp(KeyCode.A))
        {


          anim.SetInteger("State", 0);

        }

        if (Input.GetKeyDown(KeyCode.D))
        {

          anim.SetInteger("State", 1);

        }

        if (Input.GetKeyUp(KeyCode.D))
        {


          anim.SetInteger("State", 0);

        }

         if (Input.GetKeyDown(KeyCode.W))
        {

          anim.SetInteger("State", 2);

         }

     if (Input.GetKeyUp(KeyCode.W))
        {

          anim.SetInteger("State", 0);

        }
        
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);

            if (scoreValue == 4)
            {
                transform.position = new Vector3(41.0f, 173.0f, 0.0f);
                livesValue = 3;
                lives.text = livesValue.ToString();
            }

            if (scoreValue == 8)
            {
                winText.text = "You win! Game made by Vanessa Sanchez";
                musicSource.clip = musicClipTwo;
                musicSource.Play();
            }

        }
        
        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = livesValue.ToString();
            Destroy(collision.collider.gameObject);
            
            
            
            if(livesValue == 0)
            {
                winText.text = "You lose! By Vanessa Sanchez";
                Destroy (this.gameObject);
            }
            
        }    


    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
            if (Input.GetKey("escape"))
            {
                Application.Quit();
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    private void JumpAction()
    {
        rd2d.AddForce(transform.up * 600f);
        anim.SetTrigger( "Jump" );
    }
}