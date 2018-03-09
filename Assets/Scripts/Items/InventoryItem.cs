using System;
using UnityEngine;
using RogueGem.Player;
using UnityEngine.UI;

namespace RogueGem.Items{
	public class InventoryItem{

		private int amount;
		private string name;
        private Texture image; 

		public InventoryItem (Item item){
			amount = item.GetAmount ();
			name = item.GetName ();
            image = item.GetComponent<SpriteRenderer>().sprite.texture;
		}

		public void Use(PlayerBehaviour player){
			player.Heal (amount);
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
	}
}

