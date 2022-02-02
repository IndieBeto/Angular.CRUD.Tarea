using Angular.Crud.Tarea.Core.DTOs.Articulos;
using Angular.Crud.Tarea.Core.DTOs.Categorias;
using Angular.Crud.Tarea.Core.Entities;
using Angular.CRUD.Tarea.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Angular.CRUD.Tarea.Application.Common.Mappings
{
    public static class Extensions
    {
        public static Categoria MapCreateCategoriaDTOToCategoria(this CreateCategoriaDTO dto, int id)
        {
            return new Categoria
            {
                glosa_categoria_articulo = dto.glosa_categoria_articulo,
                vigente_categoria_articulo = dto.vigente_categoria_articulo,
                fecha_creacion = dto.fecha_creacion,
                fecha_modificacion = dto.fecha_modificacion,
                id_cliente = dto.id_cliente,
                usuario_creador = dto.usuario_creador,
                usuario_modificador = dto.usuario_modificador,
                id_categoria_articulo = id
            };
        }

        public static Articulo MapCreateArticuloDToArticulo(this CreateArticuloDTO dto, int id)
        {
            return new Articulo
            {
                id_articulo = id,
                codigo_articulo = dto.codigo_articulo,
                descripcion_articulo = dto.descripcion_articulo,
                id_categoria_articulo = dto.id_categoria_articulo,
                marca_articulo = dto.marca_articulo,
                modelo_articulo = dto.modelo_articulo,
                id_tipo_insumo = dto.id_tipo_insumo                
            };
        }

        public static ArticuloModel MapArticuloToArticuloModel(this Articulo entity, bool fullModel = false)
        {
            if (fullModel)
            {
                return new ArticuloModel
                {
                    codigo_articulo = entity.codigo_articulo,
                    descripcion_articulo = entity.descripcion_articulo,
                    id_articulo = entity.id_articulo,
                    marca_articulo = entity.marca_articulo,
                    id_tipo_insumo = entity.id_tipo_insumo,
                    modelo_articulo = entity.modelo_articulo,
                    glosa_categoria_articulo = entity.Categoria.glosa_categoria_articulo,
                    id_categoria_articulo = entity.Categoria.id_categoria_articulo,
                    vigente_categoria_articulo = entity.Categoria.vigente_categoria_articulo
                };
            }
            return new ArticuloModel
            {
                codigo_articulo = entity.codigo_articulo,
                descripcion_articulo = entity.descripcion_articulo,
                id_articulo = entity.id_articulo,
                marca_articulo = entity.marca_articulo,
                id_tipo_insumo = entity.id_tipo_insumo,
                modelo_articulo = entity.modelo_articulo,
                id_categoria_articulo = entity.id_categoria_articulo
            };
        }

        public static CategoriaModel MapCategoriaToCategoriaModel(this Categoria entity)
        {
            return new CategoriaModel
            {
                id_categoria_articulo = entity.id_categoria_articulo,
                glosa_categoria_articulo = entity.glosa_categoria_articulo
            };
        }
    }
}
