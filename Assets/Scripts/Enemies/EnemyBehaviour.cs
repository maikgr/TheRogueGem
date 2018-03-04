using RogueGem.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RogueGem.Enemies {
    public class EnemyBehaviour : MonoBehaviour{

        private ICreature creature;
        private MovementController movementControl;
        void Start() {
            creature = gameObject.GetComponent<ICreature>();
            movementControl = new MovementController(this);
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)
                || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)
                || Input.GetKeyDown(KeyCode.Space)) {
                Vector2 destination = creature.GetDestination();
                movementControl.MoveByTile(gameObject, (int)destination.x, (int)destination.y);
            }
        }
    }
}
