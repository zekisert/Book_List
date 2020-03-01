using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor
{
    public class EditModel : PageModel
    {
        private ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Books { get; set; }


        public async Task OnGet(int id)
        {
            Books = await _db.Books.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var BookFromDb = await _db.Books.FindAsync(Books.Id);
                BookFromDb.Name = Books.Name;
                BookFromDb.ISBN = Books.ISBN;
                BookFromDb.Author = Books.Author;

                await _db.SaveChangesAsync();

                return RedirectToPage("Index"); 
            }
            return RedirectToPage();
        }

    }
}
