using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Domain.Modells
{
    public class Book
    {
        public int Id { get; set; }
        public required string BookName { get; set; }
        public required string Description { get; set; } 
        public required string Author { get; set; } 
        public int Price { get; set; }
        public int PageCount { get; set; }
        public DateTime Created { get; set; }

  
    }
}
