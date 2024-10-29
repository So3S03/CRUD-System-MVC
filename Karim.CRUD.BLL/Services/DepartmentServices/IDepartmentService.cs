using Karim.CRUD.BLL.ModelDtos.DepartmentDtos;
using Karim.CRUD.DAL.Entities.DepartmentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karim.CRUD.BLL.Services.DepartmentServices
{
    public interface IDepartmentService
    {
        Task<IEnumerable<GetAllDepartmentDto>> GetAllNotDeletedDepartment();
        Task<IEnumerable<DeletedDepartmentDto>> GetAllDeletedDepartment();
        Task<DepartmentDetailsDto> GetDepartmentById(int id);
        Task<int> CreateDepartment(DepartmentCreationDto department);
        Task<int> UpdateDepartment(DepartmentUpdateDto department);
        Task<bool> DeleteDepartmentSoftly(int id);
        Task<bool> DeleteDepartmentHardly(int id);
        Task<bool> RestoringDeletedDepartment(int id);
    }
}
