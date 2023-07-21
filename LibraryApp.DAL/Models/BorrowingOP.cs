using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DAL.Models
{
    public class BorrowingOP
    {
        public int Id { get; set; }
        public Book? Book { get; set; }

        [ForeignKey("Book")]
        public int Book_Id { get; set; }

        public Borrower? Borrower { get; set; }

        [ForeignKey("Borrower")]
        public int Borrower_Id { get; set; }


    }
}
