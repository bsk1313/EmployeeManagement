using Microsoft.EntityFrameworkCore;

namespace EmployeeManagment.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                    new Employee
                    {
                        Id = 1,
                        Name = "Baljeet Singh Kochhar",
                        Email = "baljeetsinghkochhar13@gmail.com",
                        Department = Dept.CEO
                    }
                );
        }
    }
}
