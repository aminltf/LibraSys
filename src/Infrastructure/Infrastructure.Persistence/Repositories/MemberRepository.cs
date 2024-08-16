#nullable disable

using Application.Common.Models;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories;

public class MemberRepository : GenericRepository<Member>, IMemberRepository
{
    private readonly IMemoryCache _cache;

    public MemberRepository(LibraSysContext context, IMemoryCache cache) : base(context) => _cache = cache;

    // Fetch a member by ID with their related loans (Eager Loading)
    public async Task<Member> GetByIdWithLoans(int id)
    {
        return await _context.Members
            .Include(m => m.Loans) // Include related loans
            .ThenInclude(l => l.Book) // Include the related books for each loan
            .FirstOrDefaultAsync(m => m.MemberId == id);
    }

    // Paginated fetching of members with optional filtering
    public async Task<PaginatedList<Member>> GetPaginated(int pageNumber, int pageSize, Expression<Func<Member, bool>> filter = null)
    {
        var query = _context.Members.AsQueryable();

        if (filter != null)
            query = query.Where(filter);

        var totalCount = await query.CountAsync();
        var items = await query
            .Include(m => m.Loans) // Include related loans
            .ThenInclude(l => l.Book) // Include related books for each loan
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedList<Member>(items, totalCount, pageNumber, pageSize);
    }

    // Cached fetching of all members
    public async Task<IList<Member>> GetCachedMembers()
    {
        var cacheKey = "all_members";
        if (!_cache.TryGetValue(cacheKey, out IList<Member> cachedMembers))
        {
            cachedMembers = await _context.Members
                .Include(m => m.Loans) // Include related loans for caching
                .ThenInclude(l => l.Book) // Include related books for each loan in caching
                .ToListAsync();
            _cache.Set(cacheKey, cachedMembers, TimeSpan.FromMinutes(10)); // Cache data for 10 minutes
        }
        return cachedMembers;
    }
}
