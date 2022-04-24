using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSkater : MonoBehaviour
{
    public float speed;
    public float turnSpeed;
    public bool mouseControl;

    //[]
    public bool isGliding = false;
    public float glidingDuration;
    public float jumpCoolDownDuration;
    private Timer glideTimer;
    private Timer jumpCooldownTimer;

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
        if (mouseControl)
        {
            //Debug.Log("MouseLocation" + Camera.main.ScreenToWorldPoint(Input.mousePosition) + "Diamond"+ transform.position);
            Vector3 mouseVector = Camera.main.ScreenToWorldPoint(Input.mousePosition)+ new Vector3(0,0,10) - transform.position;
            float rotation = Mathf.Deg2Rad * transform.eulerAngles[2];
            Vector3 directionVector = new Vector3(Mathf.Sin(rotation), -Mathf.Cos(rotation), 0);
            float angleToMouse = Vector3.Dot(directionVector, mouseVector.normalized);

            int turn = angleToMouse > 0 ? -1 : 1;

            transform.eulerAngles += new Vector3(0, 0, turn * turnSpeed);
            rotation = Mathf.Deg2Rad * transform.eulerAngles[2];
            transform.position += new Vector3(Mathf.Cos(rotation), Mathf.Sin(rotation), 0) * speed;

            //Debug.Log("Direction:"+directionVector + "MouseVector:"+ mouseVector + "Angle to Mouse" + angleToMouse);
        }
        else
        {
            if (!isGliding)
            {
                float turn = -Input.GetAxis("Horizontal");
                transform.eulerAngles += new Vector3(0, 0, turn * turnSpeed);
            }
            float rotation = Mathf.Deg2Rad * transform.eulerAngles[2];
            transform.position += new Vector3(Mathf.Cos(rotation), Mathf.Sin(rotation), 0) * speed;

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
    }
}
