using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BabyPredictions.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BabyPredictions.Pages
{
    public class MakePredictionModel : PageModel
    {
        private readonly DatabaseContext _context;

        public MakePredictionModel(DatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        [Required(ErrorMessage = "You must enter your first name")]
        public string Forename { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "You must enter your last name")]
        public string Surname { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please select the gender you think the baby will be")]
        public Gender Gender { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please select the date you think the baby will be born on")]
        public DateTimeOffset BirthDate { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please select the time you think the baby will be born at")]
        public TimeSpan BirthTime { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please enter your prediction for the baby's weight in pounds (lbs) at birth")]
        public int WeightInPounds { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please enter your prediction for the baby's weight in ounces (oz) at birth")]
        public int WeightInOunces { get; set; }

        public ActionResult OnPost()
        {
            if (HasUserAlreadySubmittedPrediction())
            {
                string errorMessage = $"{Forename} {Surname}, you have already submitted a prediction and cannot submit another";
                ModelState.AddModelError("Forename", errorMessage);
            }

            if (HasPredictedBirthDateAlreadyPassed())
            {
                ModelState.AddModelError("BirthDate", $"Your prediction of the baby's birth date as {BirthDate:dd/MM/yyyy} has already passed");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var prediction = new Prediction
            {
                Forename = Forename,
                Surname = Surname,
                Gender = Gender,
                BirthDate = BirthDate.Add(BirthTime),
                BirthWeightInOunces = (WeightInPounds * 16) + WeightInOunces
            };

            _context.Add(prediction);
            _context.SaveChanges();

            return RedirectToPage("Index");
        }

        private bool HasUserAlreadySubmittedPrediction()
        {
            var prediction = _context.Set<Prediction>()
                .Where(x => x.Forename == Forename)
                .Where(x => x.Surname == Surname)
                .SingleOrDefault();

            return prediction != null;
        }

        private bool HasPredictedBirthDateAlreadyPassed()
        {
            return BirthDate < DateTime.Today;
        }
    }
}
