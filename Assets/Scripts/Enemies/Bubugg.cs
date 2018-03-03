using System;
using RogueGem.Enemies;
using UnityEngine;

namespace RogueGem.Enemies {
    public class Bubugg : IEnemy {

        private int currentHP;

        public Bubugg() {
            currentHP = GetMaxHP();
        }

        public int GetATK() {
            return 3;
        }

        public int GetCRIT() {
            return 0;
        }

        public int GetCurentHP() {
            return currentHP;
        }

        public int GetDEF() {
            return 1;
        }

        public int GetMaxHP() {
            return 5;
        }

        public string GetName() {
            return "Bubugg";
        }

        public GameObject GetPrefab() {
            return Resources.Load("Prefabs/Bubugg") as GameObject;
        }

        public void RecieveDamage(int damage) {
            throw new NotImplementedException();
        }
    }
}
