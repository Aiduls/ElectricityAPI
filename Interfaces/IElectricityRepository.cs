using ElectricityAPI1.Models;

namespace ElectricityAPI1.Interfaces
{
    public interface IElectricityRepository
    {
        ICollection<Electricity> GetElectricities();
    }
}
