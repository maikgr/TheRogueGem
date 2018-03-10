using RogueGem.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RogueGem.Skills {
    public class AttackController {

        public static void Attack(CreatureBehaviour attacker, CreatureBehaviour defender) {
            defender.ReceiveDamage(attacker.GetATK());
        }
    }
}
