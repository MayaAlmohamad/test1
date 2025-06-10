using Microsoft.AspNetCore.Mvc;
using MvcTodoApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace MvcTodoApp.Controllers
{
    public class HomeController : Controller
    {
        // قاعدة بيانات مؤقتة (في الذاكرة)
        private static List<TaskItem> tasks = new List<TaskItem>
        {
            new TaskItem { Id = 1, Title = "تدرب على MVC Design Pattern", IsComplete = false },
            new TaskItem { Id = 2, Title = "تدرب على N-tier Architecture", IsComplete = false },
            new TaskItem { Id = 3, Title = "تدرب على استخدام git", IsComplete = false },
        };

        /// <summary>
        /// عرض قائمة المهام
        /// </summary>
        public IActionResult Index()
        {
            return View(tasks);
        }

        /// <summary>
        /// إضافة مهمة جديدة
        /// </summary>
        [HttpPost]
        public IActionResult AddTask(string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                int newId = tasks.Any() ? tasks.Max(t => t.Id) + 1 : 1;
                var newTask = new TaskItem { Id = newId, Title = title, IsComplete = false };
                tasks.Add(newTask);
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// تحديث حالة المهمة إلى مكتملة
        /// </summary>
        [HttpPost]
        public IActionResult CompleteTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.IsComplete = true;
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// تحديث عنوان أو حالة المهمة
        /// </summary>
        [HttpPost]
        public IActionResult EditTask(int id, string newTitle, bool? isComplete)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                if (!string.IsNullOrEmpty(newTitle))
                    task.Title = newTitle;

                if (isComplete.HasValue)
                    task.IsComplete = isComplete.Value;
            }
            return RedirectToAction("Index");
        }
    }
}
