using RogueGem.Enemies;
using RogueGem.Player;
using UnityEngine;

namespace RogueGem.Skills {
    public abstract class Skill {
        protected string skillName;
        protected int skillDamage;

        public Skill(string name, int damage) {
            skillName = name;
            skillDamage = damage;
        }

        public virtual string GetName() {
            return skillName;
        }
        public virtual int GetDamage() {
            return skillDamage;
        }
        public abstract SkillArea GetSkillArea();
        public abstract void Use(CreatureBehaviour user, Vector2 direction, CreatureType targetType);
    }

    public enum SkillArea {
        Linear,
        Single
    }
}
