using System.Collections.Generic;
using System.Linq;
using BabyPredictions.Domain;
using Microsoft.AspNetCore.Mvc;
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

        public IEnumerable<Prediction> Predictions { get; private set; } = new Prediction[0];

        public void OnGet()
        {
            Predictions = _context.Set<Prediction>().ToArray();
        }

        public ActionResult OnPostDelete(int id)
        {
            var prediction = _context.Set<Prediction>()
                .SingleOrDefault(x => x.Id == id);

            if (prediction != null)
            {
                _context.Remove(prediction);
                _context.SaveChanges();
            }

            return RedirectToPage();
        }

        public ActionResult OnPostPaid(int id)
        {
            var prediction = _context.Set<Prediction>()
                .SingleOrDefault(x => x.Id == id);

            if (prediction != null)
            {
                prediction.HasPaid = true;

                _context.Update(prediction);
                _context.SaveChanges();
            }

            return RedirectToPage();
        }
    }
}
