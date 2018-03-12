using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RogueGem.Utilities {
	class MessagesController : MonoBehaviour{

		private static List<Message> messageList = new List<Message> ();
		public static int maxMessages = 50;
		public GameObject mBox, textO;

		private static GameObject messageBox, textObject;        

		// Set private static variables to the ones exposed in Editor. Not great practice but good enough!
		void Start() {
			messageBox = mBox;
			textObject = textO;
		}

        public static void DisplayMessage (string text) {
			if (messageList.Count >= maxMessages) {
				Destroy (messageList [0].textObject.gameObject);
				messageList.Remove (messageList [0]);
			}

			Message newMessage = new Message ();
			newMessage.text = text;

			GameObject newText = Instantiate (textObject, messageBox.transform) as GameObject;
			newMessage.textObject = newText.GetComponent<Text> ();
			newMessage.textObject.text = newMessage.text;

			messageList.Add (newMessage);

            Debug.Log(text);
        }
    }

	public class Message {
		public string text;
		public Text textObject;
	}
}
