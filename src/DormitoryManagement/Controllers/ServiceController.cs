using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DormitoryManagementSystem.Controllers
{
    public class ServiceController : Controller
    {
        // MOCK DATA (sau này thay bằng Database)
        private static List<ServiceViewModel> services = new List<ServiceViewModel>
        {
            new ServiceViewModel { Id="DV001", Name="Internet Tốc Độ Cao", Price=120000, Unit="Tháng / Phòng", Description="Băng thông 100Mbps", UpdatedAt=DateTime.Now.AddDays(-7), Status="Hoạt động" },
            new ServiceViewModel { Id="DV002", Name="Giặt Sấy Tự Động", Price=15000, Unit="Lượt / 7Kg", Description="Máy giặt tầng G", UpdatedAt=DateTime.Now.AddDays(-6), Status="Hoạt động" },
            new ServiceViewModel { Id="DV003", Name="Trông Giữ Xe Máy", Price=90000, Unit="Tháng / Xe", Description="Bãi xe tầng hầm", UpdatedAt=DateTime.Now.AddDays(-5), Status="Hoạt động" }
        };

        private static List<ServiceViewModel> trash = new List<ServiceViewModel>();

        // =========================
        // INDEX
        // =========================
        [HttpGet]
        public IActionResult Index()
        {
            return View(services);
        }

        // =========================
        // CREATE
        // =========================
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ServiceViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            model.Id = "DV" + (services.Count + trash.Count + 1).ToString("000");
            model.UpdatedAt = DateTime.Now;
            model.Status = "Hoạt động";

            services.Add(model);

            return RedirectToAction("Index");
        }

        // =========================
        // EDIT
        // =========================
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var service = services.FirstOrDefault(x => x.Id == id);
            if (service == null) return NotFound();

            return View(service);
        }

        [HttpPost]
        public IActionResult Edit(ServiceViewModel model)
        {
            var service = services.FirstOrDefault(x => x.Id == model.Id);
            if (service == null) return NotFound();

            service.Name = model.Name;
            service.Price = model.Price;
            service.Unit = model.Unit;
            service.Description = model.Description;
            service.Status = model.Status;
            service.UpdatedAt = DateTime.Now;

            return RedirectToAction("Index");
        }

        // =========================
        // TRASH (danh sách thùng rác)
        // =========================
        [HttpGet]
        public IActionResult Trash()
        {
            return View(trash);
        }

        // =========================
        // DELETE (chuyển vào thùng rác)
        // Route: /Service/Trash/{id}
        // =========================
        [HttpGet]
        public IActionResult Trash(string id)
        {
            var service = services.FirstOrDefault(x => x.Id == id);
            if (service == null) return NotFound();

            services.Remove(service);
            trash.Add(service);

            return RedirectToAction("Index");
        }

        // =========================
        // RESTORE từ trash
        // =========================
        public IActionResult Restore(string id)
        {
            var service = trash.FirstOrDefault(x => x.Id == id);
            if (service == null) return NotFound();

            trash.Remove(service);
            services.Add(service);

            return RedirectToAction("Trash");
        }
    }

    // =========================
    // VIEW MODEL
    // =========================
    public class ServiceViewModel
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public required string Unit { get; set; }
        public required string Description { get; set; }
        public DateTime UpdatedAt { get; set; }
        public required string Status { get; set; }
    }
}