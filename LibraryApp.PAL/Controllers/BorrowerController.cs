using LibraryApp.BAL.Interfaces;
using LibraryApp.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.PAL.Controllers
{
    [Authorize]
    public class BorrowerController : Controller
    {
        private readonly ICrudOperations<Borrower> cb;

        public BorrowerController(ICrudOperations<Borrower> _cb)
        {
            cb = _cb;
        }
        public async Task<IActionResult> Index()
        {
            var Borrowers = await cb.GetAll();
            return View(Borrowers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                await cb.Add(borrower);
                return RedirectToAction("Index");
            }
            return View(borrower);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var borrower = await cb.Get(id);
            if (borrower == null) { return NotFound(); }
            return View(borrower);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                cb.Update(borrower);
                return RedirectToAction("Index");
            }
            return View(borrower);
        }

        public async Task<IActionResult> Delete(int id)
        {
                var borrower = await cb.Get(id);
                if (borrower == null) { return NotFound(); };
                cb.Delete(borrower);
                return RedirectToAction("Index");
        }
    }
}
