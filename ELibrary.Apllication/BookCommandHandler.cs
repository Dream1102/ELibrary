using ELibrary.Domain.Modells;
using Nest;

namespace ELibrary.Infrastucture.Application.Interfaces.BookCommandHandler

{
    public class CommandHandler : IRepository<BookShop>, IRepository<Book>
    {
        EBookDBContext _db = new();

        public Book Settings { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public object DelegateSettings => throw new NotImplementedException();

        public string Type => throw new NotImplementedException();

        public async Task AddAsync(BookShop obj)
        {
            await _db.DbBookShop.AddAsync(obj);   
        }

        public async Task AddAsync(Book obj)
        {
            await _db.DbBooks.AddAsync(obj);
        }

        public async Task AddRangeAsync(List<BookShop> books)
        {
            await _db.DbBookShop.AddRangeAsync(books);
        }

        public async Task AddRangeAsync(List<Book> obj)
        {
            await _db.DbBooks.AddRangeAsync(obj);
        }

        public async Task DeleteAsync(int id)
        {
            await _db.DbBookShop.DeleteAsync(id);
        }

        public async Task<IEnumerable<BookShop>> GetAllAsync()
        {
            return await _db.DbBookShop.GetAllAsync();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _db.DbBooks.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(BookShop entity)
        {
            return await _db.DbBookShop.UpdateAsync(entity);
        }

        public async Task<bool> UpdateAsync(Book entity)
        {
            return await _db.DbBooks.UpdateAsync(entity);
        }

        async Task<IEnumerable<Book>> IRepository<Book>.GetAllAsync()
        {
            return await _db.DbBooks.GetAllAsync();
        }

        async Task<Book> IRepository<Book>.GetByIdAsync(int id)
        {
            return await _db.DbBooks.GetByIdAsync(id);
        }

        async Task<BookShop> IRepository<BookShop>.GetByIdAsync(int id)
        {
            return await _db.DbBookShop.GetByIdAsync(id);
        }
    }

    
}
