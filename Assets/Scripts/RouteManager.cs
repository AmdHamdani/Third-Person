using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouteManager : MonoBehaviour {

	public TextAsset script;	// txt file of the character dialogue
	public DisplayManager dm;
	public Global global;
	public string[] scripts;	// contains every line of dialogue

	[SerializeField]
	private int idx;
	private string initialText;
	[SerializeField]
	private string currentLine;

	private void Start() {
		scripts = Parse (script.text, "\n");
		initialText = Parse(scripts [idx++], ";")[1];
	}

	// Used to call the next dialogue.
	// Consits of parser and run the line of dialogue according the the dialog command.
	public void NextRoute() {
		if (idx == scripts.Length)
			return;

		// split the scripts by ';'
		string[] cmd = Parse (scripts [idx++], ";");

		if ("a b k".Contains (cmd [0])) {	// used to show regular dialogue
			TextCmd (cmd [0], cmd [1]);
		} else if (cmd [0] == "choice") {	// used to show the choice
			string[] tmp = Parse (cmd [1], "|");
			dm.SetChoice (Parse (tmp [0], "=") [1], Parse (tmp [1], "=") [1]);
		} else if (cmd [0].Contains ("rplus") || cmd [1].Contains ("rmin")) {	// used to show dialogue according to the selected choice
			if (FindObjectOfType<Global> ().PositifIsGreater ()) {
				RouteBranch (cmd [0]);
			} else {
				RouteBranch (cmd [1]);
			}
		} else if (cmd [0].Contains ("set")) {			// set variable
			global.SetValue (Parse (cmd [0], "=") [1]);
			currentLine = Parse (cmd [1], "=") [1];
		} else if (cmd [0].Contains ("need")) {			// used to make the dialogue running, if the required variable is exist
//			Debug.Log (cmd [0] + "cmd");
			if (!global.GetValue (Parse (cmd [0], "=") [1])) {
				idx--;
				return;
			} else {
				if ("a b k".Contains (Parse (cmd [1], "=") [0])) {
					string[] temp = Parse (cmd [1], "=");
					TextCmd (temp [0], temp [1]);
				} else if (cmd [1].Contains ("choice")) {
//					Debug.Log (cmd [1]);
					string choice = Parse (cmd [1], "?") [1];
					string[] tmp = Parse(choice, "|");
					dm.SetChoice (Parse (tmp [0], "=") [1], Parse (tmp [1], "=") [1]);
				}
			}
		}

	}
		
	// Detecting the current index equals the scripts length.
	public bool End() {
		return idx == scripts.Length;
	}
		
	// Showing regular dialogue based on character.
	private void TextCmd(string type, string txt) {
		CharacterType ctype = CharacterType.K;
		currentLine = txt;

		if (type == "a")
			ctype = CharacterType.A;
		else if (type == "b")
			ctype = CharacterType.B;
		else if (type == "k")
			ctype = CharacterType.K;

		dm.ShowText (txt, ctype);
	}


	// Similiar like TextCmd, but this one is used in Branching
	private void RouteBranch(string cmd) {
		string line = Parse (cmd, "|")[1];
		string[] temp = Parse (line, "=");
		currentLine = temp [1];
		TextCmd (temp[0], temp[1]);
	}

	public string GetText() {
		return currentLine;
	}

	public string GetInitialText() {
		return initialText;
	}

	public string[] GetWords(string scripts) {
		return Parse (scripts, " ");
	}

	// used to split string based on separator
	private string[] Parse(string script, string separator) {
		return script.Split (new string[] { separator }, System.StringSplitOptions.None);
	}

}
