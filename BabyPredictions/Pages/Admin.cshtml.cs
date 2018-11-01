using System.Collections.Generic;
using System.Linq;
using BabyPredictions.Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BabyPredictions.Pages
{
    public class AdminModel : PageModel
    {
        private readonly DatabaseContext _context;

        public AdminModel(DatabaseContext context)
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
