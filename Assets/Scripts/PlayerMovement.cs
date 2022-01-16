using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float jumpheight = 15f;

    Vector3 direction;
    //......................................

    public Rigidbody rb;
    public Transform cam;
    public float speed = 80f;
    public float turnsmoothing = 0.1f;// for movement
    float turnsmoothvelocity = 0.5f;
    public float maxVelocity = 100f;
    Vector3 rbVelocity;
    //......................................
    public Transform groundcheck;
    public float grounddistance = 0.4f;// for is grounded
    public LayerMask groundmask;
    //public Collider groundedcheck;

    //......................................

    public bool isgrounded;
    Vector3 velocity;
    public Animator anim;
    bool punching;
    GameObject ground;

    void Start()
    {
        //ground = GameObject.FindGameObjectWithTag("ground");

        Cursor.lockState = CursorLockMode.Locked;

    }


    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");// uses imput to find direction
        direction = new Vector3(horizontal, 0f, vertical).normalized;



    }


    void FixedUpdate()
    {

        rbVelocity = rb.velocity;


        isgrounded = Physics.CheckSphere(groundcheck.position, grounddistance, groundmask);

        if (Input.GetKey("space") && isgrounded)
        {
            rb.AddForce(transform.up * jumpheight, ForceMode.Impulse);// here u jump
        }


        if (direction.magnitude >= 0.1f)
        {


            float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;// finds direction of movement




            if (!punching)
            {
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnsmoothvelocity, turnsmoothing);// makes it so the player faces its movement direction
                transform.rotation = Quaternion.Euler(0f, angle, 0f);// makes it so the player faces its movement direction
            }


            /*if (!isgrounded && velocity.y <= 0)
            {
                //velocity.y += -9f * Time.deltaTime;  // here you fall faster the longer you fall
                //rb.MovePosition(velocity * Time.deltaTime);
                velocity.y = -10f;
            }*/


            Vector3 movedir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward * Time.deltaTime;// here is the movement
            rb.AddForce(movedir.normalized * speed * Time.deltaTime, ForceMode.Impulse);
        }//this is doing all the movement
        else if(isgrounded == true)
        {

            Vector3 resultVelocity = rb.velocity;
            resultVelocity.z = 0;
            resultVelocity.x = 0;
            rb.velocity = resultVelocity;



        }

        float horizontalvelocity = Vector2.Dot(rb.velocity, Vector2.right);

        if (rbVelocity.sqrMagnitude > maxVelocity)// right alt and shift for||||
        {
            Vector3 endVelocity = rb.velocity;
            //limiting the velocity yes
            endVelocity.z *= 0.9f;
            endVelocity.x *= 0.9f;
            rb.velocity = endVelocity;
            
        }




    }

}
