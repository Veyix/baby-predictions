using System.Collections.Generic;
using System.Linq;
using BabyPredictions.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BabyPredictions.Pages
{
    public class WinnerModel : PageModel
    {
        private readonly DatabaseContext _context;

        public WinnerModel(DatabaseContext context)
        {
            _context = context;
        }
        
        public IEnumerable<Winner> Winners { get; private set; }

        public ActionResult OnGet()
        {
            var winners = _context.Set<Winner>()
                .OrderBy(x => x.Position)
                .ToArray();

            if (!winners.Any())
            {
                return RedirectToPage("Index");
            }
            
            Winners = winners;

            return Page();
        }
    }
}