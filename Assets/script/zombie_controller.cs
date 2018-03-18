using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie_controller : MonoBehaviour {
	
	private Animator anim;
	private Rigidbody2D rigidbody2D ;
	private SpriteRenderer spriteRender;
	// Use this for initialization
	private bool nonDamage = true;
	private int c;
	void Start () {
		c = 0;
		anim = GetComponent<Animator>();
		rigidbody2D = GetComponent<Rigidbody2D>();
		spriteRender = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		
		if(col.gameObject.tag == "attack" && nonDamage)
		{			
			c++;
			nonDamage = false;
			Debug.Log ("hit" + c);

			anim.SetTrigger ("die");

		}
	}

	void OnTriggerExit2D (Collider2D col){
		if (col.gameObject.tag == "attack") {
			Debug.Log ("end_hit" + c);
			nonDamage = true;
		

		}
	}


}
