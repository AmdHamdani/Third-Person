using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Global : MonoBehaviour {

	public int positif;		// storing current positif choice
	public int negatif;		// storing current negatif choice
	public int counter;		// storing total signal
	public RouteManager[] route;
	public bool isInit;

	public List<string> values = new List<string>();	// list of value that is created using 'set' command

	// check is the two character reach end of their dialogue
	public bool Ending() {
		return route [0].End () == route [1].End ();
	}

	// create value using set keyword
	public void SetValue(string val) {
		if (!values.Contains (val))
			values.Add (val);
	}

	// move to next scene
	public void NextEpisode() {
		SceneManager.LoadScene ("Episode 2");
	}

	// get the value of values list using need keyword
	public bool GetValue(string value) {
		return values.Contains (value);
	}

	// delete value from values list
	public void RemoveValue(string value) {
		values.Remove (value);
	}

	// add positif and counter value
	public void AddPos() {
		positif++;
		counter++;
	}

	// add negatif and counter value
	public void AddNeg() {
		negatif++;
		counter++;
	}

	// check is the positif value greater or not
	public bool PositifIsGreater() {
		return positif > negatif;
	}

	// check is the value exists in the values list
	public bool isExist(string value) {
		return value.Contains (value);
	}

}
