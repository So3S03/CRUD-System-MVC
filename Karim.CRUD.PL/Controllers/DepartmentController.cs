using Karim.CRUD.BLL.ModelDtos.DepartmentDtos;
using Karim.CRUD.BLL.Services.DepartmentServices;
using Karim.CRUD.PL.Models.DepartmentVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Karim.CRUD.PL.Controllers
{
    [Authorize]
    public class DepartmentController(IDepartmentService _departmentService, ILogger<DepartmentController> _logger) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index(string searchQuery)
        {
            var Departments = await _departmentService.GetAllNotDeletedDepartment();
            if (!string.IsNullOrEmpty(searchQuery))
               Departments = Departments.Where(D => D.Name.ToLower().Contains(searchQuery.ToLower()));

            if (Departments.Count() < 0 || Departments is null) ModelState.AddModelError(string.Empty, "This Table Has No Data");
            return View(Departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Some Data is Not Valid For Creating The Departmnet Please Try Again");
                return View(model);
            }
            var MappedModel = new DepartmentCreationDto()
            {
                Code = model.Code,
                Name = model.Name,
                DepartmentCreationDate = model.DepartmentCreationDate,
                Description = model.Description,
                ManagerId = model.ManagerId
            };
            int Result;
            try
            {
                Result = await _departmentService.CreateDepartment(MappedModel);
                if (Result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);
                
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Restore()
        {
            var DeletedDepartment = await _departmentService.GetAllDeletedDepartment();
            return View(DeletedDepartment);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RestoreDepartment(int? Id)
        {
            if (Id == null) return BadRequest();
            var Department = await _departmentService.GetDepartmentById(Id.Value);
            if (Department == null) return NotFound();
            bool IsRestored;
            try
            {
                IsRestored = await _departmentService.RestoringDeletedDepartment(Id.Value);
                if (IsRestored) return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                ModelState.AddModelError(string.Empty, "An Error Occured During Restore The Department Please Try Again");
            }
            return View(nameof(Restore));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? Id)
        {
            if(Id is null) return BadRequest();
            var Department = await _departmentService.GetDepartmentById(Id.Value);
            if (Department is null) return NotFound();
            var MappedDepartmnet = new CreateUpdateViewModel()
            {
                Id = Id.Value,
                Code = Department.Code,
                Name = Department.Name,
                Description = Department.Description,
                DepartmentCreationDate = Department.DateOfCreation,
                ManagerId = Department.ManagerId
            };
            return View(MappedDepartmnet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute] int Id ,CreateUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "The Data You Have Entered Is Invalid");
                return View(model);
            }
            var MappedDepartmnet = new DepartmentUpdateDto()
            {
                Id= Id,
                Code= model.Code,
                Name= model.Name,
                Description= model.Description,
                DepartmentCreationDate = model.DepartmentCreationDate,
                ManagerId= model.ManagerId
            };
            int UpdatedDepartmnet;
            try
            {
                UpdatedDepartmnet = await _departmentService.UpdateDepartment(MappedDepartmnet);
                if (UpdatedDepartmnet > 0)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                _logger.LogError(ex, ex.Message);
            }
            return View(MappedDepartmnet);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null) return BadRequest();
            var Department = await _departmentService.GetDepartmentById(Id.Value);
            if (Department == null) return NotFound();
            return View(Department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SoftDelete(int? Id)
        {
            if(Id is null) return BadRequest();
            var Department = await _departmentService.GetDepartmentById(Id.Value);
            if (Department is null) return NotFound();
            bool DeletedDepartment;
            try
            {
                DeletedDepartment = await _departmentService.DeleteDepartmentSoftly(Id.Value);
                if (DeletedDepartment) return RedirectToAction(nameof(Restore));

            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                _logger.LogError(ex, ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HardDelete(int? Id)
        {
            if (Id is null) return BadRequest();
            var Department = await _departmentService.GetDepartmentById(Id.Value);
            if (Department is null) return NotFound();
            bool DeletedDepartment;
            try
            {
                DeletedDepartment = await _departmentService.DeleteDepartmentHardly(Id.Value);
                if (DeletedDepartment) return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                _logger.LogError(ex, ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
