using RogueGem.Controllers;
using RogueGem.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RogueGem.Enemies {
    public abstract class ICreature {
        protected int currentHp;
        protected EnemyState state;

        public ICreature() {
            currentHp = GetMaxHP();
            state = EnemyState.Normal;
        }

        public abstract string GetName();
        public abstract int GetMaxHP();
        public abstract int GetATK();
        public abstract int GetDEF();
        public abstract int GetCRIT();
        public abstract GameObject GetPrefab();
        public abstract IEnumerable<IItem> GetItemLoot();
        public abstract Vector2 GetDestination();       

        public virtual void ReceiveDamage(int damage) {
            currentHp = currentHp - damage;
            if (currentHp <= 1 && state.Equals(EnemyState.Normal)) {
                currentHp = 1;
                state = EnemyState.Fainted;
            } else if (state.Equals(EnemyState.Fainted)) {
                currentHp = 0;
                state = EnemyState.Dead;
            }
        }

        public int GetCurentHP() {
            return currentHp;
        }

        public EnemyState GetState() {
            return state;
        }
    }

    public enum EnemyState {
        Normal,
        Fainted,
        Dead
    }
}
