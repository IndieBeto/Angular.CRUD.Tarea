using Angular.Crud.Tarea.Core.DTOs.Articulos;
using Angular.CRUD.Tarea.Application.Common.Mappings;
using Angular.CRUD.Tarea.Application.Common.Models;
using Angular.CRUD.Tarea.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Angular.CRUD.Tarea.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        private readonly IArticuloRepository articuloRepository;

        public ArticulosController(IArticuloRepository articuloRepository)
        {
            this.articuloRepository = articuloRepository;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var articulos = await articuloRepository.GetAll();
            return Ok(articulos.Select(x => x.MapArticuloToArticuloModel(fullModel: true)));
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var articulo = await articuloRepository.Get(id);

            if (articulo == null)
                return NotFound();

            return Ok(articulo.MapArticuloToArticuloModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateArticuloDTO dto)
        {
            // todo validacion en server
            return Ok((await articuloRepository.Create(dto)).MapArticuloToArticuloModel());
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateArticuloDTO dto)
        {
            await articuloRepository.Update(dto);
            return Ok(dto);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await articuloRepository.Delete(id);
            return Ok();
        }

    }
}
