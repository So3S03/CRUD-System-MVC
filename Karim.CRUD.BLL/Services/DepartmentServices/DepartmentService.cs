using Karim.CRUD.BLL.ModelDtos.DepartmentDtos;
using Karim.CRUD.DAL.Entities.DepartmentModel;
using Karim.CRUD.DAL.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Karim.CRUD.BLL.Services.DepartmentServices
{
	public class DepartmentService : IDepartmentService
    {
        #region Services
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        //This is For Representing The Main Tables
        #region GetAllDepartments [Deleted | Not Deleted]
        public async Task<IEnumerable<GetAllDepartmentDto>> GetAllNotDeletedDepartment()
        {
            var Departments = await _unitOfWork.GetRepository<Department>().GetAllIQueryable().Select(D => new GetAllDepartmentDto()
            {
                Id = D.Id,
                Code = D.Code,
                Name = D.Name,
                SmallDescription = D.Description != null ? string.Join(" ", D.Description.Split(new[] { ' ' }, 2)) : string.Empty,
                DepartmentCreationDate = D.DateOfCreation,
                Manager = $"{D.Manager.FirstName} {D.Manager.LastName}"
            }).ToListAsync();
            await _unitOfWork.CompleteAsync();
            return Departments;
        }

        public async Task<IEnumerable<DeletedDepartmentDto>> GetAllDeletedDepartment()
        {
            var Department = await _unitOfWork.GetRepository<Department>().GetAllDeletedIQueryable().Select(D => new DeletedDepartmentDto()
            {
                Id = D.Id,
                Code = D.Code,
                Name = D.Name
            }).ToListAsync();
            await _unitOfWork.CompleteAsync();
            return Department;
        }

        #endregion

        //This is For Representing The Department Details
        #region GetDepartmentById
        public async Task<DepartmentDetailsDto> GetDepartmentById(int id)
        {
            var Department = await _unitOfWork.GetRepository<Department>().GetByIdAsync(id);
            if (Department == null) return null!;
            var MappedDepartment = new DepartmentDetailsDto()
            {
                Id = id,
                Code = Department.Code,
                Name = Department.Name,
                Description = Department.Description,
                DateOfCreation = Department.DateOfCreation,
                CreatedBy = 1,
                Manager = $"{Department.Manager?.FirstName ?? "N/A"} {Department.Manager?.LastName ?? "N/A"}",
                ManagerId = Department.ManagerId,
                LastModifiedBy = 1,
                CreatedOn = Department.CreatedOn,
                LastModifiedOn = Department.LastModifiedOn,
                IsDeleted = Department.IsDeleted
            };
            return MappedDepartment;
        }

        #endregion

        //This is For Creating A Department
        #region CreateDepartment
        public async Task<int> CreateDepartment(DepartmentCreationDto department)
        {
            if (department == null) return 0;
            var MappedDepartment = new Department() 
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                DateOfCreation = department.DepartmentCreationDate,
                CreatedBy = 1,
                CreatedOn = DateTime.UtcNow, //For Record Creation
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
                IsDeleted = false,
                ManagerId = department.ManagerId
            };
            _unitOfWork.GetRepository<Department>().Create(MappedDepartment);
            return await _unitOfWork.CompleteAsync();
        }
        #endregion

        //This is For Updating The Departmment
        #region UpdateDepartment
        public async Task<int> UpdateDepartment(DepartmentUpdateDto department)
        {
            var MappedDepartment = new Department()
            {
                Id = department.Id,
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                IsDeleted = false,
                ManagerId = department.ManagerId,
                CreatedBy = 1,
                CreatedOn = DateTime.UtcNow,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
                DateOfCreation = department.DepartmentCreationDate
            };
            _unitOfWork.GetRepository<Department>().Update(MappedDepartment);
            return await _unitOfWork.CompleteAsync();
        }
        #endregion

        //This is For Deleting Department [Soft | Hard]
        #region DeleteDepartment
        public async Task<bool> DeleteDepartmentSoftly(int id)
        {
            var Department = await _unitOfWork.GetRepository<Department>().GetByIdAsync(id);
            if (Department == null) return false;
            _unitOfWork.GetRepository<Department>().SoftDelete(Department);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> DeleteDepartmentHardly(int id)
        {
            var Department = await _unitOfWork.GetRepository<Department>().GetByIdAsync(id);
            if (Department == null) return false;
            _unitOfWork.GetRepository<Department>().HardDelete(Department);
            return await _unitOfWork.CompleteAsync() > 0;
        }
        #endregion

        //This is For Restoring Deleted Departments
        #region RestoringDeletedDepartment
        public async Task<bool> RestoringDeletedDepartment(int id)
        {
            var Department = await _unitOfWork.GetRepository<Department>().GetByIdAsync(id);
            if (Department == null) return false;
            _unitOfWork.GetRepository<Department>().ResotoringSoftDeletedEntities(Department);
            return await _unitOfWork.CompleteAsync() > 0;
        }
        #endregion
    }
}
