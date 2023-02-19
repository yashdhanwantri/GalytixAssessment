namespace GalytixAssessment.BLL.IBusinessLogic
{
    public interface IGwpByCountryBusinessLogic
    {
        Task<Dictionary<string, double?>> GetAverageGrossWrittenPremium(string country, List<string> lineOfBusiness);
    }
}
