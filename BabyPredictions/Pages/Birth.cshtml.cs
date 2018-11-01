using System;
using System.ComponentModel.DataAnnotations;
using BabyPredictions.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BabyPredictions.Pages
{
    public class BirthModel : PageModel
    {
        private readonly DatabaseContext _context;

        public BirthModel(DatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        [Required(ErrorMessage = "Select the gender of the baby")]
        public Gender Gender { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Enter the date the baby was born")]
        public DateTimeOffset BirthDate { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Enter the time the baby was born")]
        public TimeSpan BirthTime { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Enter the baby's weight (total number of pounds)")]
        public int WeightInPounds { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Enter the baby's weight (number of ounces not in whole pounds)")]
        public int WeightInOunces { get; set; }

        public ActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // TODO: Submit.

            return RedirectToPage();
        }
    }
}
