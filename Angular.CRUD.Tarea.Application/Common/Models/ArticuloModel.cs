using System;
using System.Collections.Generic;
using System.Text;

namespace Angular.CRUD.Tarea.Application.Common.Models
{
    public class ArticuloModel
    {
        public int id_articulo { get; set; }
        public string codigo_articulo { get; set; }
        public string marca_articulo { get; set; }
        public string modelo_articulo { get; set; }
        public string descripcion_articulo { get; set; }
        public int id_tipo_insumo { get; set; }
        public int id_categoria_articulo { get; set; }
        public string? glosa_categoria_articulo { get; set; }
        public bool vigente_categoria_articulo { get; set; }
    }
}
