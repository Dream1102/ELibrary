using Dapper;
using ELibrary.Domain.Modells;
using ELibrary.Infrastucture.Application.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Infrastucture
{
    public class DbBookShop : IRepository<BookShop>
    {
        private readonly string _connectionString = EBookDBContext.conString;
        
        public async Task AddAsync(BookShop obj)
        {
            try
            {
                using NpgsqlConnection connection = new(_connectionString);
                connection.Open();
                string cmdText = @"insert into BookShop(shoop_name,adress,isreturnnet) 
                    values (@shoop_name, @adress, @phone_number,@isreturnnet)";
                NpgsqlCommand cmd = new(cmdText, connection);
                cmd.Parameters.AddWithValue("@shoop_name", obj.ShoopName); 
                cmd.Parameters.AddWithValue("@adress", obj.Adress);
           
                cmd.Parameters.AddWithValue("@isreturnnet", obj.IsReturnet);
                int res = await cmd.ExecuteNonQueryAsync();
                if (res > 0)
                {
                    Console.WriteLine(obj.ShoopName + " added succesfully");
                }
                //return Task.CompletedTask;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                // return Task.CompletedTask;
            }
        }
        public Task AddRangeAsync(List<BookShop> bookShops)
        {
            DataColumn shoop_name = new()
            {
                ColumnName = "shoop_name",
                DataType = typeof(string)
            };
            DataColumn adress = new()
            {
                ColumnName = "description",
                DataType = typeof(string)
            };

           

            DataColumn isreturnnet = new()
            {
                ColumnName = "created",
                DataType = typeof(bool)
            };

            DataTable dataTable = new("Book");
            dataTable.Columns.Add(shoop_name);
            dataTable.Columns.Add(adress);
         
            dataTable.Columns.Add(isreturnnet);


            foreach (BookShop book in bookShops)
            {
                DataRow row = dataTable.NewRow();

                row["shoop_name"] = book.ShoopName;
                row["adress"] = book.Adress;
            
                row["isreturnnet"] = book.IsReturnet;

                dataTable.Rows.Add(row);
            }

            using NpgsqlConnection connection = new(_connectionString);
            NpgsqlDataAdapter dataAdapter = new()
            {
                InsertCommand = new NpgsqlCommand(
             @"insert into BookShop(shoop_name,adress,phone_number,isreturnnet", connection)
            };
            dataAdapter.InsertCommand.Parameters.Add("@shoop_name", NpgsqlTypes.NpgsqlDbType.Varchar, 50, "shoop_name");
            dataAdapter.InsertCommand.Parameters.Add("@adress", NpgsqlTypes.NpgsqlDbType.Varchar, 50, "adress");
          //  dataAdapter.InsertCommand.Parameters.Add("@phone_number", NpgsqlTypes.NpgsqlDbType.Integer, 20, "phone_number");
            dataAdapter.InsertCommand.Parameters.Add("@isreturnnet", NpgsqlTypes.NpgsqlDbType.Boolean, 2, "isreturnnet");

            dataAdapter.Update(dataTable);
            return Task.CompletedTask;
        }
        public async Task<BookShop> GetByIdAsync(int id)
        {
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdText = @"select * from book where shop_id=@id";
            NpgsqlCommand cmd = new(cmdText, connection);
            cmd.Parameters.AddWithValue("@id", id);

            NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
            BookShop bookshop = null;
            while (reader.Read())
            {
                bookshop = new BookShop()
                {
                    ShoopID = (int)reader["shop_id"],
                    ShoopName = (string)reader["shop_name"],
                    Adress = (string)reader["adress"],
                 //   PhoneNumber = (string)reader["phone_number"],
                    IsReturnet = (bool)reader["isreturnnet"]
                };
            }
            return bookshop;

        }
        public async Task DeleteAsync(int id)
        {
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdText = @"DELETE FROM  BookShop WHERE shop_id = @id";
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
        public async Task<IEnumerable<BookShop>> GetAllAsync()
        {
            using NpgsqlConnection connection = new(_connectionString);
            IEnumerable<BookShop> dd = await connection.QueryAsync<BookShop>("SELECT shop_id as ShoopID, shop_name as ShoopName, adress as Adress  , isreturntet FROM BookShop ");

            return dd;
        }
        public async Task<bool> UpdateAsync(BookShop entity)
        {
            try
            {
                using NpgsqlConnection connection = new(_connectionString);
                connection.Open();
                string cmdText = @"insert into BookShop(shoop_name,adress,phone_number,isreturnnet) 
                    values (@shoop_name, @adress, @phone_number,@isreturnnet)";
                NpgsqlCommand cmd = new(cmdText, connection);
                cmd.Parameters.AddWithValue("@shoop_name", entity.ShoopName);
                cmd.Parameters.AddWithValue("@adress", entity.Adress);
               // cmd.Parameters.AddWithValue("@phone_number", entity.PhoneNumber);
                cmd.Parameters.AddWithValue("@isreturnnet", entity.IsReturnet);


                int res = await cmd.ExecuteNonQueryAsync();
                if (res > 0)
                {
                    Console.WriteLine(entity.ShoopName + " added succesfully");
                }
                Console.WriteLine(entity.ShoopName + " update failed");
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
