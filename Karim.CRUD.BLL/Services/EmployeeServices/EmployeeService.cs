using Karim.CRUD.BLL.ModelDtos.DepartmentDtos;
using Karim.CRUD.BLL.ModelDtos.EmployeeDtos;
using Karim.CRUD.BLL.ThirdPartyServices.AttachmentService;
using Karim.CRUD.DAL.Entities.EmployeeModel;
using Karim.CRUD.DAL.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karim.CRUD.BLL.Services.EmployeeServices
{
    public class EmployeeService(IUnitOfWork _unitOfWork, IAttachmentService _attachmentService) : IEmployeeService
    {
        //This is For Getting All Employess   
        #region GetAllEmployees [Deleted | NotDeleted]
        public async Task<IEnumerable<GetAllEmployeeDto>> GetAllNotDeletedEmployees()
        {
            var Employees = await _unitOfWork.GetRepository<Employee>().GetAllIQueryable().Select(E => new GetAllEmployeeDto()
            {
                Id = E.Id,
                FullName = $"{E.FirstName} {E.LastName}",
                Age = E.Age,
                Address = E.Address,
                Email = E.Email,
                EmployeeWorkType = E.EmployeeWorkType.ToString(),
                IsActive = E.IsActive,
                Gender = E.Gender.ToString(),
                Department = E.Department.Name,
                PictureUrl = E.PictureUrl
            }).ToListAsync();
            return Employees;
        }

        public async Task<IEnumerable<GetAllEmployeeDto>> GetAllDeletedEmployees()
        {
            var Employees = await _unitOfWork.GetRepository<Employee>().GetAllDeletedIQueryable().Select(E => new GetAllEmployeeDto()
            {
                Id= E.Id,
                FullName = $"{E.FirstName} {E.LastName}",
                Age = E.Age,
                Address = E.Address,
                Email = E.Email,
                EmployeeWorkType= E.EmployeeWorkType.ToString(),
                IsActive = E.IsActive,
                Gender = E.Gender.ToString(),
                Department = E.Department.Name,
                PictureUrl = E.PictureUrl
                
            }).ToListAsync();
            return Employees;
        }

        #endregion

        //This is For Getting Employee By Id
        #region GetEmployeeById
        public async Task<EmployeeDetailsDto> GetEmployeeById(int id)
        {
            var Employee = await _unitOfWork.GetRepository<Employee>().GetByIdAsync(id);
            if (Employee == null) return null!;
            var MappedEmployee = new EmployeeDetailsDto()
            {
                Id = id,
                FullName = $"{Employee.FirstName} {Employee.LastName}",
                Age = Employee.Age,
                Address = Employee.Address,
                Email = Employee.Email,
                CreatedBy = Employee.CreatedBy,
                LastModifiedBy = Employee.LastModifiedBy,
                CreatedOn = Employee.CreatedOn,
                Department = Employee.Department.Name,
                DepartmentId = Employee.DepartmentId,
                EmployeeWorkType = Employee.EmployeeWorkType,
                IsActive = Employee.IsActive,
                Gender = Employee.Gender,
                LastModifiedOn = Employee.LastModifiedOn,
                PictureUrl= Employee.PictureUrl
            };
            return MappedEmployee;
        }
        #endregion

        //This is For Creating An Employee
        #region CreateEmployee
        public async Task<int> CreateEmployee(EmployeeCreationDto employeeCreationDto)
        {
            var MappedEmployee = new Employee()
            {
                FirstName = employeeCreationDto.FirstName,
                LastName = employeeCreationDto.LastName,
                Age = employeeCreationDto.Age,
                Address = employeeCreationDto.Address,
                Email = employeeCreationDto.Email,
                IsActive= employeeCreationDto.IsActive,
                Gender = employeeCreationDto.Gender,
                EmployeeWorkType = employeeCreationDto.EmployeeWorkType,
                CreatedBy = 1,
                LastModifiedBy = 1,
                CreatedOn = DateTime.UtcNow,
                LastModifiedOn = DateTime.UtcNow,
                IsDeleted = false,
                DepartmentId = employeeCreationDto.DepartmentId
            };

            if (employeeCreationDto.PictureFile is not null)
            {
                var PictureUrl = await _attachmentService.UploadImages(employeeCreationDto.PictureFile, "employees");
                MappedEmployee.PictureUrl = PictureUrl;
            }

            _unitOfWork.GetRepository<Employee>().Create(MappedEmployee);
            return await _unitOfWork.CompleteAsync();
        }
        #endregion

        //This is For Update An Employee
        #region UpdateEmployee
        public async Task<int> UpdateEmployee(EmployeeUpdateDto employeeUpdateDto)
        {
            var MappedEmployee = new Employee() 
            {
                Id = employeeUpdateDto.Id,
                FirstName = employeeUpdateDto.FirstName,
                LastName = employeeUpdateDto.LastName,
                Age = employeeUpdateDto.Age,
                Address = employeeUpdateDto.Address,
                Email = employeeUpdateDto.Email,
                IsActive = employeeUpdateDto.IsActive,
                Gender = employeeUpdateDto.Gender,
                EmployeeWorkType = employeeUpdateDto.EmployeeWorkType,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
                IsDeleted = false,
                DepartmentId = employeeUpdateDto.DepartmentId,
                PictureUrl = employeeUpdateDto.PictureUrl,
            };
            _unitOfWork.GetRepository<Employee>().Update(MappedEmployee);
            return await _unitOfWork.CompleteAsync();
        }
        #endregion

        //This is For Deleting Employee
        #region DeleteEmployee
        public async Task<bool> DeleteEmployeeSoftly(int id)
        {
            var Emp = await _unitOfWork.GetRepository<Employee>().GetByIdAsync(id);
            if (Emp is null) return false;
            _unitOfWork.GetRepository<Employee>().SoftDelete(Emp);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> DeleteEmployeeHardly(int id)
        {
            var Emp = await _unitOfWork.GetRepository<Employee>().GetByIdAsync(id);
            if (Emp is null) return false;
            _unitOfWork.GetRepository<Employee>().HardDelete(Emp);
            return await _unitOfWork.CompleteAsync() > 0;
        }
        #endregion

        //This Is For Resoring Soft Deleted Employee
        #region ResoringSoftDeletedEmployee
        public async Task<bool> RestoringDeletedEmployee(int id)
        {
            var Emp = await _unitOfWork.GetRepository<Employee>().GetByIdAsync(id);
            if (Emp is null) return false;
            _unitOfWork.GetRepository<Employee>().ResotoringSoftDeletedEntities(Emp);
            return await _unitOfWork.CompleteAsync() > 0;
        } 
        #endregion

    }
}
