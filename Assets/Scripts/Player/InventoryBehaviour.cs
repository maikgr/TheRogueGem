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
        public GameObject[] inventorySlot;
        public Texture emptyImage;

		private InventoryItem[] inventory;
        private RawImage[] inventorySlotImage;
        private Text[] inventorySlotText;
        private RawImage groundSlotImage;
        private Text groundSlotText;
        private int itemCount;

        void Start() {
			inventory = new InventoryItem[inventorySlot.Length];
            inventorySlotImage = new RawImage[inventorySlot.Length];
            inventorySlotText = new Text[inventorySlot.Length];
            for(int i = 0; i < inventorySlot.Length; ++i) {
                inventorySlotImage[i] = inventorySlot[i].transform.Find("Content").GetComponent<RawImage>();
                inventorySlotText[i] = inventorySlot[i].transform.Find("Quantity").GetComponent<Text>();
            }

            groundSlotImage = groundSlot.transform.Find("Content").GetComponent<RawImage>();
            groundSlotText = groundSlot.transform.Find("Quantity").GetComponent<Text>();

            itemCount = 0;
        }

        public void PutInInventory(Item item) {
			if (itemCount.Equals(0)) {
				AddNewItemToInventory (item);
			} else {
				InventoryItem itemInInventory = Array.Find(inventory, i => i.GetName() == item.name);
				if (itemInInventory == null && itemCount < inventory.Length){
					Debug.Log ("Add new item " + item.name);
					AddNewItemToInventory(item);
				} else if (itemInInventory != null){
					Debug.Log ("Update amount of " + itemInInventory.GetName());
					AddItemAmountInInventory(item);
				}
			}
        }

        private void AddNewItemToInventory(Item item) {
            for (int i = 0; i < inventory.Length; ++i) {
                if (inventory[i] == null) {
					inventory[i] = item.ToInventoryItem();
                    inventorySlotImage[i].enabled = true;
                    inventorySlotImage[i].texture = item.gameObject.GetComponent<SpriteRenderer>().sprite.texture;
                    inventorySlotText[i].text = item.GetAmount().ToString();

                    RemoveFromGroundSlot();
                    Destroy(item.gameObject);
                    ++itemCount;
                    break;
                }
            }
        }

        private void AddItemAmountInInventory(Item item) {
            for (int i = 0; i < inventory.Length; ++i) {
				if (inventory[i].GetName() == item.name) {
					inventory [i].AddAmount (item.GetAmount ());
					inventorySlotText [i].text = inventory [i].GetAmount().ToString();
                    RemoveFromGroundSlot();
                    Destroy(item.gameObject);
                    break;
                }
            }
        }

        public void PutInGroundSlot(Item item) {
            RawImage groundContent = groundSlot.transform.Find("Content").GetComponent<RawImage>();
            Text groundAmount = groundSlot.transform.Find("Quantity").GetComponent<Text>();
            groundContent.texture = item.gameObject.GetComponent<SpriteRenderer>().sprite.texture;
            groundAmount.text = item.GetAmount().ToString();
        }

        public void RemoveFromGroundSlot() {
            RawImage groundContent = groundSlot.transform.Find("Content").GetComponent<RawImage>();
            Text groundAmount = groundSlot.transform.Find("Quantity").GetComponent<Text>();
            if (groundContent.texture != null) {
                groundContent.texture = emptyImage;
                groundAmount.text = string.Empty;
            }
        }
    }
}
