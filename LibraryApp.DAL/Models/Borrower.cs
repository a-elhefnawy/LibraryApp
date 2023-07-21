using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DAL.Models
{
    public class Borrower
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BorrowingOP> OPs { get; set; } = new List<BorrowingOP>();

    }
}
