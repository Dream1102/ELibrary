using ELibrary.Apllication;
using ELibrary.Domain.Modells;
using ELibrary.Infrastucture;

namespace ELibraryPrsentation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EBookDBContext _db = new EBookDBContext();
            _db.CreateDb();

            Console.WriteLine("Enter table operation to perform :");
            Console.WriteLine("1. Book");
            Console.WriteLine("2. BookShop");

            string tanlan = Console.ReadLine();
            int tanlang;
            int.TryParse(tanlan, out tanlang);

            switch (tanlang)
            {
                case 1:

                    BookRunner.BookRUn();
                    break;
                case 2:
                    BookShopRunner.BookShopRun();
                    break;
            }
        }
    }
}

