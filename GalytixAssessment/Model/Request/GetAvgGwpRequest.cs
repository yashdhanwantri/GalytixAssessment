using System.ComponentModel.DataAnnotations;

namespace GalytixAssessment.API.Model.Request
{
    public class GetAvgGwpRequest
    {
        [Required]
        public string Country { get; set; }

        [Required]
        public List<string> Lob { get; set; }

        public GetAvgGwpRequest()
        {
            Country = string.Empty;
            Lob = new List<string>();
        }
    }
}
