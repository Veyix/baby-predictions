using System;
using System.Collections.Generic;
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
        public string Forename { get; set; }

        [BindProperty]
        public string Surname { get; set; }

        [BindProperty]
        public Gender Gender { get; set; }

        [BindProperty]
        public DateTimeOffset BirthDate { get; set; }

        [BindProperty]
        public TimeSpan BirthTime { get; set; }

        [BindProperty]
        public int WeightInPounds { get; set; }

        [BindProperty]
        public int WeightInOunces { get; set; }

        public ActionResult OnPost()
        {
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
    }
}
