using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpinner : MonoBehaviour
{
    private float rotateAngle = 0.0f;
    public float turnSpeed = 1.0f;

    // Update is called once per frame
    void Update()
    {
        rotateAngle += turnSpeed;
        transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x, transform.rotation.y + rotateAngle, transform.rotation.eulerAngles.z);
    }
}
