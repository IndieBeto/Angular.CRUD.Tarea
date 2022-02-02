using System;
using System.Collections.Generic;
using System.Text;

namespace Angular.Crud.Tarea.Core.DTOs.Articulos
{
    public class CreateArticuloDTO
    {
        public string? codigo_articulo { get; set; }
        public string? marca_articulo { get; set; }
        public string? modelo_articulo { get; set; }
        public string? descripcion_articulo { get; set; }
        public int id_tipo_insumo { get; set; }
        public int id_categoria_articulo { get; set; }
    }
}
