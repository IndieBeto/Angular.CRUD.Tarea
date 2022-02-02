using System;
using System.Collections.Generic;
using System.Text;

namespace Angular.Crud.Tarea.Core.DTOs.Categorias
{
    public class CreateCategoriaDTO
    {
        public string? glosa_categoria_articulo { get; set; }
        public bool vigente_categoria_articulo { get; set; }
        public int id_cliente { get; set; }
        public int usuario_creador { get; set; }
        public int usuario_modificador { get; set; }
        public DateTime fecha_modificacion { get; set; }
        public DateTime fecha_creacion { get; set; }
    }
}
