using GalytixAssessment.BLL.IBusinessLogic;
using GalytixAssessment.DAL.IRepository;
using Microsoft.Extensions.Caching.Memory;

namespace GalytixAssessment.BLL.BusinessLogic
{
    public class GwpByCountryBusinessLogic : IGwpByCountryBusinessLogic
    {
        private readonly IGwpByCountryRepository _gwpByCountryRepository;
        private readonly IMemoryCache _memoryCache;

        public GwpByCountryBusinessLogic(IGwpByCountryRepository gwpByCountryRepository, IMemoryCache memoryCache)
        {
            _gwpByCountryRepository = gwpByCountryRepository;
            _memoryCache = memoryCache;

        }

        public async Task<Dictionary<string, double?>> GetAverageGrossWrittenPremium(string country, List<string> lineOfBusiness)
        {
            ValidateGetAverageGwp(country, lineOfBusiness);

            var result = new Dictionary<string, double?>();
            var itemToRemove = new List<string>();
            foreach (var currentItem in lineOfBusiness)
            {
                if(_memoryCache.TryGetValue(country+currentItem, out double item))
                {
                    result.Add(currentItem, item);
                    itemToRemove.Add(currentItem);
                }
            }

            lineOfBusiness.RemoveAll(x => itemToRemove.Contains(x));

            if (lineOfBusiness.Any())
            {
                var countryLineOfBusiness = await _gwpByCountryRepository.Get(country, lineOfBusiness);

                foreach (var lob in countryLineOfBusiness)
                {
                    var average = (lob.Y2008 ?? 0 + lob.Y2009 ?? 0 + lob.Y2010 ?? 0 + lob.Y2011 ?? 0 + lob.Y2012 ?? 0 + lob.Y2013 ?? 0 + lob.Y2014 ?? 0 + lob.Y2015 ?? 0) / 8;
                    result.Add(lob.LineOfBusiness, average);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(60));
                    _memoryCache.Set(country + lob, average, cacheEntryOptions);
                }
            }
            

            return result;

        }

        private static void ValidateGetAverageGwp(string country, List<string> lineOfBusiness)
        {
            if (string.IsNullOrWhiteSpace(country))
            {
                throw new ArgumentNullException(nameof(country));
            }

            if (lineOfBusiness == null || !lineOfBusiness.Any() || lineOfBusiness.Any(x => string.IsNullOrWhiteSpace(x)))
            {
                throw new ArgumentNullException(nameof(lineOfBusiness));
            }

            country = country.Trim().ToLower();
            lineOfBusiness.ForEach(x => x = x.Trim().ToLower());
        }
    }
}
