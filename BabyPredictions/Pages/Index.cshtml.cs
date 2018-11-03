using System.Collections.Generic;
using System.Linq;
using BabyPredictions.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BabyPredictions.Pages
{
    public class IndexModel : PageModel
    {
        private readonly DatabaseContext _context;

        public IndexModel(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Prediction> Predictions { get; private set; }

        public ActionResult OnGet()
        {
            if (IsThereAWinner())
            {
                return RedirectToPage("Winner");
            }

            Predictions = _context.Set<Prediction>()
                .OrderBy(x => x.Id)
                .ToArray();

            return Page();
        }

        private bool IsThereAWinner()
        {
            var winners = _context.Set<Winner>().ToArray();
            return winners.Any();
        }
    }
}
