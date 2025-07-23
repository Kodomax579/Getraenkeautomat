using GetraenkeautomatVorrat.DTO;
using GetraenkeautomatVorrat.Models;
using Microsoft.AspNetCore.Mvc;

namespace GetraenkeautomatVorrat.Interfaces
{
    public interface IVorratController
    {
        [HttpGet]
        public ActionResult<List<VorratDTO>> GetAll();

        [HttpPost]
        public ActionResult<VorratDTO> Post(VorratDTO item);

        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id);

        [HttpGet("{id}")]
        public ActionResult<Vorrat> Get(int id);

        [HttpPut]
        public ActionResult<bool> Update(UpdateVorratDTO vorrat, string name);
    }
}
