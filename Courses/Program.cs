using System;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main()
    {
        using (var context = new Context())
        {
            // Добавить новые данные
            var specialty = new Specialty { SpecialtyName = "Computer Science" };
            context.Specialties.Add(specialty);
            context.SaveChanges();

            var department = new Department { DepartmentName = "IT Department" };
            context.Departments.Add(department);
            context.SaveChanges();

            var group = new Group
            {
                GroupName = "CS101",
                SpecialtyId = specialty.SpecialtyId,
                DepartmentId = department.DepartmentId
            };
            context.Groups.Add(group);
            context.SaveChanges();

            // Получить данные
            var groups = context.Groups
                .Include(g => g.Specialty)
                .Include(g => g.Department)
                .ToList();

            foreach (var g in groups)
            {
                Console.WriteLine($"Group: {g.GroupName}, Specialty: {g.Specialty.SpecialtyName}, Department: {g.Department.DepartmentName}");
            }
        }
    }
}


public class TrainingContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Specialty> Specialties { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseSchedule> CourseSchedules { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=DESKTOP-JU5GO11\COURSES;Database=CoursesDB;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Additional configuration here (e.g., relationships, indexes, etc.)
    }
}
public class Student
{
    public int StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public DateTime BirthDate { get; set; }
    public string PhoneNumber { get; set; }

    public ICollection<Group> Groups { get; set; }
}

public class Group
{
    public int GroupId { get; set; }
    public string GroupName { get; set; }
    public int SpecialtyId { get; set; }
    public int DepartmentId { get; set; }

    public Specialty Specialty { get; set; }
    public Department Department { get; set; }
    public ICollection<Student> Students { get; set; }
    public ICollection<CourseSchedule> CourseSchedules { get; set; }
}

public class Specialty
{
    public int SpecialtyId { get; set; }
    public string SpecialtyName { get; set; }

    public ICollection<Group> Groups { get; set; }
}

public class Department
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; }

    public ICollection<Group> Groups { get; set; }
}

public class Teacher
{
    public int TeacherId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string PhoneNumber { get; set; }
    public int ExperienceYears { get; set; }

    public ICollection<CourseSchedule> CourseSchedules { get; set; }
}

public class Course
{
    public int CourseId { get; set; }
    public string CourseName { get; set; }
    public int LectureTypeId { get; set; }
    public string Subject { get; set; }
    public decimal HourlyRate { get; set; }

    public LectureType LectureType { get; set; }
    public ICollection<CourseSchedule> CourseSchedules { get; set; }
}

public class LectureType
{
    public int LectureTypeId { get; set; }
    public string LectureTypeName { get; set; }

    public ICollection<Course> Courses { get; set; }
}

public class CourseSchedule
{
    public int CourseScheduleId { get; set; }
    public int CourseId { get; set; }
    public int GroupId { get; set; }
    public int TeacherId { get; set; }
    public int Hours { get; set; }

    public Course Course { get; set; }
    public Group Group { get; set; }
    public Teacher Teacher { get; set; }
}