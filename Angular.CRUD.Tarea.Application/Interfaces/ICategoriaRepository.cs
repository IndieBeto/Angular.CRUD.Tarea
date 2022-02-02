using Angular.Crud.Tarea.Core.DTOs.Categorias;
using Angular.Crud.Tarea.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Angular.CRUD.Tarea.Application.Interfaces
{
    public interface ICategoriaRepository 
    {
        Task<Categoria> Create(CreateCategoriaDTO dto);
        Task Update(UpdateCategoriaDTO dto);
        Task Delete(int id);
        Task<Categoria> Get(int id);
        Task<IEnumerable<Categoria>> GetAll();
    }
}
