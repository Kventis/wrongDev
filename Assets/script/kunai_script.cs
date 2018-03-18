using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kunai_script : MonoBehaviour {

	public GameObject parent;

	private Rigidbody2D rigidbody ;
	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (!coll.gameObject.tag.Equals ("Player") && !coll.gameObject.tag.Equals ("attack") && !coll.gameObject.tag.Equals ("MainCamera")) {
			this.gameObject.SetActive (false);
			parent.GetComponent<character_controller> ().returnKunai (this.gameObject);
		}

		if (coll.gameObject.tag.Equals ("MainCamera")){
			this.gameObject.SetActive (false);
			rigidbody.velocity = Vector2.zero;
			rigidbody.angularVelocity = 0;
			parent.GetComponent<character_controller> ().returnKunai (this.gameObject);
		}

	}

	void OnTriggerExit2D(Collider2D coll) {
		
	}
}
