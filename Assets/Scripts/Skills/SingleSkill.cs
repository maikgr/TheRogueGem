using System;

namespace RogueGem.Skills {
    public abstract class SingleSkill : Skill{

        public SingleSkill(string name, int damage)
            : base(name, damage) {
        }

        public override SkillArea GetSkillArea() {
            return SkillArea.Single;
        }
    }
}
