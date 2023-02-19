using GalytixAssessment.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace GalytixAssessment.DAL
{
    public class GalytixDbContext : DbContext
    {
        public virtual DbSet<GwpByCountry> GwpByCountry { get; set; }

        public GalytixDbContext(DbContextOptions<GalytixDbContext> options)
         : base(options)
        {
        }
    }
}
