using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpPadForce = 500.0f;
    public AudioClip jumpPadSound;
    private AudioSource chargeSource;
    void Start() {
        chargeSource = GameObject.Find("RobotSphereParent").GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPadForce);
            chargeSource.PlayOneShot(jumpPadSound);
        }
    }
}
