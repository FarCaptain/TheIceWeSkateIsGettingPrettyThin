using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSkater : MonoBehaviour
{
    public float speed;
    public float turnSpeed;
    public bool mouseControl;

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
            float turn = -Input.GetAxis("Horizontal");
            transform.eulerAngles += new Vector3(0, 0, turn * turnSpeed);
            float rotation = Mathf.Deg2Rad * transform.eulerAngles[2];
            transform.position += new Vector3(Mathf.Cos(rotation), Mathf.Sin(rotation), 0) * speed;
        }
    }
}
