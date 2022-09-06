using System;
using System.Collections.Generic;

namespace EfCoreStoredProcedureTutorial
{
    public partial class Skill
    {
        public Skill()
        {
            Characters = new HashSet<Character>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Damage { get; set; }

        public virtual ICollection<Character> Characters { get; set; }
    }
}
