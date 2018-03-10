using System;

namespace RogueGem.Skills {
    public abstract class LinearSkill : Skill{
        protected int distance;

        public LinearSkill(string name, int damage, int distance)
            : base(name, damage) {
            this.distance = distance;
        }

        public override SkillArea GetSkillArea() {
            return SkillArea.Linear;
        }

        public virtual int GetDistance() {
            return distance;
        }
    }
}
