using System;
using System.Collections.Generic;

namespace EfCoreStoredProcedureTutorial
{
    public partial class Character
    {
        public Character()
        {
            Skills = new HashSet<Skill>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int HitPoints { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int Intelligence { get; set; }
        public int Class { get; set; }
        public int? UserId { get; set; }
        public int Defeats { get; set; }
        public int Fights { get; set; }
        public int Victories { get; set; }

        public virtual User? User { get; set; }
        public virtual Weapon Weapon { get; set; } = null!;

        public virtual ICollection<Skill> Skills { get; set; }
    }
}
