#nullable disable

using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Contexts;

public class LibraSysContext : DbContext
{
    public LibraSysContext(DbContextOptions<LibraSysContext> options) : base(options) { }
    
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BookAuthor> BooksAuthors { get; set; } // Junction Table (Many-to-Many Relationship)
    public DbSet<Loan> Loans { get; set; }
    public DbSet<Member> Members { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuring primary keys
        modelBuilder.Entity<Author>().HasKey(a => a.AuthorId);
        modelBuilder.Entity<Book>().HasKey(a => a.BookId);
        modelBuilder.Entity<BookAuthor>().HasKey(ba => new {ba.AuthorId, ba.BookId});
        modelBuilder.Entity<Loan>().HasKey(l => l.LoanId);
        modelBuilder.Entity<Member>().HasKey(m => m.MemberId);

        // Configuring relationships between tables
        modelBuilder.Entity<BookAuthor>()
            .HasOne(ba => ba.Book)
            .WithMany(b => b.BookAuthors)
            .HasForeignKey(ba => ba.BookId);

        modelBuilder.Entity<BookAuthor>()
            .HasOne(ba => ba.Author)
            .WithMany(a => a.BookAuthors)
            .HasForeignKey(ba => ba.AuthorId);

        modelBuilder.Entity<Loan>()
            .HasOne(l => l.Book)
            .WithMany(b => b.Loans)
            .HasForeignKey(l => l.BookId);

        modelBuilder.Entity<Loan>()
            .HasOne(l => l.Member)
            .WithMany(m => m.Loans)
            .HasForeignKey(l => l.MemberId);
    }
}
