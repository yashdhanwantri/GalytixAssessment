using GalytixAssessment.BLL.BusinessLogic;
using GalytixAssessment.BLL.IBusinessLogic;
using GalytixAssessment.DAL.IRepository;
using GalytixAssessment.DAL.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Galytix.BLL.Tests
{
    public class GwpByCountryBusinessLogicTests
    {
        private readonly IGwpByCountryBusinessLogic _gwpByCountryBusinessLogic;
        private Mock<IGwpByCountryRepository> _mockGwpByCountryRepository;

        public GwpByCountryBusinessLogicTests()
        {
            _mockGwpByCountryRepository = new Mock<IGwpByCountryRepository>();
            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();

            var memoryCache = serviceProvider.GetService<IMemoryCache>();
            _gwpByCountryBusinessLogic = new GwpByCountryBusinessLogic(_mockGwpByCountryRepository.Object, memoryCache);
        }

        [Fact]
        public async Task GetAverageGrossWrittenPremium_ShouldThrowException_WhenCountryIsNull()
        {
            var lineOfBusiness = new List<string> { "Test1", "Test2" };

            var result = await Assert.ThrowsAsync<ArgumentNullException>(() => _gwpByCountryBusinessLogic.GetAverageGrossWrittenPremium(null, lineOfBusiness));

            Assert.Contains("Value cannot be null", result.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task GetAverageGrossWrittenPremium_ShouldThrowException_WhenLineOfBusinessIsNull()
        {
            var emptyLineOfBusiness = new List<string>();

            var result = await Assert.ThrowsAsync<ArgumentNullException>(() => _gwpByCountryBusinessLogic.GetAverageGrossWrittenPremium("eu", emptyLineOfBusiness));

            Assert.Contains("Value cannot be null", result.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task GetAverageGrossWrittenPremium_ShouldReturnSuccess()
        {
            var lineOfBusiness = new List<string> { "Test 1", "Test 2" };
            var country = "test";

            var mockedGwpByCountry = new List<GwpByCountry>
            {
                new GwpByCountry
                {
                    Country = country,
                    LineOfBusiness = "Test 1"
                },
                new GwpByCountry
                {
                    Country = country,
                    LineOfBusiness = "Test 2"
                }
            };

            _mockGwpByCountryRepository.Setup(x => x.Get(country, lineOfBusiness)).ReturnsAsync(mockedGwpByCountry);
            var result = await _gwpByCountryBusinessLogic.GetAverageGrossWrittenPremium(country, lineOfBusiness);

            Assert.NotNull(result);
            Assert.Equal(result.Count, mockedGwpByCountry.Count);
        }


    }
}