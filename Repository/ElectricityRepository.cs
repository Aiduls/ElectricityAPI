using ElectricityAPI1.Data;
using ElectricityAPI1.Interfaces;
using ElectricityAPI1.Models;

namespace ElectricityAPI1.Repository
{
    public class ElectricityRepository : IElectricityRepository
    {
        private readonly DataContext _context;
        public ElectricityRepository(DataContext context)
        {
            _context = context; 
        }

        public ICollection<Electricity> GetElectricities()
        {
            return _context.Electricities.OrderBy(p => p.Id).ToList();
        }
    }
}
