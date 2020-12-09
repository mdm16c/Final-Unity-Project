using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private RobotFreeAnim rs;
    Vector3 rot = Vector3.zero;
	public float rotSpeed = 40.0f;
	public float moveSpeed = 1.0f;
	public float revForce = 0.0f;
	public float maxRev = 100f;
    public float revForceDelta = 50.0f;
	public bool canWalk = true;
	public bool canRev = false;
	private Rigidbody rb;
    public Animator anim;
	public ParticleSystem part;

	// Use this for initialization
	void Awake()
	{
        rs = GameObject.Find("robotSphere").GetComponent<RobotFreeAnim>();
		anim = GameObject.Find("robotSphere").GetComponent<Animator>();
		part = GameObject.Find("robotSphere").GetComponent<ParticleSystem>();
		gameObject.transform.eulerAngles = rot;
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		CheckKey();
		gameObject.transform.eulerAngles = rot;
	}

	void CheckKey()
	{
		if (anim.GetCurrentAnimatorStateInfo(0).IsName("anim_Idle_Loop_S") || anim.GetCurrentAnimatorStateInfo(0).IsName("anim_Walk_Loop"))
        {
            canWalk = true;
        }
		else {
			canWalk = false;
		}

		if(anim.GetCurrentAnimatorStateInfo(0).IsName("closed_Roll_Loop"))
		{
			canRev = true;
		}
		else {
			canRev = false;
		}

		// Walk
		if (Input.GetKey(KeyCode.W))
		{
			anim.SetBool("Walk_Anim", true);

            if (canWalk) {
                transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
            }
		}
		else if (Input.GetKeyUp(KeyCode.W))
		{
			anim.SetBool("Walk_Anim", false);
		}

		// Rotate Left
		if (Input.GetKey(KeyCode.A))
		{
			rot[1] -= rotSpeed * Time.fixedDeltaTime;
		}

		// Rotate Right
		if (Input.GetKey(KeyCode.D))
		{
			rot[1] += rotSpeed * Time.fixedDeltaTime;
		}

		// Roll
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (!anim.GetBool("Roll_Anim"))
			{
				anim.SetBool("Roll_Anim", true);
			}
		}

		if(Input.GetKey(KeyCode.Space))
		{
			if(revForce < maxRev && canRev){
				revForce += revForceDelta * Time.deltaTime;
				part.Play();
			}
		}

		if(Input.GetKeyUp(KeyCode.Space))
		{
			// if (anim.GetBool("Roll_Anim"))
			// {
			// 	anim.SetBool("Roll_Anim", false);
			// }
			part.Stop();
			rb.AddForce(transform.forward * revForce, ForceMode.VelocityChange);
			revForce = 0.0f;
		}

		// Close
		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			if (!anim.GetBool("Open_Anim"))
			{
				anim.SetBool("Open_Anim", true);
			}
			else
			{
				anim.SetBool("Open_Anim", false);
			}
		}
	}
}
