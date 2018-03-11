using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RogueGem.Player;
using UnityEngine;
using RogueGem.Utilities;
using RogueGem.Enemies;

namespace RogueGem.Skills {
    public class AbsorbSkill : SingleSkill {

        public AbsorbSkill(string name, int damage)
            : base(name, damage) { }        

        public override void Use(CreatureBehaviour user, Vector2 direction, CreatureType targetType) {
            PlayerBehaviour player = user as PlayerBehaviour;
            Vector2 targetPos = (Vector2)user.transform.position + direction;            
            if (!WorldController.IsTileEmpty(targetPos)) {
                EnemyBehaviour enemy = WorldController.GetGameObjectOnPos(targetPos).GetComponent<EnemyBehaviour>();
                if (enemy != null) {
                    Debug.Log("You tries to absorb " + enemy.GetName() + "...");
                    if (enemy.GetState() == EnemyState.Fainted) {
                        enemy.Absorb();
                        player.SetSkill(enemy.GetSkill());
                        Debug.Log("You acquired " + enemy.GetSkill().GetName() + "!");
                    } else {
                        Debug.Log("...but " + enemy.GetName() + " resisted!");
                    }
                }
            }
        }
    }
}
