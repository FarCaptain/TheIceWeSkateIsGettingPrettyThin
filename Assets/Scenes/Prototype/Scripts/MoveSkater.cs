using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSkater : MonoBehaviour
{
    public float speed;
    public float turnSpeed;

    // Update is called once per frame
    void Update()
    {
        float turn = -Input.GetAxis("Horizontal");
        transform.eulerAngles += new Vector3(0,0,turn*turnSpeed);
        float rotation = Mathf.Deg2Rad*transform.eulerAngles[2];
        transform.position += new Vector3(Mathf.Cos(rotation),Mathf.Sin(rotation), 0) * speed;
    }
}
