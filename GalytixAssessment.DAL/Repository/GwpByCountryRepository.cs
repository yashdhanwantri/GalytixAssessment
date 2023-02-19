using GalytixAssessment.DAL.IRepository;
using GalytixAssessment.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace GalytixAssessment.DAL.Repository
{
    public class GwpByCountryRepository : IGwpByCountryRepository
    {
        private readonly GalytixDbContext _dbContext;

        public GwpByCountryRepository(GalytixDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<GwpByCountry>> Get(string country, List<string> lineOfBusiness)
        {
            var result = _dbContext
                .GwpByCountry
                .Where(x => x.Country == country && lineOfBusiness.Contains(x.LineOfBusiness))
                .ToListAsync();

            return result;
        }
    }
}
