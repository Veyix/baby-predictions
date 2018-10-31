using System;
using System.Collections.Generic;
using System.Linq;

namespace BabyPredictions.Domain
{
    public class PredictionRepository
    {
        private readonly DatabaseContext _context;

        public PredictionRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IReadOnlyCollection<Prediction> GetPredictions()
        {
            return _context.Set<Prediction>().ToArray();
        }
    }
}