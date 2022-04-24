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

    //Julian
    public bool hasFell = false;
    public float speedDif;
    private float dynamSpeed;

    void Start()
    {
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

            float turn = -Input.GetAxis("Horizontal");
            transform.eulerAngles += new Vector3(0, 0, turn * turnSpeed * Time.deltaTime);
            //Speed variation
            float speedUP = Input.GetAxis("Vertical");
            dynamSpeed = speed + (speedUP * speedDif);
            }

            float rotation = Mathf.Deg2Rad * transform.eulerAngles[2];
            transform.position += new Vector3(Mathf.Cos(rotation), Mathf.Sin(rotation), 0) * dynamSpeed * Time.deltaTime;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(-10f, 0));
        print("int");
    }
}
