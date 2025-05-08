using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyStore.Data;
using MyStore.Models;

namespace MyStore.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            Product = product;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var productToUpdate = await _context.Products.FindAsync(Product.Id);

            if (productToUpdate == null)
            {
                return NotFound();
            }

            productToUpdate.Name = Product.Name;
            productToUpdate.Brand = Product.Brand;
            productToUpdate.Price = Product.Price;
            productToUpdate.Description = Product.Description;

            await _context.SaveChangesAsync();

            return RedirectToPage("/Products/Index");
        }
    }
}
