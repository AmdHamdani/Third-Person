using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	// speed movement of player
	public float speed;

	private void FixedUpdate () {
		float x = Input.GetAxisRaw ("Horizontal");	// detect input on horizontal axis (A, W, LeftArrow, RightArrow)
//		float mirror = (x != 0) ? 1 : x;

		// flip the facing direction of character
		if (x == -1)
			transform.localScale = new Vector3 (1, 1, 1);
		else if (x == 1)
			transform.localScale = new Vector3 (-1, 1, 1);

		// move the character
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (speed * x, 0);
	}

}
