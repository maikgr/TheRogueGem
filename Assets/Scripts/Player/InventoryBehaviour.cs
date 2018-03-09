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
        private InventoryItem[] inventory;
        private int itemCount;
        private UIBehaviour uiBehaviour;
        void Start() {
            inventory = new InventoryItem[3];
            itemCount = 0;
            uiBehaviour = FindObjectOfType(typeof(UIBehaviour)) as UIBehaviour;
        }

        public void PutInInventory(Item item) {
            if (itemCount.Equals(0)) {
                AddNewItemToInventory(item);
            } else {
                int slotIndex = Array.FindIndex(inventory, i => i != null && i.GetName() == item.GetName());
                if (slotIndex.Equals(-1) && itemCount < inventory.Length) {
                    AddNewItemToInventory(item);
                } else if (!slotIndex.Equals(-1)) {
                    AddItemAmountInInventory(slotIndex, item);
                }
            }
        }

        private void AddNewItemToInventory(Item item) {
            for (int i = 0; i < inventory.Length; ++i) {
                if (inventory[i] == null) {
                    inventory[i] = item.ToInventoryItem();
                    uiBehaviour.UpdateInventory(i, inventory[i]);
                    uiBehaviour.RemoveItemOnGround();
                    Destroy(item.gameObject);
                    ++itemCount;
                    break;
                }
            }
        }

        private void AddItemAmountInInventory(int index, Item item) {
            inventory[index].AddAmount(item.GetAmount());
            uiBehaviour.UpdateInventory(index, inventory[index]);
            uiBehaviour.RemoveItemOnGround();
            Destroy(item.gameObject);
        }
    }
}
