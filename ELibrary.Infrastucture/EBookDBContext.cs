using Dapper;
using Npgsql;

namespace ELibrary.Infrastucture
{
    public class EBookDBContext
    {
        public static string conString = File.ReadAllText(@"..\..\..\..\..\ELibraryPrsentation\ELibrary.Infrastucture\appconfig.txt");
     
        public static void InitializeTables()
        {
            try
            {

                using (var connection = new NpgsqlConnection(conString))
                {
                    connection.Execute(@"
            create table Book
            (
	       book_ID serial primary key,
           book_name varchar(50) not null,
	      description varchar(100) not null,
	      author varchar(35) not null,
	      price int not null,
	      page_count int not null,
	      created date
          );
                        create table BookShop
	                           	(
			                   shop_id serial primary key,
                               shoop_name varchar(50) not null,
			                   adress varchar(50) not null,
		                    
		                    	isreturnnet boolean default true);
                                                                        ");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public  void CreateDb()
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(conString);
                connection.Open();
                connection.Close();
            }
            catch (NpgsqlException e)
            {
                if (e.Message.Contains("does not exist", StringComparison.OrdinalIgnoreCase))
                {
                    string con = conString.Replace("eshop", "postgres");
                    using NpgsqlConnection connection = new(con);
                    connection.Open();
                    string query = "create database eshop";
                    NpgsqlCommand command = new(query, connection);
                    command.ExecuteNonQuery();
                    InitializeTables();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        public DbBooks DbBooks { get; set; } = new DbBooks();
        public DbBookShop DbBookShop { get; set; } = new DbBookShop();
    }
}
