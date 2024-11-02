using Karim.CRUD.BLL.ModelDtos.EmployeeDtos;
using Karim.CRUD.BLL.Services.EmployeeServices;
using Karim.CRUD.BLL.ThirdPartyServices.AttachmentService;
using Karim.CRUD.DAL.Entities.EmployeeModel;
using Karim.CRUD.PL.Models.EmployeeVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Karim.CRUD.PL.Controllers
{
    [Authorize]
    public class EmployeeController(IAttachmentService _attachmentService, IEmployeeService _employeeService, ILogger<EmployeeController> _logger) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index(string searchQuery)
        {
            var Employees = await _employeeService.GetAllNotDeletedEmployees();
            if (!string.IsNullOrEmpty(searchQuery))
                Employees = Employees.Where(E => E.FullName.ToLower().Contains(searchQuery.ToLower()));

            return View(Employees);
        }

        [HttpGet]
        public async Task<IActionResult> DeletedEmpList()
        {
            var DeletedEmployeeList = await _employeeService.GetAllDeletedEmployees();
            return View(DeletedEmployeeList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUpdateEmpsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "This Data Is Invalid, Please Fill All Inputs With Valid Data");
                return View(model);
            }
            int Employee;
            try
            {
                var MappedEmployee = new EmployeeCreationDto()
                {
                    DepartmentId = model.DepartmentId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    Age = model.Age,
                    Email = model.Email,
                    EmployeeWorkType = model.EmployeeWorkType,
                    Gender = model.Gender,
                    IsActive = model.IsActive,
                    PictureFile = model.PictureFile
                };
                Employee = await _employeeService.CreateEmployee(MappedEmployee);
                if (Employee > 0)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An Error Occured While Creating The Employee");
                _logger.LogError(ex, ex.Message);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? Id)
        {
            if (Id == null) return BadRequest();
            var Employee = await _employeeService.GetEmployeeById(Id.Value);
            if (Employee == null) return NotFound();
            var MappedEmp = new CreateUpdateEmpsViewModel()
            {
                Id = Id.Value,
                FirstName = Employee.FullName.Split(' ')[0],
                LastName = Employee.FullName.Split(' ')[1],
                Address = Employee.Address,
                Age= Employee.Age,
                Email = Employee.Email,
                EmployeeWorkType = Employee.EmployeeWorkType,
                Gender = Employee.Gender,
                IsActive = Employee.IsActive,
                DepartmentId = Employee.DepartmentId
            };
            TempData["PicUrl"] = Employee.PictureUrl;
            return View(MappedEmp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute] int? Id, CreateUpdateEmpsViewModel model)
        {
            if(Id == null) return BadRequest();
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "The Data You Have Provided Is Not Valid, Please Try Again With Valid Data");
                return View(model);
            }
            int UpdatedEmployee;
            try
            {
                var MappedEmployee = new EmployeeUpdateDto()
                {
                    Id = Id.Value,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    Age = model.Age,
                    Email = model.Email,
                    EmployeeWorkType = model.EmployeeWorkType,
                    Gender = model.Gender,
                    IsActive = model.IsActive,
                    DepartmentId = model.DepartmentId,
                    PictureFile = model.PictureFile
                    
                };
                if (model.PictureFile is not null)
                {
                    if (TempData["PicUrl"] is not null)
                    {
                        _attachmentService.DeleteImages(TempData["PicUrl"] as string ?? "N/A");
                    }
                    MappedEmployee.PictureUrl =  await _attachmentService.UploadImages(model.PictureFile, "employees");
                }

                UpdatedEmployee = await _employeeService.UpdateEmployee(MappedEmployee);
                if (UpdatedEmployee > 0)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An Error Occured While Updating The Employee");
                _logger.LogError(ex, ex.Message);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? Id)
        {
            if(Id == null) return BadRequest();
            var Employee = await _employeeService.GetEmployeeById(Id.Value);
            if (Employee == null) return NotFound();
            return View(Employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SoftDelete(int? Id)
        {
            if (Id == null) return BadRequest();
            var Emp = await _employeeService.GetEmployeeById(Id.Value);
            if (Emp == null) return NotFound();
            bool DeletedEmp;
            try
            {
                DeletedEmp = await _employeeService.DeleteEmployeeSoftly(Id.Value);
                if (DeletedEmp)
                    return RedirectToAction(nameof(DeletedEmpList));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An Error Occured While Deleting The Employee");
                _logger.LogError(ex, ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HardDelete(int? Id)
        {
            if(Id is null) return BadRequest();
            var Emp = await _employeeService.GetEmployeeById(Id.Value);
            if (Emp == null) return NotFound();
            bool DeletedEmp;
            try
            {
                DeletedEmp = await _employeeService.DeleteEmployeeHardly(Id.Value);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An Error Occured While Deleting The Employee, Please Try Again Later");
                _logger.LogError(ex, ex.Message);
            }
            return RedirectToAction(nameof(DeletedEmpList));
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore(int? Id)
        {
            if(Id is null) return BadRequest();
            var Emp = await _employeeService.GetEmployeeById(Id.Value);
            if (Emp is null) return NotFound();
            bool RestoredEmp;
            try
            {
                RestoredEmp = await _employeeService.RestoringDeletedEmployee(Id.Value);
                if (RestoredEmp) return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An Error Occured While Restoring The Employee, Please Try Again Later");
                _logger.LogError(ex, ex.Message);
            }
            return RedirectToAction(nameof(DeletedEmpList));
        }
    }
}
