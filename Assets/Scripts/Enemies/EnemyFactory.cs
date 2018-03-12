using System.Collections.Generic;
using UnityEngine;

namespace RogueGem.Enemies {
    public class EnemyFactory {
        private static readonly string SNEK = "Snek";
        private static readonly string BUBUGG = "Bubugg";
        private static readonly string SLIME = "Slime";
        private static readonly string WILDER = "Wilder";
        private static readonly string SKULLY = "Skully";
        private static readonly string WIGGLE_GREEN = "GreenWiggle";
        private static readonly string WIGGLE_BROWN = "BrownWiggle";
        private static readonly string HUGGY = "Huggy";
        private static readonly string PORKY = "Porky";
        private static readonly string SKULLY_RED = "RedSkully";
        private static readonly string GOOBER = "Goober";
        private static readonly string PIERSON = "Pierson";

        public static GameObject GetEnemy(int level) {
            string[] enemyRepo;
            switch (level) {
                case 1:
                    enemyRepo = new string[] { SNEK, SLIME, BUBUGG, WILDER };
                    break;
                case 2:
                    enemyRepo = new string[] { WIGGLE_GREEN, WIGGLE_BROWN, SKULLY, HUGGY };
                    break;
                case 3:
                    enemyRepo = new string[] { SKULLY_RED, PIERSON, PIERSON, GOOBER, PORKY };
                    break;
                default:
                    enemyRepo = new string[] { SLIME };
                    break;
            }

            int dice = UnityEngine.Random.Range(0, enemyRepo.Length);

            return Resources.Load<GameObject>("Prefabs/Creatures/" + enemyRepo[dice]);
        }

		public static GameObject GetRandomEnemy() {
            GameObject[] enemies = Resources.LoadAll<GameObject>("Prefabs/Creatures");
            return enemies[Random.Range(0, enemies.Length)];
        }

        public static GameObject GetBossMayhoc() {
            return Resources.Load<GameObject>("Prefabs/BossMayhoc");
        }
    }
}
