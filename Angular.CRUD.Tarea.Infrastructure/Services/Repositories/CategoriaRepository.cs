using Angular.Crud.Tarea.Core.DTOs.Categorias;
using Angular.Crud.Tarea.Core.Entities;
using Angular.CRUD.Tarea.Application.Common.Mappings;
using Angular.CRUD.Tarea.Application.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angular.CRUD.Tarea.Infrastructure.Services.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CategoriaRepository> _logger;

        public CategoriaRepository(IConfiguration configuration, ILogger<CategoriaRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<Categoria> Create(CreateCategoriaDTO dto)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("AngularCRUD")))
            {
                try
                {
                    connection.Open();
                    // Retorna tipo dynamic
                    var queryResult = await connection.QueryAsync("SP_GUARDAR_CATEGORIA", dto, commandType: CommandType.StoredProcedure);
                    int id = (int)queryResult.FirstOrDefault().id_categoria_articulo;
                    await connection.CloseAsync();
                    return dto.MapCreateCategoriaDTOToCategoria(id);
                }
                catch (SqlException e)
                {
                    _logger.LogError("Error al crear categoria", e);
                    // Cerramos la conexion en caso de error
                    await connection.CloseAsync(); 
                    return null;
                }
            }
        }

        public async Task Delete(int id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("AngularCRUD")))
            {
                try
                {
                    connection.Open();

                    await connection.ExecuteAsync("SP_ELIMINAR_CATEGORIA", new { id } , commandType: CommandType.StoredProcedure);
                    await connection.CloseAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al eliminar categoria", ex);
                    // Cerramos la conexion en caso de error
                    await connection.CloseAsync();
                }
            }
        }

        public async Task<Categoria> Get(int id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("AngularCRUD")))
            {
                try
                {
                    connection.Open();
                    var query = "SELECT * FROM CATEGORIA WHERE id_categoria_articulo = @id_categoria_articulo";     
                    var queryResult = (await connection.QueryAsync(query, new { id_categoria_articulo = id })).FirstOrDefault();                    
                    if (queryResult != null)
                        return new Categoria
                        {
                            id_categoria_articulo = (int)queryResult.id_categoria_articulo,
                            glosa_categoria_articulo = queryResult.glosa_categoria_articulo,
                            vigente_categoria_articulo = queryResult.vigente_categoria_articulo,
                            fecha_creacion = DateTime.ParseExact(queryResult.fecha_creacion.ToString(), new[] { "dd-MM-yyyy HH:mm:ss", "dd-MM-yyyy HH:mm:ss" }, CultureInfo.InvariantCulture, DateTimeStyles.None),
                            fecha_modificacion = DateTime.ParseExact(queryResult.fecha_modificacion.ToString(), new[] { "dd-MM-yyyy HH:mm:ss", "dd-MM-yyyy HH:mm:ss" }, CultureInfo.InvariantCulture, DateTimeStyles.None),
                            id_cliente = (int)queryResult.id_cliente,
                            usuario_creador = (int)queryResult.usuario_creador,
                            usuario_modificador = (int)queryResult.usuario_modificador
                        };
                    return null;
                }
                catch (SqlException e)
                {
                    _logger.LogError("Error al crear categoria", e);
                    // Cerramos la conexion en caso de error
                    await connection.CloseAsync();
                    return null;
                }
            }
        }
        public async Task<IEnumerable<Categoria>> GetAll()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("AngularCRUD")))
            {
                try
                {
                    connection.Open();
                    var query = "SELECT * FROM CATEGORIA;";
                    var queryResult = (await connection.QueryAsync(query)).Select(x => new Categoria
                    {
                        id_categoria_articulo = (int)x.id_categoria_articulo,
                        glosa_categoria_articulo = x.glosa_categoria_articulo,
                        vigente_categoria_articulo = x.vigente_categoria_articulo,
                        fecha_creacion = DateTime.ParseExact(x.fecha_creacion.ToString(), new[] { "dd-MM-yyyy HH:mm:ss", "dd-MM-yyyy HH:mm:ss" }, CultureInfo.InvariantCulture, DateTimeStyles.None),
                        fecha_modificacion = DateTime.ParseExact(x.fecha_modificacion.ToString(), new[] { "dd-MM-yyyy HH:mm:ss", "dd-MM-yyyy HH:mm:ss" }, CultureInfo.InvariantCulture, DateTimeStyles.None),
                        id_cliente = (int)x.id_cliente,
                        usuario_creador = (int)x.usuario_creador,
                        usuario_modificador = (int)x.usuario_modificador
                    });
                    return queryResult;
                }
                catch (SqlException e)
                {
                    _logger.LogError("Error solicitar categorias", e);
                    // Cerramos la conexion en caso de error
                    await connection.CloseAsync();
                    return null;
                }
            }
        }
        public async Task Update(UpdateCategoriaDTO dto)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("AngularCRUD")))
            {
                try
                {
                    connection.Open();
                    await connection.ExecuteAsync("SP_ACTUALIZAR_CATEGORIA", dto, commandType: CommandType.StoredProcedure);
                    await connection.CloseAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al actualizar categoria", ex);
                    // Cerramos la conexion en caso de error
                    await connection.CloseAsync();
                }
            }
        }
    }
}
