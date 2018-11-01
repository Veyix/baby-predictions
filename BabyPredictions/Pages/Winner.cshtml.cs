using System.Linq;
using BabyPredictions.Domain;
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

        public string FirstPlaceWinnerName { get; private set; }
        public string SecondPlaceWinnerName { get; private set; }
        public string ThirdPlaceWinnerName { get; private set; }

        public void OnGet()
        {
            var winners = _context.Set<Winner>()
                .OrderBy(x => x.Position)
                .Take(3)
                .ToArray();

            if (!winners.Any())
            {
                return RedirectToPage("Index");
            }

            var firstPlaceWinner = winners[0];
            FirstPlaceWinnerName = $"{firstPlaceWinner.Forename} {firstPlaceWinner.Surname}";

            var secondPlaceWinner = winners[1];
            SecondPlaceWinnerName = $"{secondPlaceWinner.Forename} {secondPlaceWinner.Surname}";

            var thirdPlaceWinner = winners[2];
            ThirdPlaceWinnerName = $"{thirdPlaceWinner.Forename} {thirdPlaceWinner.Surname}";
        }
    }
}