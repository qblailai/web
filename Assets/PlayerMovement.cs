using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
       Rigidbody2D rb;
       Animator anim;
       SpriteRenderer spr;
       public float speed = 5f;
       public float jump = 5f;
       float movement;
       bool isGrounded = false;
       public GameObject gameover;
       public bool isDead = false;

       int groundContacts = 0; //目前同時碰到幾塊平台，起始值為0

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            groundContacts++; //++為+1的意思

            isGrounded = true;
            //Debug.Log("碰到地板了!");
            anim.SetBool("jump", false);
            transform.SetParent(other.transform);
        } else if (other.CompareTag("Deathline"))
        {
           // Debug.Log("主角GG了");
            gameover.SetActive(true);
            isDead = true;
        }
    }
     void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            groundContacts--;
            if (groundContacts <= 0)
            {
                groundContacts = 0; //萬一算錯變成負數，強制拉回0
                isGrounded = false;
                transform.SetParent(null);
            }
            //Debug.Log("離開地板了!");  
        } 
    }

    void Update()
    {
        if (isDead && Keyboard.current.enterKey.wasPressedThisFrame) //Input.anyKeyDown
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
    void FixedUpdate()
    {
        if (Keyboard.current.rightArrowKey.isPressed){
            movement = speed; 
            //rb.linearVelocityX = 5f;
            anim.SetBool("run", true);
            spr.flipX = false;
       } else if (Keyboard.current.leftArrowKey.isPressed){
            movement = -speed;
            //rb.linearVelocityX = -5f;
            anim.SetBool("run", true);
            spr.flipX = true;
       } else {
            movement = 0f;
            //rb.linearVelocityX = 0f;
            anim.SetBool("run", false);
        }
        rb.linearVelocityX = movement;
        if ((Keyboard.current.spaceKey.isPressed) && isGrounded ==true){
            rb.linearVelocity = new Vector2(movement, jump);
            anim.SetBool("jump", true);
        }
       }
    }
