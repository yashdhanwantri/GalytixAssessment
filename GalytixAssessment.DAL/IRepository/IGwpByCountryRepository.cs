using GalytixAssessment.DAL.Models;

namespace GalytixAssessment.DAL.IRepository
{
    public interface IGwpByCountryRepository
    {
        Task<List<GwpByCountry>> Get(string country, List<string> lob);
    }
}
