using Karim.CRUD.DAL._Common._Enums;
using Karim.CRUD.DAL.Entities.DepartmentModel;
using Karim.CRUD.DAL.Entities.EmployeeModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karim.CRUD.DAL.Persistence.Data.Configurations
{
    public class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            //Entity Private Property Configuration
            builder.Property(E => E.FirstName).HasColumnType("varchar(20)").IsRequired();
            builder.Property(E => E.LastName).HasColumnType("varchar(20)").IsRequired();
            builder.Property(E => E.Email).HasColumnType("varchar(Max)").IsRequired();
            builder.Property(E => E.Address).HasColumnType("varchar(Max)").IsRequired();
            builder.Property(E => E.EmployeeWorkType)
                .HasConversion(
                    EmpType => EmpType.ToString(), //This Is For Storing The Enum As String In DB
                    EmpType => (EmployeeWorkType) Enum.Parse(typeof(EmployeeWorkType), EmpType) //This Is For Retriving The Enum That Is String To Enum Value From DB
                );
            builder.Property(E => E.Gender)
                .HasConversion(
                    Gender => Gender.ToString(),//This Is For Storing The Enum As String In DB
                    Gender => (Gender) Enum.Parse(typeof(Gender), Gender) //This Is For Retriving The Enum That Is String To Enum Value From DB
                    );


            //Entity Common Property Configuration
            builder.Property(X => X.Id).UseIdentityColumn(10, 10).IsRequired();
            builder.Property(X => X.CreatedOn).HasDefaultValueSql("GETDATE()");
            builder.Property(X => X.LastModifiedOn).HasComputedColumnSql("GETDATE()");


            //Relationships Mapping

            //1. Work Relationship Between Emps And Depts
            builder.HasOne(E => E.Department).WithMany(D => D.Employees).HasForeignKey(E => E.DepartmentId).OnDelete(DeleteBehavior.SetNull);

            //2. Manage Relationship Between Emps And Depts
            builder.HasOne(E => E.ManagedDepartment).WithOne(D => D.Manager).HasForeignKey<Department>(D => D.ManagerId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
