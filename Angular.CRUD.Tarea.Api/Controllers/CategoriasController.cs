using Angular.Crud.Tarea.Core.DTOs.Categorias;
using Angular.CRUD.Tarea.Application.Common.Mappings;
using Angular.CRUD.Tarea.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Angular.CRUD.Tarea.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriasController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            // todo handle cuando categoria no existe

            return Ok(await _categoriaRepository.Get(id));
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            return Ok((await _categoriaRepository.GetAll()).Select(x => x.MapCategoriaToCategoriaModel()));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoriaDTO model)
        {
            // todo validacion

            // crear
          
            return Ok(await _categoriaRepository.Create(model));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCategoriaDTO model)
        {
            // todo validacion
            await _categoriaRepository.Update(model);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoriaRepository.Delete(id);
            return Ok();
        }



    }
}
