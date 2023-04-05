using ELibrary.Domain.Modells;
using ELibrary.Infrastucture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Apllication
{
    public class BookShopRunner
    {
        public static void BookShopRun()
        {
           EBookDBContext _db = new EBookDBContext();

            bool isActive = true;
            while (isActive)
            {
                Console.WriteLine("Enter operation to perform:");
                Console.WriteLine("1. Create");
                Console.WriteLine("2. Read");
                Console.WriteLine("3. Update");


                string choose = Console.ReadLine();
                int choice;
                int.TryParse(choose, out choice);

                switch (choice)
                {

                    case 1:
                        // Create
                        Console.WriteLine("Enter BookShop details:");
                     
                        
                            Console.Write("Name: ");
                           string name = Console.ReadLine();
                      

                        
                        Console.Write("Adress");
                        string adreess = Console.ReadLine();
                        //Console.Write(" PhoneNumber ");
                        //string phoneNumber = (Console.ReadLine());
                        Console.Write("Isreturnnet :");
                        char isreturn = char.Parse(Console.ReadLine());
                        // Console.Write("Created :");


                        BookShop newBook = new BookShop
                        {
                            ShoopName = name,
                            Adress = adreess,
                         //   PhoneNumber = phoneNumber,
                            IsReturnet = true
                        };
                        _db.DbBookShop.AddAsync(newBook);


                        _db.ToString();

                        Console.WriteLine("Book added successfully.");
                        break;
                    case 2:
                        read();
                        break;
                    case 3:
                        Update();
                        break;
                }
            }
        }
        public static async Task read() 
        {
            DbBookShop dbSHop = new();
            var books = await dbSHop.GetAllAsync();
            foreach (var book in books)
            {
                Console.WriteLine($"ID: {book.ShoopID} - Name: {book.ShoopName} - Adress : {book.Adress} - Price :{book.IsReturnet} ");
            }
        }
        public static async Task Update()
        {
            Console.WriteLine("Enter book details:");
            Console.Write("ID :");
            int ID = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter BookShop details:");
            string? name = null;
            while (true)
            {
                Console.Write("Name: ");
                name = Console.ReadLine();
                if (name == null) continue;

            }
            Console.Write("Adress");
            string adreess = Console.ReadLine();
            //Console.Write(" PhoneNumber ");
            //string phoneNumber = (Console.ReadLine());
            Console.Write("Isreturnnet :");
            char isreturn = char.Parse(Console.ReadLine());
            // Console.Write("Created :");
            BookShop newBooks = new BookShop
            {
                ShoopName = name,
                Adress = adreess,
              //  PhoneNumber = phoneNumber,
                IsReturnet = true
            };

            DbBookShop dbBookShop = new DbBookShop();

            bool updated = await dbBookShop.UpdateAsync(newBooks);

            if (updated)
            {
                Console.WriteLine("Book updated successfully.");
            }
            else
            {
                Console.WriteLine("Book update failed.");
            }


        }

    }
}
