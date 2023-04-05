using ELibrary.Domain.Modells;
using ELibrary.Infrastucture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Apllication
{
    public class BookRunner
    {  
         public static void BookRUn()
            {
                EBookDBContext _db = new EBookDBContext();
              
                bool isActive = true;
                while (isActive)
                {

                    Console.WriteLine("Enter operation to perform:");
                    Console.WriteLine("1. Create");
                    Console.WriteLine("2. Read");
                    Console.WriteLine("3. Update");
                    Console.WriteLine("4. Delete");

                    string choose = Console.ReadLine();
                    int choice;
                    int.TryParse(choose, out choice);

                    switch (choice)
                    {
                        case 1:
                            // Create
                            Console.WriteLine("Enter book details:");
                            Console.Write("Name: ");
                            string name = Console.ReadLine();
                            Console.Write("Description: ");
                            string description = Console.ReadLine();
                            Console.Write("Author: ");
                            string author = Console.ReadLine();
                            Console.Write("Price: ");
                            int price = int.Parse(Console.ReadLine());
                            Console.Write("Page_Count :");
                            int page_count = int.Parse(Console.ReadLine());
                            Console.Write("Created :");


                            Book newBook = new Book
                            {
                                
                                BookName = name,
                                Description = description,
                                Author = author,
                                Price = price,
                                PageCount = page_count,
                                Created = DateTime.Now
                            };

                            _db.DbBooks.AddAsync(newBook);

                            _db.ToString();

                            Console.WriteLine("Book added successfully.");
                            break;

                        case 2:
                            // Read

                            Run();
                            break;

                        case 3:

                            Update();

                            break;

                        case 4:

                            Delete();
                            break;


                        default:
                            Console.WriteLine("Invalid option.");
                            break;
                    }
                }
            }
            static async Task Run()
            {

                DbBooks dbBooks = new();
                var books = await dbBooks.GetAllAsync();
                foreach (var book in books)
                {
                    Console.WriteLine($"ID: {book.Id} - Name: {book.BookName} - Description : {book.Description}" +
                                      $"\n - Author :{book.Author} - Price :{book.Price} " +
                                      $"- PageCount :{book.PageCount} - Created :{book.Created}\n");
                }
            }

            static async Task Update()
            {
                Console.WriteLine("Enter book details:");
                Console.Write("ID :");
                int ID = int.Parse(Console.ReadLine());
                Console.Write("Name: ");
                string name = Console.ReadLine();
                Console.Write("Description: ");
                string description = Console.ReadLine();
                Console.Write("Author: ");
                string author = Console.ReadLine();
                Console.Write("Price: ");
                int price = int.Parse(Console.ReadLine());
                Console.Write("Page_Count :");
                int page_count = int.Parse(Console.ReadLine());
                Console.Write("Created :");
                DateTime created = DateTime.Now;
                Book bookToUpdate = new Book()
                {
                    Id = ID,
                    BookName = name,
                    Description = description,
                    Author = author,
                    Price = price,
                    PageCount = page_count,
                    Created = created
                };

                DbBooks dbBooks = new DbBooks();
                bool updated = await dbBooks.UpdateAsync(bookToUpdate);

                if (updated)
                {
                    Console.WriteLine("Book updated successfully.");
                }
                else
                {
                    Console.WriteLine("Book update failed.");
                }


            }
            static async Task Delete()
            {
                Console.WriteLine("Enter book id:");
                int idToDelete = int.Parse(Console.ReadLine());
                DbBooks dbBooks = new DbBooks();
                await dbBooks.DeleteAsync(idToDelete);

                await Console.Out.WriteLineAsync("Successfully");

            }
        }
    }
