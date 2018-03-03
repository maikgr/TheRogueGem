using RogueGem.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RogueGem.Controllers {
    public class MovementController {

        private const float moveSpeed = 0.2f;
        private MonoBehaviour context;
        private Coroutine animation;

        public MovementController(MonoBehaviour context) {
            this.context = context;
        }

        public void MoveByTile(GameObject entity, int xPos, int yPos) {
            Vector2 destinationPos = new Vector2(entity.transform.position.x + xPos, entity.transform.position.y + yPos);

            if (WorldController.CheckCharacterExistsOnPos(destinationPos)) {                
                GameObject characterOnPos = WorldController.GetCharacterOnPos(destinationPos);
                
                if (IsEnemy(entity, characterOnPos)) {
                    Attack(entity, xPos, yPos);
                }
            } else {
                animation = context.StartCoroutine(AnimateMoving(entity, destinationPos));
            }
        }

        private void Attack(GameObject attacker, int xPos, int yPos) {            
            Vector2 initialPos = attacker.transform.position;
            Vector2 destinationPos = initialPos + (new Vector2(xPos, yPos) * 2f);
            context.StartCoroutine(Attack(attacker, destinationPos));
            context.StartCoroutine(Return(attacker, initialPos));
        }

        private IEnumerator AnimateMoving(GameObject entity, Vector2 destination) {            
            if (animation == null) {
                float t = 0;
                while (t < 1) {
                    t += Time.deltaTime / moveSpeed;
                    entity.transform.position = Vector2.Lerp(entity.transform.position, destination, t);
                    yield return null;
                }
                animation = null;
            }            
        }

        private IEnumerator Attack(GameObject attacker, Vector2 destinationPos) {
            yield return context.StartCoroutine(AnimateMoving(attacker, destinationPos));
        }

        private IEnumerator Return(GameObject attacker, Vector2 initialPos) {
            yield return context.StartCoroutine(AnimateMoving(attacker, initialPos));
        }

        private bool IsEnemy(GameObject attacker, GameObject defender) {
            if (attacker.tag.Equals(CharacterTag.Player.ToString()) && defender.tag.Equals(CharacterTag.Enemy.ToString())) {
                return true;
            } else if (attacker.tag.Equals(CharacterTag.Enemy.ToString()) && defender.tag.Equals(CharacterTag.Player.ToString())) {
                return true;
            }
            return false;
        }
    }
}