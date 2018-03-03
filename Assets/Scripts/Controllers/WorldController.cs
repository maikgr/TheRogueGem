using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RogueGem.Controllers {
    public class WorldController {       

        public static GameObject GetCharacterOnPos(Vector2 pos) {
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.down);
            if(hit.collider != null) {
                return hit.transform.gameObject;
            }
            return null;
        }

        public static bool CheckCharacterExistsOnPos(Vector2 pos) {
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            return hit.collider != null;
        }
    }
}