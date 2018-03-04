using RogueGem.Controllers;
using RogueGem.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RogueGem.Enemies {
    public abstract class CreatureBehaviour : MonoBehaviour {
        protected int currentHp;

        void Awake() {
            currentHp = GetMaxHP();
        }

        public abstract string GetName();
        public abstract int GetMaxHP();
        public abstract int GetATK();
        public abstract int GetDEF();
        public abstract int GetCRIT();
        public abstract IEnumerable<IItem> GetItemLoot();
        public abstract Vector2 GetDestination();
        public abstract void ReceiveDamage(int damage);        

        public int GetCurentHP() {
            return currentHp;
        }
    }

    public enum EnemyState {
        Normal,
        Fainted,
        Dead
    }
}
