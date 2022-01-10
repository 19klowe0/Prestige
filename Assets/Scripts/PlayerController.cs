using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    public float jumpForce;
    public float speed;
    private float moveInput;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    public float fallingThreshold;

    KeyCode transformToButterfly = KeyCode.I;
    private float holdToTransform = 0;


    private Animator anim;
    public SpriteSwitch i;
    public GameObject butterfly;

    //particle systems 
    public GameObject onhold;
    

    //timing for on hold transform particle system 
    private bool instantiated = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        anim.SetBool("facingRight", true);
        onhold.SetActive(false);

    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        
    }

    private void Update()
    {
        
        //transform to butterfly 
        if (Input.GetKey(transformToButterfly))
        {
            holdToTransform += Time.deltaTime;

            //moving particle to plaer position
            onhold.transform.position = this.transform.position;

            //instantiate a particle effect here along with new animation.
            if (instantiated == false)
            {
                //Instantiate(onhold, transform.position, Quaternion.identity);
                onhold.SetActive(true);
                instantiated = true;
                
            }
            
        }
        if (Input.GetKey(transformToButterfly) && holdToTransform > 2)
        {
            butterfly.transform.position = this.transform.position;
            
            i.SwitchForm();
            holdToTransform = 0;
            instantiated = false;
            onhold.SetActive(false);

            
        }
        if (Input.GetKeyUp(transformToButterfly) && holdToTransform < 2)
        {
            holdToTransform = 0;
            instantiated = false;
            onhold.SetActive(false);
        }


        isGrounded = Grounded();


        if (isGrounded == true && Input.GetKeyDown(KeyCode.W))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
            anim.SetTrigger("takeOff");

        }

        if (Input.GetKey(KeyCode.W) && isJumping == true)
        {
            anim.SetBool("isJumping", true);
            if (jumpTimeCounter > 0)
            {
                
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
                
            }
            else 
            {
                anim.SetBool("isFalling", true);
                anim.SetBool("isJumping", false);
                isJumping = false;
            }

            //never releases the w key
            if (rb.velocity.y < 0)
            {
                anim.SetBool("isFalling", true);
                anim.SetBool("isJumping", false);
            }

        }

        //release W
        if (Input.GetKeyUp(KeyCode.W))
        {
            isJumping = false;
            anim.SetBool("isFalling", true);
            anim.SetBool("isJumping", false);
        }
        //check to see if player has returned to ground
        if (isGrounded == true)
        {
            anim.SetBool("isFalling", false);

        }
        //check to see if falling from not a jump
        if (rb.velocity.y < fallingThreshold)
        {
            anim.SetBool("isFalling", true);
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isFalling", false);

        }


        //horizontal movement
        if (moveInput == 0)
        {

            anim.SetBool("isRunning", false);

        }
        else if (moveInput > 0)
        {
            anim.SetBool("isRunning", true);
            anim.SetBool("facingDown", false);
            anim.SetBool("facingRight", true);
            anim.SetBool("facingLeft", false);
            //flip
            transform.eulerAngles = new Vector3(0, 0, 0);

        }
        else if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            anim.SetBool("isRunning", true);
            anim.SetBool("facingDown", false);
            anim.SetBool("facingRight", false);
            anim.SetBool("facingLeft", true);


        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            anim.SetBool("facingDown", true);
        }

    }

    public bool Grounded()
    {
        return Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
    }
}
