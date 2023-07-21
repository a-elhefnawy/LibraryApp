using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DAL.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }

        public List<BorrowingOP> OPs { get; set; }=new List<BorrowingOP>();
    }
}
