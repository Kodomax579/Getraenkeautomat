﻿using Microsoft.AspNetCore.Mvc;
using GetraenkeautomatVorrat.Models;
using GetraenkeautomatVorrat.Services;
using GetraenkeautomatVorrat.DTO;
using GetraenkeautomatVorrat.Interfaces;
using System.Threading.Tasks;

namespace GetraenkeautomatVorrat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VorratController : ControllerBase, IVorratController
    {
        private readonly VorratService _service;
        private ILogger<VorratController> _logger;

        public VorratController(VorratService service, ILogger<VorratController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<VorratDTO>> GetAll()
        {
            var vorrats  = _service.GetAll();
            if(vorrats == null)
            {
                _logger.LogError("No Products found");
                return BadRequest("No products found");
            }

            var productList = new List<VorratDTO>();
            foreach(var vorrat in vorrats)
            {
                    var product = new VorratDTO()
                    {
                        Id = vorrat.Id,
                        Amount = vorrat.Anzahl,
                        Size = vorrat.Groesse,
                        Name = vorrat.Name,
                        Price = vorrat.Preis,
                        Picture = vorrat.Picture,
                    };
                productList.Add(product);
            }

            return productList;
        }

        [HttpPost]
        public ActionResult<VorratDTO> Post(VorratDTO item)
        {
            if (item == null)
            {
                _logger.LogError("No body");
                return BadRequest("No body");
            }

            var added = _service.Add(item);

            switch(added)
            {
                case 0:
                    return Ok(item);
                case 1:
                    return BadRequest("Product already exist");
                default:
                    return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
        {
            if(id == 0)
            {
                _logger.LogError("Id can't be zero");
                return BadRequest("Id can't be zero");
            }
            
            var isDeleted = _service.Delete(id);

            if(!isDeleted)
            {
                _logger.LogError("No product found");
                return BadRequest("No product found");
            }
            return true;
        }


        [HttpGet("{id}")]
        public ActionResult<Vorrat> Get(int id)
        {
            var vorrat = _service.Get(id);

            if (vorrat == null)
            {
                return NotFound();
            }

            return Ok(vorrat);
        }


        [HttpPut]
        public ActionResult<bool> Update(UpdateVorratDTO vorrat, string name)
        {
            if (vorrat == null)
            {
                _logger.LogError("No body");
                return BadRequest("No body");
            }
            var isUpdated = _service.Update(vorrat,name);

            switch(isUpdated)
            {
                case 0:
                    return Ok(true);
                case 1:
                    return BadRequest("no game found to update");
                default:
                    return BadRequest();

            }
        }
        
    }
}