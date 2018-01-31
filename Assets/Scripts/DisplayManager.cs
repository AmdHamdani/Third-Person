using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayManager : MonoBehaviour {

	// list of UI used for dialogue
	public GameObject playerDialog;
	public GameObject posDialog;
	public GameObject negDialog;
	public GameObject aDialog;
	public GameObject bDialog;
	public GameObject endingPanel;

	// delay for displaying word by word
	public float delay = 1;

	private Text aDialogText;
	private Text bDialogText;
	private bool run;

	private string[] posChoice;
	private string[] negChoice;

	private void Start() {
		DisableAll ();
	}

	// showring regular text
	public void ShowText(string words, CharacterType type) {
		Text text = null;

		StopCoroutine ("DisplaText");
		if (type == CharacterType.A) {
			aDialog.SetActive (true);
			text = aDialog.GetComponentInChildren<Text> ();
		} else if (type == CharacterType.B) {
			bDialog.SetActive (true);
			text = bDialog.GetComponentInChildren<Text> ();
		} else if (type == CharacterType.K) {
			text = playerDialog.GetComponentInChildren<Text> ();
			playerDialog.SetActive (true);
		}
		text.text = "";
//		StartCoroutine (DisplayText(words, text));	// for showing word by word
		ShowText(words, text);
	}

	// stop showing text
	public void StopShowingText() {
		StopCoroutine("DisplayText");
		DisableAll ();
	}

	// set ending by the positif and negatif value
	public void SetEnding(string ending) {
		endingPanel.SetActive (true);
		endingPanel.GetComponentInChildren<Text> ().text = ending;
	}

	// show choice box
	public void SetChoice(string posText, string negText) {
		posDialog.SetActive (true);
		negDialog.SetActive (true);

		posDialog.GetComponentInChildren<Text> ().text = posText;
		negDialog.GetComponentInChildren<Text> ().text = negText;
	}

	// disable choice button after choosing
	public void DisableButton() {
		posDialog.SetActive (false);
		StopCoroutine ("ShowPositifChoice");
		negDialog.SetActive (false);
		StopCoroutine ("ShowNegatifChoice");
	}

	// displaying text word by word
	private IEnumerator DisplayText(string words, Text dialog) {
		foreach (string text in FindObjectOfType<RouteManager>().GetWords(words)) {
			dialog.text = "";
			dialog.text = text;
			yield return new WaitForSeconds (delay);
		}
		StopCoroutine ("DisplayText");

	}

	// show all the text at once
	private void ShowText(string words, Text dialog) {
		dialog.text = words;
	}
		
	// disable All UI
	private void DisableAll() {
		playerDialog.SetActive (false);
		posDialog.SetActive (false);
		negDialog.SetActive (false);
		aDialog.SetActive (false);
		bDialog.SetActive (false);
		endingPanel.SetActive(false);
	}

}
