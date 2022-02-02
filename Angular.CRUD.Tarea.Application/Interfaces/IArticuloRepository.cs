using Angular.Crud.Tarea.Core.DTOs.Articulos;
using Angular.Crud.Tarea.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Angular.CRUD.Tarea.Application.Interfaces
{
    public interface IArticuloRepository
    {
        Task<IEnumerable<Articulo>> GetAll();
        Task<Articulo> Get(int id);
        Task Delete(int id);
        Task Update(UpdateArticuloDTO dto);
        Task<Articulo> Create(CreateArticuloDTO articulo);
    }
}
