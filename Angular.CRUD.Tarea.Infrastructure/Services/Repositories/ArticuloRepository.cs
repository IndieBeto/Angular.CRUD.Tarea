using Angular.Crud.Tarea.Core.DTOs.Articulos;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angular.CRUD.Tarea.Infrastructure.Services.Repositories
{
    public class ArticuloRepository : IArticuloRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ArticuloRepository> _logger;

        public ArticuloRepository(IConfiguration configuration, ILogger<ArticuloRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<Articulo> Get(int id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("AngularCRUD")))
            {
                try
                {
                    // todo pasar estas querys a stored procedures
                    connection.Open();
                    string query = @"select * from Articulo where id_articulo = @id_articulo";
                    var results = (await connection.QueryAsync<Articulo>(query, new { id_articulo = id })).FirstOrDefault();
                    await connection.CloseAsync();
                    return results;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al solicitar articulos", ex);
                    // Cerramos la conexion en caso de error
                    await connection.CloseAsync();
                    return null;
                }
            }
        }

        public async Task<Articulo> Create(CreateArticuloDTO dto)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("AngularCRUD")))
            {
                try
                {
                    connection.Open();
                    // Retorna tipo dynamic
                    var queryResult = await connection.QueryAsync("SP_GUARDAR_ARTICULO", dto, commandType: CommandType.StoredProcedure);
                    int id = (int)queryResult.FirstOrDefault().id_articulo;
                    await connection.CloseAsync();
                    return dto.MapCreateArticuloDToArticulo(id);
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

        public async Task Update(UpdateArticuloDTO dto)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("AngularCRUD")))
            {
                try
                {
                    // todo pasar estas querys a stored procedures
                    connection.Open();
                    var articulo = await Get(dto.id_articulo);

                    if (articulo == null)
                        return;
                    var results = (await connection.QueryAsync<Articulo>("SP_ACTUALIZAR_ARTICULO", dto, commandType: CommandType.StoredProcedure));
                    await connection.CloseAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al solicitar articulos", ex);
                    // Cerramos la conexion en caso de error
                    await connection.CloseAsync();
                }
            }
        }

        public async Task Delete(int id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("AngularCRUD")))
            {
                try
                {
                    // todo pasar estas querys a stored procedures
                    connection.Open();
                    string query = @"delete from Articulo where id_articulo = @id_articulo";
                    var results = (await connection.QueryAsync(query, new { id_articulo = id }));
                    await connection.CloseAsync();;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al borrar articulo", ex);
                    // Cerramos la conexion en caso de error
                    await connection.CloseAsync();
                }
            }
        }

        public async Task<IEnumerable<Articulo>> GetAll()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("AngularCRUD")))
            {
                try
                {
                    connection.Open();
                    string sql = @"select ar.*, ca.* from dbo.Articulo ar
                                   left join dbo.Categoria ca
                                   on ar.id_categoria_articulo = ca.id_categoria_articulo";

                    var articulos = await connection.QueryAsync<Articulo, Categoria, Articulo>(sql,
                        (articulo, categoria) => { articulo.Categoria = categoria; return articulo; }, splitOn: "id_categoria_articulo");                  
                    await connection.CloseAsync();
                    return articulos;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al solicitar articulos", ex);
                    // Cerramos la conexion en caso de error
                    await connection.CloseAsync();
                    return null;
                }
            }
        }
    }
}
