using LibraryApp.DAL.Models;
using LibraryApp.BAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LibraryApp.DAL.Data;
using LibraryApp.PAL.ViewModel;

namespace LibraryApp.PAL.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly ICrudOperations<Book> cb;
        private readonly LibraryContext db;

        public BookController(ICrudOperations<Book> _cb,LibraryContext _db)
        {
            cb = _cb;
            db = _db;
        }
        public async Task<IActionResult> Index()
        {
            var Books = await cb.GetAll();
            List<BookVM> Models = new List<BookVM>();
            foreach(var item in Books)
            {
                var num = db.BorrowingOPs.Where(x => x.Book_Id==item.Id).Count();

                BookVM Model = new BookVM()
                {
                    Id=item.Id, 
                    Name=item.Name,
                    Stock=item.Stock,
                    Borrowed=num
                };
                Models.Add(Model);
            }
            return View(Models);
        }

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if(ModelState.IsValid)
            {
                await cb.Add(book);
                return RedirectToAction("Index");
            }
            return View(book);
        }

        public async Task<IActionResult> Edit(int id) 
        {
            var book = await cb.Get(id);
            if(book== null) { return NotFound(); }
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Book book) 
        {
            if(ModelState.IsValid)
            {
                cb.Update(book);
                return RedirectToAction("Index");
            }
            return View(book);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var book= await cb.Get(id);
            if(book==null) { return NotFound(); };
            cb.Delete(book);
            return RedirectToAction("Index");
        }
    }
}
