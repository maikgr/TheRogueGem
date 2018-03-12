using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public string sceneName;
	public string scriptName;
	public Queue<string> sentences = new Queue<string> ();
	public Text dialogueText;

	public void Start() {	
		LoadLines (scriptName);
	}

	public void LoadLines(string scriptName) {
		string[] s = Strings.dialogueMap[scriptName];	
		foreach (string line in s) {
			sentences.Enqueue (line);
		}
	}

	public virtual void DisplayNextSentence() {
		if (sentences.Count == 0)
		{
			EndDialogue ();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	private void EndDialogue() {
		if (sceneName != null) {
			SceneManager.LoadScene (sceneName);
		}
	}

}
