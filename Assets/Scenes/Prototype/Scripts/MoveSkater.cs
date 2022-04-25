using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSkater : MonoBehaviour
{
    public float speed;
    public float turnSpeed;

    //[]
    public bool isGliding = false;
    public float glidingDuration;
    public float jumpCoolDownDuration;
    private Timer glideTimer;
    private Timer jumpCooldownTimer;

    // physics movement
    public bool canSlide = false;
    public float slideFactor;

    //Julian
    public bool hasFell = false;
    public float speedDif;
    private float dynamSpeed;

    private Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        glideTimer = gameObject.AddComponent<Timer>();
        jumpCooldownTimer = gameObject.AddComponent<Timer>();
        glideTimer.MaxTime = glidingDuration;
        jumpCooldownTimer.MaxTime = jumpCoolDownDuration;
        glideTimer.TimerStart = false;
        jumpCooldownTimer.TimerStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Stops Skater when they fall
        if (hasFell)
            dynamSpeed = 0;

        if (!isGliding&&!hasFell)
        {
            float speedUP = Input.GetAxis("Vertical");
            dynamSpeed = speed + (speedUP * speedDif) * Time.deltaTime;
            //Move(new Vector3(dynamSpeed, 0f, 0f));


            float turn = -Input.GetAxis("Horizontal");
            transform.eulerAngles += new Vector3(0, 0, turn * turnSpeed * Time.deltaTime);
        }

        float rotation = Mathf.Deg2Rad * transform.eulerAngles[2];
        Move(new Vector3(Mathf.Cos(rotation) * dynamSpeed, Mathf.Sin(rotation) * dynamSpeed, 0) );
        //transform.position += new Vector3(Mathf.Cos(rotation), Mathf.Sin(rotation), 0) * dynamSpeed;

        
        float jump = Input.GetAxis("Jump");
        Animator SkaterAnimator = gameObject.GetComponentInChildren<Animator>();

        if (!isGliding && jump != 0f)

        {
            SkaterAnimator.SetBool("JumpTrigger", true);
            //glide for a while, cannot use arrow keys
            isGliding = true;
            glideTimer.TimerStart = true;
        }

        if(isGliding && glideTimer.TimerStart == false)
        {
            //if (jumpCooldownTimer.TimerStart == false)
            //{
            //    jumpCooldownTimer.TimerStart = true;
            //}
            //else
            {
                glideTimer.ResetTimer();
                SkaterAnimator.SetBool("JumpTrigger", false);
                isGliding = false;
            }
        }
    }

    private void Move(Vector3 motion)
    {
        if (canSlide)
            rigidBody.AddForce(motion * slideFactor);
        else
            rigidBody.velocity = motion;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Vector2 vel = rigidBody.velocity;
    //    //vel.Normalize();

    //    rigidBody.velocity *= -1;

    //    //GetComponent<Rigidbody2D>().AddForce(-vel * 1000f);
    //    //print("int");
    //}
}
