using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RogueGem.Controllers {
    public class WorldController {

        private static Dictionary<Vector2, GameObject> characterMap = new Dictionary<Vector2, GameObject>();     

        public static void SpawnCharacter(GameObject character, Vector2 pos) {
            characterMap.Add(pos, character);
        }

        public static void MoveCharacterPos(GameObject character, Vector2 oldPos, Vector2 newPos) {
            characterMap.Remove(oldPos);
            characterMap.Add(newPos, character);
        }

        public static void RemoveCharacterOnPos(Vector2 pos) {
            characterMap.Remove(pos);
        }

        public static GameObject GetCharacterOnPos(Vector2 pos) {
            return characterMap[pos];
        }

        public static bool CheckCharacterExistsOnPos(Vector2 pos) {            
            return characterMap.ContainsKey(pos);
        }

        public static void Reset() {
            characterMap.Clear();
        }
    }
}