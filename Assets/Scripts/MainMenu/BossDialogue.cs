using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BossDialogue : DialogueManager {

	public Text creatureName;
	public string scriptName2;
	public Animator animator;

	private int conversationPart = 1;


	public override void DisplayNextSentence() {
		if (sentences.Count == 0)
		{
			if (conversationPart == 1) {
				conversationPart++;
				LoadLines (scriptName2);
				animator.SetBool("isAppear", true);
				creatureName.text = "Weird dog with wings???";
			} else {
				LevelManager lm = GetComponent<LevelManager> ();
				lm.BossLevelPart2 ();
			}
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

}
