using RogueGem.Items;
using RogueGem.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace RogueGem.Player {
    public class InventoryBehaviour : MonoBehaviour {
        public GameObject groundSlot;
        public List<GameObject> inventorySlot;

        public void PutInInventory(Item item) {
            Texture itemSprite = item.gameObject.GetComponent<SpriteRenderer>().sprite.texture;
            foreach (var slot in inventorySlot) {
                RawImage slotContent = slot.transform.Find("Content").GetComponent<RawImage>();                
                if (slotContent.texture == null) {
                    Text slotAmount = slot.transform.Find("Quantity").GetComponent<Text>();
                    slotContent.enabled = true;
                    slotContent.texture = itemSprite;
                    slotAmount.text = item.GetAmount().ToString();
                    RemoveFromGroundSlot();
                    Destroy(item.gameObject);
                    EventBehaviour.TriggerEvent(GameEvent.MoveEnemy);
                    break;
                }
            }
        }

        public void PutInGroundSlot(Item item) {
            RawImage groundContent = groundSlot.transform.Find("Content").GetComponent<RawImage>();
            Text groundAmount = groundSlot.transform.Find("Quantity").GetComponent<Text>();
            groundContent.enabled = true;
            groundContent.texture = item.gameObject.GetComponent<SpriteRenderer>().sprite.texture;
            groundAmount.text = item.GetAmount().ToString();
        }

        public void RemoveFromGroundSlot() {
            RawImage groundContent = groundSlot.transform.Find("Content").GetComponent<RawImage>();
            Text groundAmount = groundSlot.transform.Find("Quantity").GetComponent<Text>();
            if (groundContent.texture != null) {
                groundContent.enabled = false;
                groundContent.texture = null;
                groundAmount.text = string.Empty;
            }
        }
    }
}
