using Karim.CRUD.BLL.ModelDtos.DepartmentDtos;
using Karim.CRUD.BLL.ModelDtos.EmployeeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karim.CRUD.BLL.Services.EmployeeServices
{
    public interface IEmployeeService
    {
        Task<IEnumerable<GetAllEmployeeDto>> GetAllNotDeletedEmployees();
        Task<IEnumerable<GetAllEmployeeDto>> GetAllDeletedEmployees();
        Task<EmployeeDetailsDto> GetEmployeeById(int id);
        Task<int> CreateEmployee(EmployeeCreationDto employeeCreationDto);
        Task<int> UpdateEmployee(EmployeeUpdateDto employeeUpdateDto);
        Task<bool> DeleteEmployeeSoftly(int id);
        Task<bool> DeleteEmployeeHardly(int id);
        Task<bool> RestoringDeletedEmployee(int id);
    }
}
