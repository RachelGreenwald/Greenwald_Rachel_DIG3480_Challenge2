using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    

    public float speed;
    public float jumpForce;
    public Text countText;
    public Text winText;
    public Text livesText;
    public Text loseText;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;

    private Rigidbody2D rb2d;
    private int count;
    private int lives;
    private bool facingRight = true;
    Animator anim;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        lives = 3;
        winText.text = "";
        loseText.text = "";
        SetCountText();
        SetLivesText();
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            musicSource.clip = musicClipOne;
            musicSource.Play();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            anim.SetInteger("WORK", 2);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            anim.SetInteger("WORK", 0);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            anim.SetInteger("WORK", 2);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            anim.SetInteger("WORK", 0);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            anim.SetInteger("WORK", 1);
        }
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0);
        rb2d.AddForce(movement * speed);
        if (Input.GetKey("escape"))
            Application.Quit();

        if (facingRight == false && moveHorizontal > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveHorizontal < 0)
        {
            Flip();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
           other.gameObject.SetActive (false);
            count = count + 1;
            SetCountText();
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            lives = lives + -1;
            SetLivesText();
        }

        if (count == 4)
        {
            transform.position = new Vector3(-30,-10);
            lives = 3;
            SetLivesText();
        }

         


    }

    void SetCountText ()
    {
        countText.text = "Score:" + count.ToString();
        if (count >= 8)
        {
            winText.text = "You Win!";
            musicSource.Stop();
            musicSource.clip = musicClipTwo;
            musicSource.Play();

        }
        
    }

    void SetLivesText ()
    {
        livesText.text = "Lives:" + lives.ToString();
        if (lives <= 0)
        {
            loseText.text = "You Lose!";
            Destroy(this);
        }

    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground") {


            if (Input.GetKey(KeyCode.UpArrow)) {

                rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
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
}
