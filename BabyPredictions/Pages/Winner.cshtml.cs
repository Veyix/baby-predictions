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
        
        public Birth Birth { get; private set; }
        public IEnumerable<Winner> Winners { get; private set; }

        public ActionResult OnGet()
        {
            Birth = _context.Set<Birth>().SingleOrDefault();

            if (Birth == null)
            {
                return RedirectToPage("Index");
            }

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