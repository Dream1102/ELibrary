using Dapper;
using ELibrary.Domain.Modells;
using ELibrary.Infrastucture.Application.Interfaces;
using Npgsql;
using System.Data;

namespace ELibrary.Infrastucture
{
    public class DbBooks : IRepository<Book>
    {
        private readonly string _connectionString = EBookDBContext.conString;

        public async Task AddAsync(Book obj)
        {
            try

            {
                using NpgsqlConnection connection = new(_connectionString);
                connection.Open();
                string cmdText = @"insert into Book(book_name,description,author,price,page_count,created) 
                    values (@book_name, @description, @author,@price,@page_count,@created)";
                NpgsqlCommand cmd = new(cmdText, connection);
                cmd.Parameters.AddWithValue("@book_name", obj.BookName);
                cmd.Parameters.AddWithValue("@description", obj.Description);
                cmd.Parameters.AddWithValue("@author", obj.Author);
                cmd.Parameters.AddWithValue(@"price",obj.Price);
                cmd.Parameters.AddWithValue("@page_count", obj.PageCount);
                cmd.Parameters.AddWithValue("@created", obj.Created);

                int res = await cmd.ExecuteNonQueryAsync();
                if (res > 0)
                {
                    Console.WriteLine(obj.BookName + " added succesfully");
                }
                // return Task.CompletedTask;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                // return Task.CompletedTask;
            }
        }

        public Task AddRangeAsync(List<Book> books)
        {
            DataColumn book_name = new()
            {
                ColumnName = "book_name",
                DataType = typeof(string)
            };
            DataColumn description = new()
            {
                ColumnName = "description",
                DataType = typeof(string)
            };
            DataColumn author = new()
            {
                ColumnName = "author",
                DataType = typeof(string)
            };
            DataColumn price = new()
            {
                ColumnName ="price",
                DataType = typeof(int)
            };
            DataColumn pageCount = new()
            {
                ColumnName = "page_count",
                DataType = typeof(int)
            };
            DataColumn created = new()
            {
                ColumnName = "created",
                DataType = typeof(DateTime)
            };

            DataTable dataTable = new("Book");
            dataTable.Columns.Add(book_name);
            dataTable.Columns.Add(description);
            dataTable.Columns.Add(author);
            dataTable.Columns.Add(price);
            dataTable.Columns.Add(pageCount);
            dataTable.Columns.Add(created);

            foreach (Book book in books)
            {
                DataRow row = dataTable.NewRow();

                row["book_name"] = book.BookName;
                row["description"] = book.Description;
                row["author"] = book.Author;
                row["price"] =book.Price;
                row["pageCount"] = book.PageCount;
                row["created"] = (DateTime)book.Created;


                dataTable.Rows.Add(row);
            }

            using NpgsqlConnection connection = new(_connectionString);
            NpgsqlDataAdapter dataAdapter = new()
            {
                InsertCommand = new NpgsqlCommand(
               "insert into Book(book_name,description,author,price,page_count,created)",
                connection)
            };
            dataAdapter.InsertCommand.Parameters.Add("@book_name", NpgsqlTypes.NpgsqlDbType.Varchar, 50, "book_name");
            dataAdapter.InsertCommand.Parameters.Add("@description", NpgsqlTypes.NpgsqlDbType.Varchar, 50, "description");
            dataAdapter.InsertCommand.Parameters.Add("@author", NpgsqlTypes.NpgsqlDbType.Varchar, 50, "author");
            dataAdapter.InsertCommand.Parameters.Add("@price", NpgsqlTypes.NpgsqlDbType.Integer,3, "price");
            dataAdapter.InsertCommand.Parameters.Add("pageCount", NpgsqlTypes.NpgsqlDbType.Integer, 3, "pageCount");
            dataAdapter.InsertCommand.Parameters.Add("@created", NpgsqlTypes.NpgsqlDbType.Date, 0, "created");
            dataAdapter.Update(dataTable);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdText = @"DELETE FROM Book WHERE book_id = @id";
            int res = await connection.ExecuteAsync(cmdText, new { id });

            if (res > 0)
            {
                Console.WriteLine("Deleted succesfully");
            }
            else
            {
                Console.WriteLine("Deleted failed");
            }
        }

      


        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            using NpgsqlConnection connection = new(_connectionString);
            IEnumerable<Book> dd= await connection.QueryAsync<Book>("SELECT book_id as Id, book_name as BookName, Description, Author, price, page_count as PageCount, created FROM Book ");

            return dd;
        }


        public async Task<Book> GetByIdAsync(int id)
        {
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdText = @"select * from book where book_id=@id";
            NpgsqlCommand cmd = new(cmdText, connection);
            cmd.Parameters.AddWithValue("@id", id);

            NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
            Book book = null;
            while (reader.Read())
            {
                book = new Book
                {
                    Id = (int)reader["book_id"],
                    BookName = reader["book_name"]?.ToString(),
                    Description = reader["description"]?.ToString(),
                    Author = (string)reader["author"],
                    Price =(int) reader["price"],
                    PageCount = (int)reader["page_count"],
                    Created = DateTime.Parse(reader["created"]?.ToString())

                };
            }
            return book;

        }
        public async Task<bool> UpdateAsync(Book entity)
        {
            try
            {
                using NpgsqlConnection connection = new(_connectionString);
                connection.Open();
                string cmdText = @"insert into Book(book_name,description,author,price,page_count,created) 
                    values (@book_name, @description, @author,@price,@page_count,@created)";
                NpgsqlCommand cmd = new(cmdText, connection);
                cmd.Parameters.AddWithValue("@book_name", entity.BookName);
                cmd.Parameters.AddWithValue("@description", entity.Description);
                cmd.Parameters.AddWithValue("@price",entity.Price);
                cmd.Parameters.AddWithValue("@author", entity.Author);
                cmd.Parameters.AddWithValue("@page_count", entity.PageCount);
                cmd.Parameters.AddWithValue("@created", entity.Created);

                int res = await cmd.ExecuteNonQueryAsync();
                if (res > 0)
                {
                    Console.WriteLine(entity.BookName + " added succesfully");
                }
                Console.WriteLine(entity.BookName + " update failed");
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Update Time:" + e.Message);
                return false;
            }
        }
    }
}



