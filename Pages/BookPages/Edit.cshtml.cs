using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Porcilescu_Ionela_Delia_Lab2.Models;

namespace Porcilescu_Ionela_Delia_Lab2.Pages.BookPages;

public class EditModel : BookCategoriesPageModel
{
    private readonly Porcilescu_Ionela_Delia_Lab2Context _context;

    public EditModel(Porcilescu_Ionela_Delia_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Book? book = await _context.Book
            .Include(b => b.Authors)
            .Include(b => b.Publisher)
            .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);

        if (book == null)
        {
            return NotFound();
        }

        Book = book;

        PopulateAssignedCategoryData(_context, Book);

        ViewData["PublisherID"] = new SelectList(
            _context.Publisher,
            "ID",
            "PublisherName",
            Book.PublisherID);

        var authorList = _context.Authors.Select(a => new
        {
            a.ID,
            FullName = a.FirstName + " " + a.LastName
        });

        ViewData["AuthorsID"] = new SelectList(
            authorList,
            "ID",
            "FullName",
            Book.AuthorsID);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(
        int? id, string[] selectedCategories)
    {
        id ??= Book.ID;

        if (id == null)
        {
            return NotFound();
        }

        var bookToUpdate = await _context.Book
            .Include(b => b.Authors)
            .Include(b => b.Publisher)
            .Include(b => b.BookCategories)
            .ThenInclude(bc => bc.Category)
            .FirstOrDefaultAsync(b => b.ID == id);

        if (bookToUpdate == null)
        {
            return NotFound();
        }

        if (await TryUpdateModelAsync<Book>(
            bookToUpdate,
            "Book",
            b => b.Title,
            b => b.AuthorsID,
            b => b.Price,
            b => b.PublishingDate,
            b => b.PublisherID))
        {
            UpdateBookCategories(
                _context,
                selectedCategories,
                bookToUpdate);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        UpdateBookCategories(
            _context,
            selectedCategories,
            bookToUpdate);

        PopulateAssignedCategoryData(
            _context,
            bookToUpdate);

        ViewData["PublisherID"] = new SelectList(
            _context.Publisher,
            "ID",
            "PublisherName",
            bookToUpdate.PublisherID);

        var authorList = _context.Authors.Select(a => new
        {
            a.ID,
            FullName = a.FirstName + " " + a.LastName
        });

        ViewData["AuthorsID"] = new SelectList(
            authorList,
            "ID",
            "FullName",
            bookToUpdate.AuthorsID);

        Book = bookToUpdate;

        return Page();
    }
}
