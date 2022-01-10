using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyController : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed;
    private float moveInput;
    private float moveInputVertical;
    private float holdToTransform = 0;



    KeyCode transformToPerson = KeyCode.I;

    private Animator anim;

    public SpriteSwitch i;

    public GameObject person;

    //particle systems 
    public GameObject onhold;
    //timing for on hold transform particle system 
    private bool instantiated = false;


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        onhold.SetActive(false);

    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        moveInputVertical = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(moveInput * speed, moveInputVertical *speed);

    }

    private void Update()
    {
        //transform to person
        if (Input.GetKey(transformToPerson))
        {
            holdToTransform += Time.deltaTime;

            //moving particle to plaer position
            onhold.transform.position = this.transform.position;

            //instantiate a particle effect here along with new animation.
            if (instantiated == false)
            {
                onhold.SetActive(true);
                instantiated = true;
            }
        }
        if (Input.GetKey(transformToPerson) && holdToTransform > 2)
        {
            person.transform.position = this.transform.position;
            i.SwitchForm();
            holdToTransform = 0;
            instantiated = false;
            onhold.SetActive(false);
        }
        if (Input.GetKeyUp(transformToPerson) && holdToTransform < 2)
        {
            holdToTransform = 0;
            instantiated = false;
            onhold.SetActive(false);
        }
       


        //horizontal movement
        if (moveInput == 0)
        {
            
        }
        else if (moveInput > 0)
        {

            //flip
            transform.eulerAngles = new Vector3(0, 0, 0);

        }
        else if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);

        }

    }

}
