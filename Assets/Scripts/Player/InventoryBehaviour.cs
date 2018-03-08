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

        private Item[] inventory;
        private RawImage[] inventorySlotImage;
        private Text[] inventorySlotText;
        private RawImage groundSlotImage;
        private Text groundSlotText;
        private int itemCount;

        void Start() {       
            inventory = new Item[inventorySlot.Length];
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
            Item itemInInventory = Array.Find(inventory, i => i != null && i.name == item.name);
            if (itemInInventory == default(Item) && itemCount < inventory.Length) {
                Debug.Log("Adding new item");
                AddNewItemToInventory(item);
            } else if (itemInInventory != default(Item)) {
                Debug.Log("Updating amount");
                AddItemAmountInInventory(item);
            }            
        }

        private void AddNewItemToInventory(Item item) {
            for (int i = 0; i < inventory.Length; ++i) {
                if (inventory[i] == null) {
                    inventory[i] = item;
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
                if (inventory[i] == item) {
                    Debug.Log(inventory[i].GetAmount() + " - " +item.GetAmount());
                    int newAmount = inventory[i].GetAmount() + item.GetAmount();
                    inventory[i].SetAmount(newAmount);
                    inventorySlotText[i].text = newAmount.ToString();

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

        class InventoryItem {
            public RawImage image;
            public Text text;
            public Item item;

            private Texture emptyImage;
            public InventoryItem(RawImage image, Text text, Texture emptyImage) {
                this.image = image;
                this.text = text;
                this.emptyImage = emptyImage;
            }

            public void Add(Item item) {
                image.texture = item.gameObject.GetComponent<SpriteRenderer>().sprite.texture;
                text.text = item.GetAmount().ToString(); ;
                this.item = item;
            }

            public void Remove() {
                item = null;
                text.text = string.Empty;
                image.texture = emptyImage;
            }
        }
    }
}
