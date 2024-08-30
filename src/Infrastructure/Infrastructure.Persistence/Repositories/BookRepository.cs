#nullable disable

using Application.Common.Models;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories;

public class BookRepository : GenericRepository<Book>, IBookRepository
{
    private readonly IMemoryCache _cache;

    public BookRepository(LibraSysContext context, IMemoryCache cache) : base(context) => _cache = cache;

    // Fetch a book by ID with its related authors (Eager Loading)
    public async Task<Book> GetByIdWithAuthors(Guid id)
    {
        return await _context.Books
            .Include(b => b.BookAuthors)
            .ThenInclude(ba => ba.Author)
            .FirstOrDefaultAsync(b => b.BookId == id);
    }

    // Paginated fetching of books with optional filtering
    public async Task<PaginatedList<Book>> GetPaginated(int pageNumber, int pageSize, Expression<Func<Book, bool>> filter = null)
    {
        var query = _context.Books.AsQueryable();

        if (filter != null)
            query = query.Where(filter);

        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedList<Book>(items, totalCount, pageNumber, pageSize);
    }

    // Cached fetching of all books
    public async Task<IList<Book>> GetCachedBooks()
    {
        var cacheKey = "all_books";
        if (!_cache.TryGetValue(cacheKey, out IList<Book> cachedBooks))
        {
            cachedBooks = await _context.Books.ToListAsync();
            _cache.Set(cacheKey, cachedBooks, TimeSpan.FromMinutes(10)); // Cache data for 10 minutes
        }
        return cachedBooks;
    }
}
