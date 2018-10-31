using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour {

	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Horizontal") != 0 ||
		    Input.GetAxis ("Vertical") != 0) {
			anim.SetBool ("moving", true);
		} else {
			anim.SetBool ("moving", false);
		}

		if (Input.GetAxis ("Horizontal") < 0) {
			this.transform.localScale = new Vector3 (-1.0f,
				transform.localScale.y);
		} else if(Input.GetAxis ("Horizontal") > 0) {
			this.transform.localScale = new Vector3 (1.0f,
				transform.localScale.y);
		}
	}
}
