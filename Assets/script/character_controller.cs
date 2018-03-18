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
	public Transform weapon_melee; 
	public float groundRadius = 0.2f;
	public LayerMask whatIsGround;


	private bool onGround = true;
	private Animator anim;
	private Rigidbody2D rigidbody2D ;
	private SpriteRenderer spriteRender;

	public GameObject kunai;
	private Queue<GameObject> kunai_queue;
	public Transform kunay_start; 

	// Use this for initialization
	void Start () {
		Physics2D.IgnoreLayerCollision (10, 9);
		Physics2D.IgnoreLayerCollision (11, 9);
		Physics2D.IgnoreLayerCollision (11, 11);
		anim = GetComponent<Animator>();
		rigidbody2D = GetComponent<Rigidbody2D>();
		spriteRender = GetComponent<SpriteRenderer>();
		weapon_melee.GetComponent<CapsuleCollider2D> ().enabled = false;

		//create kunai pool
		kunai_queue = new Queue<GameObject> ();
		Vector3 pos = kunay_start.position;
		for (int i = 0; i < 3; i++) {
			GameObject kun = Instantiate (kunai, pos, Quaternion.identity);
			kun.SetActive (false);
			kun.GetComponent<kunai_script> ().parent = this.gameObject;
			kunai_queue.Enqueue (kun);

		}
	}


	private void FixedUpdate()
	{

		onGround = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("ground", onGround);
		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);
		float move = Input.GetAxis("Horizontal");
		if (move > 0  && spriteRender.flipX || move < 0 && !spriteRender.flipX) {
			spriteRender.flipX = !spriteRender.flipX;
			weapon_melee.transform.localPosition = new Vector3(-weapon_melee.transform.localPosition.x, weapon_melee.transform.localPosition.y);
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
		if (onGround && Input.GetButtonDown ("Fire1")) {
			rigidbody2D.velocity = new Vector2(0 , 0.05f);
			anim.SetTrigger("isAttack");
			startMelee ();
			//weapon_melee.
		} if (onGround && Input.GetButtonDown ("Fire2")) {		
			if (kunai_queue.Count > 0) {
				anim.SetTrigger ("isRangeAttack");
			}

			//weapon_melee.
		}

	}

	private void startMelee()
	{
		if (onGround) {
			weapon_melee.GetComponent<CapsuleCollider2D> ().enabled = true;
		}
	}

	private void throwKunai()
	{
		if (onGround && kunai_queue.Count > 0) {
			GameObject kun = kunai_queue.Dequeue ();

			kun.transform.position = kunay_start.position;
			int speed = 600;
			if (spriteRender.flipX) {
				speed = -speed;
			}
				
			kun.GetComponent<SpriteRenderer>().flipX  = spriteRender.flipX;

			kun.SetActive (true);

			kun.GetComponent<Rigidbody2D>().AddForce (new Vector2(speed, 0));
		}
	}

	public void returnKunai(GameObject kun)
	{
		kunai_queue.Enqueue (kun);
	}

	private void endMelee()
	{
		weapon_melee.GetComponent<CapsuleCollider2D> ().enabled = false;
	}


}
