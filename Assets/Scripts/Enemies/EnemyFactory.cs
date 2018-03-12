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
                    enemyRepo = new string[] { SNEK, SLIME, BUBUGG, SLIME };
                    break;
                case 2:
                    enemyRepo = new string[] { SNEK, SNEK, SLIME, BUBUGG, WILDER };
                    break;
                case 3:
                    enemyRepo = new string[] { SNEK, WILDER, PIERSON };
                    break;
                case 4:
                    enemyRepo = new string[] { PIERSON, WIGGLE_GREEN, SLIME };
                    break;
                case 5:
                    enemyRepo = new string[] { WIGGLE_GREEN, WIGGLE_BROWN, SKULLY, HUGGY };
                    break;
                case 6:
                    enemyRepo = new string[] { SKULLY, SKULLY_RED, WIGGLE_BROWN, PIERSON, PORKY };
                    break;
                case 7:
                    enemyRepo = new string[] { GOOBER, PIERSON, PORKY, PIERSON, SKULLY_RED, PIERSON };
                    break;
                case 8:
                    enemyRepo = new string[] { GOOBER, GOOBER, PIERSON, SKULLY_RED, WIGGLE_BROWN, HUGGY };
                    break;
                case 9:
                    enemyRepo = new string[] { SKULLY_RED, PIERSON, PIERSON, GOOBER, GOOBER, WIGGLE_BROWN, PORKY, HUGGY };
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
