using GetraenkeautomatVorrat.Data;
using GetraenkeautomatVorrat.DTO;
using GetraenkeautomatVorrat.Interfaces;
using GetraenkeautomatVorrat.Models;
using Microsoft.EntityFrameworkCore;

namespace GetraenkeautomatVorrat.Services
{
    public class VorratService : IVorratService
    {

        private readonly VorratContext _context;
        //private RequestProductsService _requestProductsService;
        public VorratService(VorratContext context /*,RequestProductsService requestProductsService*/)
        {
            _context = context;
            //_requestProductsService = requestProductsService;
        }

        public int Add(VorratDTO item)
        {
            try
            {
                var p = _context.Vorraete.First(p => p.Name  == item.Name);

                if (p != null)
                {
                    return 1;
                }
            }
            catch { }

            var product = new Vorrat()
            {
                Name = item.Name,
                Preis = item.Price,
                Groesse = item.Size,
                Picture = item.Picture,
                Anzahl = item.Amount,
            };

            _context.Vorraete.Add(product);
            _context.SaveChanges();

            return 0;
        }

        public List<Vorrat> GetAll()
        {
            return _context
                    .Vorraete
                    .AsTracking()
                    .ToList();
        }

        public Vorrat Get(int id)
        {
            try
            {
                return _context.Vorraete.First(p => p.Id == id);
            }
            catch
            {
                return null!;
            }
        }

        public bool Delete(int id)
        {
            var vorrat = Get(id);

            if (vorrat is null)
                return false;

            _context.Vorraete.Remove(vorrat);
            _context.SaveChanges();

            return true;
        }

        public VorratDTO Update(UpdateVorratDTO vorrat, string productName)
        {
            var existing = _context.Vorraete.FirstOrDefault(e => e.Name == productName);
            if (existing == null)
                return null;

            if(vorrat.Price > 0)
            {
                existing.Preis = vorrat.Price;
            }
            if (vorrat.Amount >= 0)
            {
                existing.Anzahl = vorrat.Amount;
            }
            
            _context.SaveChanges();

            if(existing.Anzahl <=3)
            {
                //await this._requestProductsService.RefillProducts(10, existing.Name);
            }

            VorratDTO vorratDTO = new VorratDTO()
            {
                Amount = existing.Anzahl,
                Id = existing.Id,
                Name = existing.Name,
                Picture = existing.Picture,
                Price = existing.Preis,
                Size = existing.Groesse,
            };

            return vorratDTO;
        }
        public void Refill(string name, int amount)
        {
            var product = _context.Vorraete.FirstOrDefault(e => e.Name == name);
            if (product == null)
                return;

            product.Anzahl = product.Anzahl + amount;
            _context.SaveChanges();
        }
    }
}