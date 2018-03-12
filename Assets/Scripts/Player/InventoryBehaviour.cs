using RogueGem.Items;
using RogueGem.Utilities;
using System;
using UnityEngine;

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

        public bool TryUseItem(int index, out InventoryItem item) {
            item = inventory[index];
            if (item == null){
                return false;
            }

            UpdateAmount(index, item.GetAmount());
            return true;            
        }

        public void UpdateAmount(int index, int newAmount) {
            if (newAmount.Equals(0)) {
                uiBehaviour.EmptyInventory(index);
                inventory[index] = null;
                --itemCount;
            } else {
                uiBehaviour.UpdateInventory(index, inventory[index]);
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
