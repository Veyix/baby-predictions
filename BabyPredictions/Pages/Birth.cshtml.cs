using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BabyPredictions.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BabyPredictions.Pages
{
    public class BirthModel : PageModel
    {
        private readonly DatabaseContext _context;
        private readonly WinnerCalculator _winnerCalculator;

        public BirthModel(DatabaseContext context, WinnerCalculator winnerCalculator)
        {
            _context = context;
            _winnerCalculator = winnerCalculator;
        }

        public bool HaveDetailsAlreadyBeenEntered { get; private set; }

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

        public void OnGet()
        {
            HaveDetailsAlreadyBeenEntered = _context.Set<Birth>().SingleOrDefault() != null;
        }

        public ActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var birth = new Birth
            {
                Gender = Gender,
                BirthDate = BirthDate.Add(BirthTime),
                BirthWeightInOunces = (WeightInPounds * 16) + WeightInOunces
            };

            _context.Add(birth);
            _context.SaveChanges();

            _winnerCalculator.CalculateWinners();

            return RedirectToPage("Index");
        }
    }
}
