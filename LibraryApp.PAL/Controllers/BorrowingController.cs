using LibraryApp.BAL.Interfaces;
using LibraryApp.DAL.Models;
using LibraryApp.PAL.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryApp.PAL.Controllers
{
    [Authorize]
    public class BorrowingController : Controller
    {
        private readonly ICrudOperations<BorrowingOP> cb;
        private readonly ICrudOperations<Book> bookRepo;
        private readonly ICrudOperations<Borrower> borrowerRepo;

        public BorrowingController(ICrudOperations<BorrowingOP> _cb,ICrudOperations<Book> _bookRepo,ICrudOperations<Borrower> _borrowerRepo)
        {
            cb = _cb;
            bookRepo = _bookRepo;
            borrowerRepo = _borrowerRepo;
        }
        public async Task<IActionResult> Index()
        {
            List<BorrowingVM> VMs = new List<BorrowingVM>();
            var ops= await cb.GetAll();

            foreach(var item in ops)
            {
                BorrowingVM vm = new BorrowingVM()
                {
                    Id = item.Id,
                    Book_Name = item.Book.Name,
                    Borrower_Name=item.Borrower.Name
                };
                VMs.Add(vm);
            }
            
            return View(VMs);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Books= new SelectList(await bookRepo.GetAll(),"Id","Name");
            ViewBag.borrowers = new SelectList(await borrowerRepo.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BorrowingOP ops)
        {
            ViewBag.Books = new SelectList(await bookRepo.GetAll(), "Id", "Name");
            ViewBag.borrowers = new SelectList(await borrowerRepo.GetAll(), "Id", "Name");

            var book = await bookRepo.Get(ops.Book_Id);
            if (book.Stock==0)
            {
                ModelState.AddModelError("", "Sorry this Book is out of Stock for now");
                return View(ops);
            }
            else
            {
                var result = await cb.Add(ops);
                if (result != null)
                {
                    book.Stock--;
                    bookRepo.Update(book);
                    return RedirectToAction("Index");
                }
                return View(ops);
            }
        }
        public IActionResult ReturnBook() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ReturnBook(ReturnVM vm) 
        {
            var borrowing= await cb.Get(vm.Id);
            if (borrowing == null)
            {
                ModelState.AddModelError("", "There is no Book Borrowed with this OP Id");
                return View (vm);
            }
            var book = await bookRepo.Get(borrowing.Book_Id);
            book.Stock++;
            bookRepo.Update(book);
            cb.Delete(borrowing);
            return RedirectToAction ("Index");
        }


    }
}
