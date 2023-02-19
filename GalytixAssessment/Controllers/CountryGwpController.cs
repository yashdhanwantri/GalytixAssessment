using GalytixAssessment.API.Model.Request;
using GalytixAssessment.BLL.IBusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace GalytixAssessment.API.Controllers
{
    [Route("server/api/gwp")]
    [ApiController]
    public class CountryGwpController : ControllerBase
    {
        private readonly IGwpByCountryBusinessLogic _gwpByCountryBusinessLogic;

        public CountryGwpController(IGwpByCountryBusinessLogic gwpByCountryBusinessLogic)
        {
            _gwpByCountryBusinessLogic = gwpByCountryBusinessLogic;
        }

        /// <summary>
        /// Returns Gross Written Premium from Year 2008 - 2015
        /// </summary>
        /// <param name="request">Request containing the Country Name and Line of Businesses.</param>
        /// <returns>Gross Written Premium per Line Of Businesses.</returns>
        [HttpPost]
        [Route("avg")]
        public async Task<IActionResult> GetAverage([FromBody] GetAvgGwpRequest request)
        {
            try
            {
                var result = await _gwpByCountryBusinessLogic.GetAverageGrossWrittenPremium(request.Country, request.Lob);
                return Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
