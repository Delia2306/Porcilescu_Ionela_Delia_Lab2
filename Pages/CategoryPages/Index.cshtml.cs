using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Porcilescu_Ionela_Delia_Lab2.Models;
using Porcilescu_Ionela_Delia_Lab2.Models.ViewModels;

namespace Porcilescu_Ionela_Delia_Lab2.Pages.CategoryPages;

public class IndexModel : PageModel
{
    private readonly Porcilescu_Ionela_Delia_Lab2Context _context;

    public IndexModel(Porcilescu_Ionela_Delia_Lab2Context context)
    {
        _context = context;
    }

    public IList<Category> Category { get; set; } = default!;

    public CategoryIndexData CategoryData { get; set; }
    public int CategoryID { get; set; }
    public int BookID { get; set; }

    public async Task OnGetAsync(int? id, int? bookID)
    {
        CategoryData = new CategoryIndexData();

        CategoryData.Categories = await _context.Category
            .Include(i => i.BookCategories)
                .ThenInclude(c => c.Book)
                    .ThenInclude(b => b.Authors)
            .OrderBy(i => i.CategoryName)
            .ToListAsync();

        if (id != null)
        {
            CategoryID = id.Value;
            Category category = CategoryData.Categories
                .Where(i => i.ID == id.Value).Single();

            CategoryData.Books = category.BookCategories
                .Select(b => b.Book);
        }
    }
}
