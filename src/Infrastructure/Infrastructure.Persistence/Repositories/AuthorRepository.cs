#nullable disable

using Application.Common.Models;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories;

public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
{
    private readonly IMemoryCache _cache;

    public AuthorRepository(LibraSysContext context, IMemoryCache cache) : base(context) => _cache = cache;

    // Fetch author by ID with related books (Eager Loading)
    public async Task<Author> GetByIdWithBooks(int id)
    {
        return await _context.Authors
            .Include(a => a.BookAuthors)
            .FirstOrDefaultAsync(a => a.AuthorId == id);
    }

    // Paginated fetching of authors with optional filtering
    public async Task<PaginatedList<Author>> GetPaginated(int pageNumber, int pageSize, Expression<Func<Author, bool>> filter = null)
    {
        var query = _context.Authors.AsQueryable();

        if (filter != null)
            query = query.Where(filter);

        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedList<Author>(items, totalCount, pageNumber, pageSize);
    }

    // Cached fetching of authors
    public async Task<IList<Author>> GetCachedAuthors()
    {
        var cacheKey = "all_authors";
        if (!_cache.TryGetValue(cacheKey, out IList<Author> cachedAuthors))
        {
            cachedAuthors = await _context.Authors.ToListAsync();
            _cache.Set(cacheKey, cachedAuthors, TimeSpan.FromMinutes(10)); // Cache data for 10 minutes
        }
        return cachedAuthors;
    }
}
