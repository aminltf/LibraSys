#nullable disable

using Application.Common.Models;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories;

public class LoanRepository : GenericRepository<Loan>, ILoanRepository
{
    private readonly IMemoryCache _cache;

    public LoanRepository(LibraSysContext context, IMemoryCache cache) : base(context) => _cache = cache;

    // Fetch a loan by ID with its related details (Eager Loading)
    public async Task<Loan> GetByIdWithDetails(Guid id)
    {
        return await _context.Loans
            .Include(l => l.Book) // Include the related book
            .Include(l => l.Member) // Include the related member
            .FirstOrDefaultAsync(l => l.LoanId == id);
    }

    // Paginated fetching of loans with optional filtering
    public async Task<PaginatedList<Loan>> GetPaginated(int pageNumber, int pageSize, Expression<Func<Loan, bool>> filter = null)
    {
        var query = _context.Loans.AsQueryable();

        if (filter != null)
            query = query.Where(filter);

        var totalCount = await query.CountAsync();
        var items = await query
            .Include(l => l.Book) // Include related book
            .Include(l => l.Member) // Include related member
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedList<Loan>(items, totalCount, pageNumber, pageSize);
    }

    // Cached fetching of all loans
    public async Task<IList<Loan>> GetCachedLoans()
    {
        var cacheKey = "all_loans";
        if (!_cache.TryGetValue(cacheKey, out IList<Loan> cachedLoans))
        {
            cachedLoans = await _context.Loans
                .Include(l => l.Book) // Include related book for caching
                .Include(l => l.Member) // Include related member for caching
                .ToListAsync();
            _cache.Set(cacheKey, cachedLoans, TimeSpan.FromMinutes(10)); // Cache data for 10 minutes
        }
        return cachedLoans;
    }
}
