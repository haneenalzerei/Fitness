using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Fitness.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

       
        public virtual DbSet<FitnessDietInformation> FitnessDietInformations { get; set; }
        public virtual DbSet<FitnessEmployee> FitnessEmployees { get; set; }
        public virtual DbSet<FitnessRole> FitnessRoles { get; set; }
        public virtual DbSet<FitnessUser> FitnessUsers { get; set; }
        public virtual DbSet<FitnessUserDiet> FitnessUserDiets { get; set; }
        public virtual DbSet<FitnessUserInformation> FitnessUserInformations { get; set; }
        public virtual DbSet<FitnessUserLogin> FitnessUserLogins { get; set; }
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("DATA SOURCE=94.56.229.181:3488/traindb;PASSWORD=haneen1997;USER ID=train_user50");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TRAIN_USER50")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");

  



            modelBuilder.Entity<FitnessDietInformation>(entity =>
            {
                entity.HasKey(e => e.Dietid)
                    .HasName("DIET_PK");

                entity.ToTable("FITNESS_DIET_INFORMATION");

                entity.Property(e => e.Dietid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("DIETID");

                entity.Property(e => e.Diettext)
                    .HasMaxLength(1000)
                    .HasColumnName("DIETTEXT");

                entity.Property(e => e.Maxheight)
                    .HasColumnType("NUMBER")
                    .HasColumnName("MAXHEIGHT");

                entity.Property(e => e.Maxweight)
                    .HasColumnType("NUMBER")
                    .HasColumnName("MAXWEIGHT");
            });

            modelBuilder.Entity<FitnessEmployee>(entity =>
            {
                entity.HasKey(e => e.Empid)
                    .HasName("EMP_PK");

                entity.ToTable("FITNESS_EMPLOYEE");

                entity.Property(e => e.Empid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("EMPID");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(50)
                    .HasColumnName("FULLNAME");

                entity.Property(e => e.Phonenumber)
                    .HasMaxLength(50)
                    .HasColumnName("PHONENUMBER");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ROLEID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.FitnessEmployees)
                    .HasForeignKey(d => d.Roleid)
                    .HasConstraintName("EMP_FK");
            });

            modelBuilder.Entity<FitnessRole>(entity =>
            {
                entity.HasKey(e => e.Roleid)
                    .HasName("FITNESS_ROLE_PK");

                entity.ToTable("FITNESS_ROLE");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ROLEID");

                entity.Property(e => e.Rolename)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("ROLENAME");
            });

            modelBuilder.Entity<FitnessUser>(entity =>
            {
                entity.HasKey(e => e.Userid)
                    .HasName("USER_PK");

                entity.ToTable("FITNESS_USERS");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(50)
                    .HasColumnName("FULLNAME");

                entity.Property(e => e.Phonenumber)
                    .HasMaxLength(50)
                    .HasColumnName("PHONENUMBER");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ROLEID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.FitnessUsers)
                    .HasForeignKey(d => d.Roleid)
                    .HasConstraintName("USER_FK");
            });

            modelBuilder.Entity<FitnessUserDiet>(entity =>
            {
                entity.ToTable("FITNESS_USER_DIETS");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ID");

                entity.Property(e => e.Diettext)
                    .HasMaxLength(1000)
                    .HasColumnName("DIETTEXT");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FitnessUserDiets)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("USER_DIETS_FK");
            });

            modelBuilder.Entity<FitnessUserInformation>(entity =>
            {
                entity.HasKey(e => e.Userinformationid)
                    .HasName("USER_INFORMATION_PK");

                entity.ToTable("FITNESS_USER_INFORMATION");

                entity.Property(e => e.Userinformationid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERINFORMATIONID");

                entity.Property(e => e.Height)
                    .HasColumnType("NUMBER")
                    .HasColumnName("HEIGHT");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.Property(e => e.Weight)
                    .HasColumnType("NUMBER")
                    .HasColumnName("WEIGHT");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FitnessUserInformations)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("USER_INFORMATION_FK");
            });

            modelBuilder.Entity<FitnessUserLogin>(entity =>
            {
                entity.HasKey(e => e.Userloginid)
                    .HasName("USER_LOGIN_PK");

                entity.ToTable("FITNESS_USER_LOGIN");

                entity.Property(e => e.Userloginid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERLOGINID");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("USERNAME");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FitnessUserLogins)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("USER_LOGIN_FK");
            });

        
            modelBuilder.HasSequence("SQ1");

            modelBuilder.HasSequence("SQ2");

            modelBuilder.HasSequence("SQ3");

            modelBuilder.HasSequence("SQ4");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
