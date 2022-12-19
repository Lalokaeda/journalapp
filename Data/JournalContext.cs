using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace journalapp;

public partial class JournalContext : IdentityDbContext<ClassTeacher>
{
    public JournalContext()
    {
    }

    public JournalContext(DbContextOptions<JournalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Business> Businesses { get; set; }

    public virtual DbSet<ClassTeacher> ClassTeachers { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseOfGroup> CourseOfGroups { get; set; }

    public virtual DbSet<Curator> Curators { get; set; }

    public virtual DbSet<Division> Divisions { get; set; }

    public virtual DbSet<EducativeEvent> EducativeEvents { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<GraphicVisitsHostel> GraphicVisitsHostels { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<HealthGroup> HealthGroups { get; set; }

    public virtual DbSet<Hostel> Hostels { get; set; }

    public virtual DbSet<LineOfBusiness> LineOfBusinesses { get; set; }

    public virtual DbSet<Parent> Parents { get; set; }

    public virtual DbSet<RiskGroup> RiskGroups { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Speciality> Specialities { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentAssotiation> StudentAssotiations { get; set; }

    public virtual DbSet<StudentsOfEvent> StudentsOfEvents { get; set; }

    public virtual DbSet<StudentsOnPedControl> StudentsOnPedControls { get; set; }

    public virtual DbSet<TypeOfCrime> TypeOfCrimes { get; set; }

    public virtual DbSet<WorkWithParent> WorkWithParents { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    //       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //  #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //           => optionsBuilder.UseSqlServer("Server=DESKTOP-7464DNT;Database=Journal;TrustServerCertificate=True;Trusted_Connection=True;Integrated Security=SSPI;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { //modelBuilder.Ignore <IdentityUserLogin<string>>();
     base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "44546e06-8719-4ad8-b88a-f271ae9d6eab",
                Name = "admin",
                NormalizedName = "ADMIN"
            });

            modelBuilder.Entity<ClassTeacher>().HasData(new ClassTeacher
            {
                Id = "3b62472e-4f66-49fa-a20f-e7685b9565d8",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "my@email.com",
                NormalizedEmail = "MY@EMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<ClassTeacher>().HashPassword(null, "superpassword"),
                SecurityStamp = string.Empty
            });

             modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "44546e06-8719-4ad8-b88a-f271ae9d6eab",
                UserId = "3b62472e-4f66-49fa-a20f-e7685b9565d8"
            });

        modelBuilder.Entity<Business>(entity =>
        {
            entity.ToTable("Business");

            entity.Property(e => e.Workshop)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Year).HasColumnType("date");

            entity.HasOne(d => d.StudentAssotiation).WithMany(p => p.Businesses)
                .HasForeignKey(d => d.StudentAssotiationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Business_StudentAssotiations");

            entity.HasOne(d => d.Student).WithMany(p => p.Businesses)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK_Business_Students");
        });

        modelBuilder.Entity<ClassTeacher>(entity =>
        {
            
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);
            
            entity.Property(e => e.Patronymic)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CourseOfGroup>(entity =>
        {
            entity.HasKey(e => new { e.GroupId, e.CourseId });

            entity.ToTable("CourseOfGroup");

            entity.Property(e => e.GroupId)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Year).HasColumnType("date");

            entity.HasOne(d => d.Course).WithMany(p => p.CourseOfGroups)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_CourseOfGroup_Courses");

            entity.HasOne(d => d.Group).WithMany(p => p.CourseOfGroups)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK_CourseOfGroup_Groups");
        });

        modelBuilder.Entity<Curator>(entity =>
        {
            entity.Property(e => e.ChildGroupId)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.ChildGroup).WithMany(p => p.Curators)
                .HasForeignKey(d => d.ChildGroupId)
                .HasConstraintName("FK_Curators_Groups");

            entity.HasOne(d => d.Student).WithMany(p => p.Curators)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK_Curators_Students");
        });

        modelBuilder.Entity<Division>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EducativeEvent>(entity =>
        {
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Lobid).HasColumnName("LOBId");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.ClassTeacher).WithMany(p => p.EducativeEvents)
                .HasForeignKey(d => d.ClassTeacherId)
                .HasConstraintName("FK_EducativeEvents_ClassTeachers");

            entity.HasOne(d => d.Lob).WithMany(p => p.EducativeEvents)
                .HasForeignKey(d => d.Lobid)
                .HasConstraintName("FK_EducativeEvents_LineOfBusiness");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<GraphicVisitsHostel>(entity =>
        {
            entity.ToTable("GraphicVisitsHostel");

            entity.Property(e => e.GoalOfVisil)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Result)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.VisitDate).HasColumnType("date");

            entity.HasOne(d => d.Student).WithMany(p => p.GraphicVisitsHostels)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK_GraphicVisitsHostel_Students");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.Property(e => e.Id)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.RecruitmentYear).HasColumnType("date");
            entity.Property(e => e.SpecialityId)
                .HasMaxLength(8)
                .IsUnicode(false);

            entity.HasOne(d => d.ClassTeacher).WithMany(p => p.Groups)
                .HasForeignKey(d => d.ClassTeacherId)
                .HasConstraintName("FK_Groups_ClassTeachers");

            entity.HasOne(d => d.Speciality).WithMany(p => p.Groups)
                .HasForeignKey(d => d.SpecialityId)
                .HasConstraintName("FK_Groups_Specialities");
        });

