using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;


public class Students
{
    [Key]
    public int StudentsID { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }
    public DateTime BirthDate { get; set; }
    public string Phone { get; set; }
    public DateTime RegistrationDate { get; set; }
}

public class Group
{
    public int GroupID { get; set; }
    public string GroupName { get; set; }
    public int SpecialtyID { get; set; }
    public int DepartmentID { get; set; }
}

public class Department
{
    public int DepartmentID { get; set; }
    public string DepartmentName { get; set; }
}

public class Specialty
{
    public int SpecialtyID { get; set; }
    public string SpecialtyName { get; set; }
}

public class Course
{
    public int CourseID { get; set; }
    public string CourseName { get; set; }
    public string Subject { get; set; }
}

public class Teacher
{
    public int TeacherID { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }
    public string PhoneNumber { get; set; }
    public int ExperienceYears { get; set; }
}

public class CourseSchedule
{
    public int CourseScheduleID { get; set; }
    public int CourseID { get; set; }
    public int GroupID { get; set; }
    public int TeacherID { get; set; }
    public int Hours { get; set; }
}
public class Context : DbContext
{
   

    public DbSet<Students> Students { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Specialty> Specialties { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseSchedule> CourseSchedules { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-JU5GO11\COURSES;Database=CoursesDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }

}



public class Program
{
    public static void Main()
    {
        using (var context = new Context())
        {
            // Пример создания нового студента
            var newStudent = new Students
            {
                Name = "Иван",
                Surname = "Соколов",
                Patronymic = "Андреевич",
                BirthDate = new DateTime(2003, 02, 11),
                Phone = "89229453402",
                RegistrationDate = DateTime.Now
            };
            context.Students.Add(newStudent);
            context.SaveChanges();

            // Пример чтения списка студентов
            var students = context.Students.ToList();
            foreach (var student in students)
            {
                Console.WriteLine($"{student.Name} {student.Surname}");
            }

            /*  // Пример обновления данных студента
              var studentToUpdate = context.Students.FirstOrDefault(s => s.Name == "Иван");
              if (studentToUpdate != null)
              {
                  studentToUpdate.PhoneNumber = "9876543210";
                  context.SaveChanges();
              }*/

            /* // Пример удаления студента
             var studentToDelete = context.Students.FirstOrDefault(s => s.Name == "))");
             if (studentToDelete != null)
             {
                 context.Students.Remove(studentToDelete);
                 context.SaveChanges();
             }*/
        }
    }
}