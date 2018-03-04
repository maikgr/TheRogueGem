using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RogueGem.Controllers {
    public class PlayerBehaviour : MonoBehaviour {

        private MovementController movementControl;
        private int atk;
        private int def;
        private int crit;
        private int maxHp;
        private int currentHp;
	    void Start () {
            movementControl = new MovementController(this);
            atk = 2;
            def = 1;
            crit = 10;
            maxHp = 10;
            currentHp = maxHp;
	    }
	
	    void Update () {
		    if (Input.GetKeyDown(KeyCode.D)) {
                movementControl.MoveByTile(gameObject, 1, 0);
            } else if (Input.GetKeyDown(KeyCode.A)) {
                movementControl.MoveByTile(gameObject, -1, 0);
            } else if (Input.GetKeyDown(KeyCode.W)) {
                movementControl.MoveByTile(gameObject, 0, 1);
            } else if (Input.GetKeyDown(KeyCode.S)) {
                movementControl.MoveByTile(gameObject, 0, -1);
            }
        }
    }
}
