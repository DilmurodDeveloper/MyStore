using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyStore.Data;
using MyStore.Models;

namespace MyStore.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _context;

        public DeleteModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product? Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Product = await _context.Products.FindAsync(id);

            if (Product == null)
            {
                return NotFound(); 
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Product != null)
            {
                _context.Products.Remove(Product); 
                await _context.SaveChangesAsync(); 
            }

            return RedirectToPage("Index"); 
        }
    }
}
