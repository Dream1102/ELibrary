using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Domain.Modells
{
    public class BookShop
    {
        public int ShoopID { get; set; }
        public required string ShoopName { get; set; }
        public required string Adress { get; set; }
        public Boolean IsReturnet { get; set; } = true;
    }
}
