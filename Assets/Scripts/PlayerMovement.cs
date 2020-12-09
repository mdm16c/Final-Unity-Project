using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private RobotFreeAnim rs;
    Vector3 rot;
	public float rotSpeed = 30.0f;
	public float moveSpeed = 1.0f;
	public float brakeSpeed = 0.95f;
	public float revForce = 0.0f;
	public float maxRev = 100f;
    public float revForceDelta = 50.0f;
	public bool canWalk = true;
	public bool canRev = false;
	public bool isMoving = false;
	public bool canCheck = false;
	private Rigidbody rb;
    public Animator anim;
	public ParticleSystem part;
	public AudioClip charge;
	public AudioClip roll;
	public AudioClip collisionSound;
	public AudioClip chargeRelease;
	private AudioSource chargeSource;
	private AudioSource rollSource;
	private GameHandler GH;

	// Use this for initialization
	void Awake()
	{
        rs = GameObject.Find("robotSphere").GetComponent<RobotFreeAnim>();
		anim = GameObject.Find("robotSphere").GetComponent<Animator>();
		part = GameObject.Find("robotSphere").GetComponent<ParticleSystem>();
		rot = gameObject.transform.eulerAngles;

		chargeSource = GetComponent<AudioSource>();
		chargeSource.clip = charge;

		rollSource = GameObject.Find("Audio").GetComponent<AudioSource>();
		rollSource.clip = roll;

		rb = GetComponent<Rigidbody>();

		GH = GameObject.Find("HUD").GetComponent<GameHandler>();
	}

	// Update is called once per frame
	void Update()
	{
		CheckKey();
		gameObject.transform.eulerAngles = rot;
	}

	void switchBool() {
		canCheck = true;
	}

	void CheckKey()
	{
		if(rb.velocity.magnitude < 0.1f) {
			isMoving = false;
		}
		else {
			isMoving = true;
		}

		if (canCheck && !isMoving && anim.GetBool("Roll_Anim") && !Input.GetKey(KeyCode.Space)) {
			anim.SetBool("Roll_Anim", false);
			canCheck = false;
		}

		if (anim.GetCurrentAnimatorStateInfo(0).IsName("anim_Idle_Loop_S") || anim.GetCurrentAnimatorStateInfo(0).IsName("anim_Walk_Loop")) {
            canWalk = true;
        }
		else {
			canWalk = false;
		}

		if(anim.GetCurrentAnimatorStateInfo(0).IsName("closed_Roll_Loop")) {
			if(!rollSource.isPlaying && !GH.youLose && !GH.youWon)
				rollSource.Play();
			canRev = true;
		}
		else {
			rollSource.Stop();
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
			canCheck = false;
			if(revForce < maxRev && canRev){
				revForce += revForceDelta * Time.deltaTime;
				if (!part.isPlaying) {
					part.Play();
				}
				if(!chargeSource.isPlaying){
					chargeSource.Play();
				}
			}
			else {
				part.Stop();
			}
		}

		if(Input.GetKeyUp(KeyCode.Space))
		{
			part.Stop();
			chargeSource.Stop();
			rb.AddForce(transform.forward * revForce, ForceMode.VelocityChange);
			if(revForce > 1.0)
				chargeSource.PlayOneShot(chargeRelease);
			revForce = 0.0f;
			Invoke("switchBool", .1f);
		}

		//Brake
		if(Input.GetKey(KeyCode.S))
		{
			if(isMoving)
			{
				rb.velocity *= brakeSpeed;
				rb.angularVelocity *= brakeSpeed;
			}
		}
	}

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "Obstacle") {
			chargeSource.PlayOneShot(collisionSound);
		}
	}
}
