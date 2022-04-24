using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public bool followRotation = false;
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);

        if (followRotation)
            transform.rotation = target.transform.rotation * Quaternion.Euler(0, 0, -90);
    }
}
