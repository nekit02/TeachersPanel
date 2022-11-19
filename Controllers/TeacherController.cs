using AdminPanel.Models;
using AdminPanel.Models.AdminPanel.Models;
using AdminPanel.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace AdminPanel.Controllers
{
    public class TeacherController : Controller
    {
        IWebHostEnvironment _environment;

        public TeacherService TeacherService { get; }

        TeacherService _TeacherService;
        public TeacherController(ApplicationContext context, IWebHostEnvironment env)
        {
            _environment = env;
            TeacherService = new TeacherService(context);
        }
        public async Task<IActionResult> Index()
        {
            var listTeacher = TeacherService.GetAll();
            return View(listTeacher);
        }

        public IActionResult Edit(int TeacherId)
        {
            //получить из сервиса 1 новость по айди
            var OneTeacher = TeacherService.GetDetails(TeacherId);
            //передать во вьюшку
            return View(OneTeacher);
        }

        [HttpPost]
        public IActionResult Edit([FromForm] Teachers teacher)
        {
            TeacherService.Edit(teacher);
            return RedirectToAction("Index");
        }
        public IActionResult DeleteNews([FromForm] Teachers teacher)
        {
            //Получить айди новости для удаления
            TeacherService.Delete(teacher);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Create(string teacherName, string teacherSurname, IFormFileCollection uploadedFiles)
        {
            //создаем новость через экземпляр класса news
            var teacher = new Teachers { Name = teacherName, Surname = teacherSurname };
            var TeacherID = TeacherService.Create(teacher);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
