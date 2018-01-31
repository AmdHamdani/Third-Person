using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	public RouteManager rm;
	public DisplayManager dm;
	public CharacterType type;
	public Global global;
	private bool start;
	private bool near;
	public GameObject[] signals;

	private void Start() {
		// deactive all signals
		foreach (GameObject go in signals)
			go.SetActive (false);
	}

	private void Update() {

		// active the signal
		if (global.counter == 1) {
			signals [0].SetActive (true);
		}
		// set the signal color
		signals [0].GetComponent<SpriteRenderer> ().color = global.PositifIsGreater() ? Color.white : Color.black;

		if (global.counter == 2) {
			signals [1].SetActive (true);
		} 
		signals [1].GetComponent<SpriteRenderer> ().color = global.PositifIsGreater() ? Color.white : Color.black;

		if (global.counter == 3) {
			signals [2].SetActive (true);
		}
		signals [2].GetComponent<SpriteRenderer> ().color = global.PositifIsGreater() ? Color.white : Color.black;

		// check for finish condition
		if (global.Ending () && global.counter == 3) {
			if (global.PositifIsGreater ())
				dm.SetEnding ("Akhirnya mereka saling mengetahui isi hati masing - masing.\nTerima kasih orang ketiga.\n\n\nCredits : \nArt : Carlista\nPrograming : \nNaufal\nHamdani\nSound : Bunch - https://opengameart.org/users/buch");
			else
				dm.SetEnding ("Mereka pun tidak pernah bertemu lagi.\nTerima kasih orang ketiga\n\n\nCredits : \nArt : Carlista\nPrograming : \nNaufal\nHamdani\nSound : Bunch - https://opengameart.org/users/buch");
		}
	}

	// run the dialogue when player other character
	private void OnMouseDown() {
		if (global.GetValue ("near " + type)) {
			start = true;
			dm.StopShowingText ();
			rm.NextRoute ();
		} else {
			return;
		}
	}

	// show current dialogue when player near the character
	private void OnTriggerEnter2D() {
		global.SetValue ("near " + type);
		if(global.GetValue ("near " + type) && start)
			dm.ShowText (rm.GetText(), type);
		if(!start)
			dm.ShowText (rm.GetInitialText(), type);
	}

	// remove current dialogue when player far the character
	private void OnTriggerExit2D() {
		global.RemoveValue ("near " + type);
		dm.StopShowingText ();
			
	}

}

public enum CharacterType {
	A, B, K
}