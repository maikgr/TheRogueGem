using System;
using UnityEngine;
using RogueGem.Player;

namespace RogueGem.Items{
	public class InventoryItem{

		private int amount;
		private string name;

		public InventoryItem (Item item){
			amount = item.GetAmount ();
			name = item.GetName ();
		}

		public void UseOn(PlayerBehaviour player){
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
	}
}