        modelBuilder.Entity<HealthGroup>(entity =>
        {
            entity.ToTable("HealthGroup");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Recommendation).IsUnicode(false);
        });

        modelBuilder.Entity<Hostel>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Address)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.HasMany(d => d.RoomsNavigation).WithMany(p => p.Hostels)
                .UsingEntity<Dictionary<string, object>>(
                    "RoomsOfHostel",
                    r => r.HasOne<Room>().WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_RoomsOfHostel_Rooms"),
                    l => l.HasOne<Hostel>().WithMany()
                        .HasForeignKey("HostelId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_RoomsOfHostel_Hostels"),
                    j =>
                    {
                        j.HasKey("HostelId", "RoomId");
                        j.ToTable("RoomsOfHostel");
                    });
        });

        modelBuilder.Entity<LineOfBusiness>(entity =>
        {
            entity.ToTable("LineOfBusiness");

            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Parent>(entity =>
        {
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Patronymic)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNum)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasMany(d => d.Srudents).WithMany(p => p.Parents)
                .UsingEntity<Dictionary<string, object>>(
                    "ParentOfStud",
                    r => r.HasOne<Student>().WithMany()
                        .HasForeignKey("SrudentId")
                        .HasConstraintName("FK_ParentOfStud_Students"),
                    l => l.HasOne<Parent>().WithMany()
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ParentOfStud_Parents"),
                    j =>
                    {
                        j.HasKey("ParentId", "SrudentId");
                        j.ToTable("ParentOfStud");
                    });
        });

        modelBuilder.Entity<RiskGroup>(entity =>
        {
            entity.Property(e => e.Reason)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasOne(d => d.Hostel).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.HostelId)
                .HasConstraintName("FK_Rooms_Hostels");
        });

        modelBuilder.Entity<Speciality>(entity =>
        {
            entity.Property(e => e.Id)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.HasOne(d => d.Division).WithMany(p => p.Specialities)
                .HasForeignKey(d => d.DivisionId)
                .HasConstraintName("FK_Specialities_Divisions");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.Property(e => e.Address)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.GroupId)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Note).IsUnicode(false);
            entity.Property(e => e.Patronymic)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNum)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Group).WithMany(p => p.Students)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK_Students_Groups");

            entity.HasOne(d => d.HealthGroup).WithMany(p => p.Students)
                .HasForeignKey(d => d.HealthGroupId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Students_HealthGroup");

            entity.HasOne(d => d.Room).WithMany(p => p.Students)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Students_Rooms");

            entity.HasMany(d => d.Reasons).WithMany(p => p.Students)
                .UsingEntity<Dictionary<string, object>>(
                    "StudentsOfRiskGroup",
                    r => r.HasOne<RiskGroup>().WithMany()
                        .HasForeignKey("ReasonId")
                        .HasConstraintName("FK_StudentsOfRiskGroup_RiskGroups"),
                    l => l.HasOne<Student>().WithMany()
                        .HasForeignKey("StudentId")
                        .HasConstraintName("FK_StudentsOfRiskGroup_Students"),
                    j =>
                    {
                        j.HasKey("StudentId", "ReasonId");
                        j.ToTable("StudentsOfRiskGroup");
                    });
        });

        modelBuilder.Entity<StudentAssotiation>(entity =>
        {
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StudentsOfEvent>(entity =>
        {
            entity.Property(e => e.DateTime).HasColumnType("datetime");
            entity.Property(e => e.Result)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Event).WithMany(p => p.StudentsOfEvents)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK_StudentsOfEvents_Events");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentsOfEvents)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK_StudentsOfEvents_Students");
        });

        modelBuilder.Entity<StudentsOnPedControl>(entity =>
        {
            entity.ToTable("StudentsOnPedControl");

            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.MeasuresTaken)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Tocid).HasColumnName("TOCId");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentsOnPedControls)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK_StudentsOnPedControl_Students");

            entity.HasOne(d => d.Toc).WithMany(p => p.StudentsOnPedControls)
                .HasForeignKey(d => d.Tocid)
                .HasConstraintName("FK_StudentsOnPedControl_TypeOfCrime");
        });

        modelBuilder.Entity<TypeOfCrime>(entity =>
        {
            entity.ToTable("TypeOfCrime");

            entity.Property(e => e.Name)
                .HasMaxLength(1500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<WorkWithParent>(entity =>
        {
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Questions).IsUnicode(false);

            entity.HasOne(d => d.Parent).WithMany(p => p.WorkWithParents)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK_WorkWithParents_Parents");
        });

        OnModelCreatingPartial(modelBuilder);
       
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
