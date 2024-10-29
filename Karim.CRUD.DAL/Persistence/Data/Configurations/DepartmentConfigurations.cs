using Karim.CRUD.DAL.Entities.DepartmentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karim.CRUD.DAL.Persistence.Data.Configurations
{
    public class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            //Entity Private Property Configuration
            builder.Property(D => D.Code).HasColumnType("varchar(MAX)").IsRequired();
            builder.Property(D => D.Name).HasColumnType("varchar(MAX)").IsRequired();
            builder.Property(D => D.Description).HasColumnType("varchar(MAX)");


            //Entity Common Property Configuration
            builder.Property(X => X.Id).UseIdentityColumn(10,10).IsRequired();
            builder.Property(X => X.CreatedOn).HasDefaultValueSql("GETDATE()"); 
            builder.Property(X => X.LastModifiedOn).HasComputedColumnSql("GETDATE()");

            // ******* Notes ******
            //The Diffrencece Between HasDefaultValueSql & HasComputedColumnSql
            // 1. HasDefaultValueSql: This Store The Value On The Creation Only And Don't Allow Any Modification After That
            // 1. HasComputedColumnSql: Every Time When Change Happen The Column Will Auto Change It's Value

            // * "Convert(date, GETDATE())" => This Will Store Only Date
            // * "GETDATE()" => This Will Store The Date and Time



            //Relationships Mapping

            //1. Work Relationship Between Emps And Depts
            builder.HasMany(D => D.Employees).WithOne(E => E.Department).HasForeignKey(E => E.DepartmentId).OnDelete(DeleteBehavior.SetNull);

            //2. Manage Relationship Between Emps And Depts
            builder.HasOne(D => D.Manager).WithOne(E => E.ManagedDepartment).HasForeignKey<Department>(D => D.ManagerId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
