using System;
using UnityEngine;
using RogueGem.Player;
using UnityEngine.UI;

namespace RogueGem.Items {
	public abstract class InventoryItem {

		private int amount;
		private string name;
        private Texture image;
        private ItemType type;

		public InventoryItem (Item item){
			amount = item.GetAmount ();
			name = item.GetName ();
            image = item.GetComponent<SpriteRenderer>().sprite.texture;
            type = item.GetItemType();
		}

		public virtual void Use(PlayerBehaviour player){
            amount--;
		}

		public void AddAmount(int addByAmount){
			amount = amount + addByAmount;
		}

		public string GetName(){
			return name;
		}

		public int GetAmount(){
			return amount;
		}

        public Texture GetImage() {
            return image;
        }

        public ItemType GetItemType() {
            return type;
        }
	}
}

