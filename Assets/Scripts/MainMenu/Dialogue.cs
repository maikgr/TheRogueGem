using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour {

	void Start() {
		DialogueManager manager = GetComponent<DialogueManager> ();
		string[] text = Strings.Mayhoc1;

		manager.LoadLines (text);
	}
}
