using System.Collections.Generic;
using System.Linq;
using BabyPredictions.Domain;
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

        public void OnGet()
        {
            Predictions = _context.Set<Prediction>().ToArray();
        }
    }
}
