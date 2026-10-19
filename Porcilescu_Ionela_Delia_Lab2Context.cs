
using Microsoft.EntityFrameworkCore;
using Porcilescu_Ionela_Delia_Lab2;
using Porcilescu_Ionela_Delia_Lab2.Models;

public class Porcilescu_Ionela_Delia_Lab2Context(DbContextOptions<Porcilescu_Ionela_Delia_Lab2Context> options) : DbContext(options)
{
    public DbSet<Book> Book { get; set; } = default!;

    public DbSet<Publisher> Publisher { get; set; } = default!;

    public DbSet<Authors> Authors { get; set; } = default!;

    public DbSet<Category> Category { get; set; } = default!;

    public DbSet<BookCategory> BookCategory { get; set; } = default!;
}
