using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie_controller : MonoBehaviour {
	
	private Animator anim;
	private Rigidbody2D rigidbody2D ;
	private SpriteRenderer spriteRender;
	// Use this for initialization
	private bool nonDamage = true;
	private float startPositionX;
	private bool dead;
	public float maxSpeed = 600;
	public float maxWalkRange = 3;

	void Start () {
		anim = GetComponent<Animator>();
		rigidbody2D = GetComponent<Rigidbody2D>();
		spriteRender = GetComponent<SpriteRenderer>();
		startPositionX = transform.position.x;
		dead = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void FixedUpdate()
	{
		if (dead || !nonDamage) 
			return;
		
		if (maxWalkRange <= transform.position.x - startPositionX && !spriteRender.flipX  || maxWalkRange <= startPositionX - transform.position.x && spriteRender.flipX) {
			maxSpeed = -maxSpeed;
		}
		

		if (maxSpeed > 0  && spriteRender.flipX || maxSpeed < 0 && !spriteRender.flipX) {
			spriteRender.flipX = !spriteRender.flipX;
			//weapon_melee.transform.localPosition = new Vector3(-weapon_melee.transform.localPosition.x, weapon_melee.transform.localPosition.y);
		}

		anim.SetFloat("speed", Mathf.Abs(maxSpeed));
		rigidbody2D.velocity = new Vector2(maxSpeed , 0.05f);

	}

	void OnTriggerEnter2D (Collider2D col)
	{
		
		if(col.gameObject.tag == "attack" && nonDamage)
		{	
			//dead = true;
			nonDamage = false;
			//anim.SetTrigger ("die");

		}
	}

	void OnTriggerExit2D (Collider2D col){
		if (col.gameObject.tag == "attack") {
			nonDamage = true;	

		}
	}


}
