using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_controller : MonoBehaviour {

	[SerializeField]
	public float maxSpeed = 0.5f;

	[SerializeField]
	public float upSpeed = 0.5f;

	public float speedInAir = 1f;
	public Transform groundCheck;
	public float groundRadius = 0.2f;
	public LayerMask whatIsGround;


	private bool isFacingRight = true;
	private bool onGround = true;
	private Animator anim;
	private Rigidbody2D rigidbody2D ;
	private SpriteRenderer spriteRender;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rigidbody2D = GetComponent<Rigidbody2D>();
		spriteRender = GetComponent<SpriteRenderer>();
	}


	private void FixedUpdate()
	{

		onGround = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("ground", onGround);
		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);
		float move = Input.GetAxis("Horizontal");
		if (move > 0  && spriteRender.flipX || move < 0 && !spriteRender.flipX) {
			spriteRender.flipX = !spriteRender.flipX;
		}
		if (!onGround) {
			rigidbody2D.AddForce (new Vector2(move * speedInAir, 0));
			return;
		}


		anim.SetFloat("speed", Mathf.Abs(move));
		rigidbody2D.velocity = new Vector2(move * maxSpeed , 0.05f);
		//rigidbody2D.AddForce (new Vector2(move * maxSpeed, 0));


	}

	private void Update(){

		if (onGround && Input.GetButtonDown ("Jump")) {
		
			anim.SetBool ("ground", false);
			rigidbody2D.AddForce (new Vector2(0, upSpeed));
		}

	}


}
