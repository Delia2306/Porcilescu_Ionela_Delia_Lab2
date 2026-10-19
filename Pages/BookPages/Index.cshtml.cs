using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Porcilescu_Ionela_Delia_Lab2.Models;

namespace Porcilescu_Ionela_Delia_Lab2.Pages.BookPages;

public class IndexModel : PageModel
{
    private readonly Porcilescu_Ionela_Delia_Lab2Context _context;

    public IndexModel(Porcilescu_Ionela_Delia_Lab2Context context)
    {
        _context = context;
    }

    public IList<Book> Book { get; set; } = default!;
    public BookData BookD { get; set; } = default!;
    public int BookID { get; set; }
    public int CategoryID { get; set; }

    public SelectList AuthorsList { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public int? SelectedAuthorID { get; set; }

    public async Task OnGetAsync(int? id, int? categoryID)
    {
        BookD = new BookData();

        var authors = await _context.Authors
            .OrderBy(a => a.LastName)
            .ToListAsync();

        AuthorsList = new SelectList(
            authors.Select(a => new
            {
                a.ID,
                FullName = a.FirstName + " " + a.LastName
            }),
            "ID",
            "FullName");

        var books = _context.Book
            .Include(b => b.Publisher)
            .Include(b => b.Authors)
            .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
            .AsNoTracking()
            .AsQueryable();

        if (SelectedAuthorID.HasValue)
        {
            books = books.Where(b => b.AuthorsID == SelectedAuthorID.Value);
        }

        Book = await books
            .OrderBy(b => b.Title)
            .ToListAsync();

        BookD.Books = Book;

        if (id != null)
        {
            BookID = id.Value;

            Book book = BookD.Books
                .Where(i => i.ID == id.Value)
                .Single();

            BookD.Categories = book.BookCategories
                .Select(s => s.Category);
        }
    }
}