using System.Collections.Generic;
using BabyPredictions.Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BabyPredictions.Pages
{
    public class IndexModel : PageModel
    {
        private readonly PredictionRepository _predictionRepository;

        public IndexModel(PredictionRepository predictionRepository)
        {
            _predictionRepository = predictionRepository;
        }

        public IEnumerable<Prediction> Predictions { get; private set; }

        public void OnGet()
        {
            Predictions = _predictionRepository.GetPredictions();
        }
    }
}
