using AutoMapper;
using CollegeGrades.Core.Entities;
using CollegeGrades.Core.Interfaces;
using CollegeGrades.Web.Models.TeacherViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CollegeGrades.Web.Controllers
{
    public class TeachersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TeachersController(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View(_unitOfWork.Teachers.ListAll());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _unitOfWork.Teachers.GetByIdAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<Teacher, DisplayTeacherViewModel>(teacher));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTeacherViewModel model)
        {
            Teacher teacher = _mapper.Map<CreateTeacherViewModel, Teacher>(model);
            teacher.ID = Guid.NewGuid().ToString();

            await _unitOfWork.Teachers.AddAsync(teacher);
            await _unitOfWork.CompletedAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _unitOfWork.Teachers.GetByIdAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<Teacher, EditTeacherViewModel>(teacher));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, Teacher teacher)
        {
            if (id != teacher.ID)
            {
                return NotFound();
            }

           

            await _unitOfWork.Teachers.UpdateAsync(teacher);
            await _unitOfWork.CompletedAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _unitOfWork.Teachers.GetByIdAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var teacher = await _unitOfWork.Teachers.GetByIdAsync(id);

            await _unitOfWork.Teachers.DeleteAsync(teacher);
            await _unitOfWork.CompletedAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}