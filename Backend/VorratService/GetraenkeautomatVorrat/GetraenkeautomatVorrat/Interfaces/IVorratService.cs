using GetraenkeautomatVorrat.DTO;
using GetraenkeautomatVorrat.Models;

namespace GetraenkeautomatVorrat.Interfaces
{
    public interface IVorratService
    {
        public int Add(VorratDTO item);

        public List<Vorrat> GetAll();

        public Vorrat Get(int id);

        public bool Delete(int id);

        public int Update(UpdateVorratDTO vorrat, string productName);
    }
}
