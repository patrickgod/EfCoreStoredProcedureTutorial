using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EfCoreStoredProcedureTutorial
{
    public partial class dotnetrpgContext : DbContext
    {
        public dotnetrpgContext()
        {
        }

        public dotnetrpgContext(DbContextOptions<dotnetrpgContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Character> Characters { get; set; } = null!;
        public virtual DbSet<Skill> Skills { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Weapon> Weapons { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=dotnet-rpg;Trusted_Connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_Characters_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Characters)
                    .HasForeignKey(d => d.UserId);

                entity.HasMany(d => d.Skills)
                    .WithMany(p => p.Characters)
                    .UsingEntity<Dictionary<string, object>>(
                        "CharacterSkill",
                        l => l.HasOne<Skill>().WithMany().HasForeignKey("SkillsId"),
                        r => r.HasOne<Character>().WithMany().HasForeignKey("CharactersId"),
                        j =>
                        {
                            j.HasKey("CharactersId", "SkillsId");

                            j.ToTable("CharacterSkill");

                            j.HasIndex(new[] { "SkillsId" }, "IX_CharacterSkill_SkillsId");
                        });
            });

            modelBuilder.Entity<Weapon>(entity =>
            {
                entity.HasIndex(e => e.CharacterId, "IX_Weapons_CharacterId")
                    .IsUnique();

                entity.HasOne(d => d.Character)
                    .WithOne(p => p.Weapon)
                    .HasForeignKey<Weapon>(d => d.CharacterId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
