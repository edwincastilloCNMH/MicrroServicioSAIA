using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Entities;
using WebAPI.Domain.IRepositories;
using WebAPI.Infrastructure.Contexts;
using WebAPI.Infrastructure.Models;

namespace WebAPI.Infrastructure.Repositories
{
    public class ConsultaRepository : IConsultaRepository
    {
        private readonly IMapper _mapper;
        private readonly WebAPIContext _context;
        public ConsultaRepository(WebAPIContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ConsultaEntity>> VerResultadoConsultaBasica(string palabra, int inicioPag, int cantidadReg)
        {
            try
            {
                if (string.IsNullOrEmpty(palabra))
                    throw new ArgumentException("La palabra clave no puede ser vacía.", nameof(palabra));

                var parametroPalabra = new SqlParameter("@palabra", palabra);
                var parametroInicioPag = new SqlParameter("@inicioPag", inicioPag);
                var parametroCantidadReg = new SqlParameter("@cantidadReg", cantidadReg);

                var query = "EXEC [dbo].[sp_saia_metabuscador] @palabra, @inicioPag, @cantidadReg";

                var resultados = await _context.ConsultasModel 
                    .FromSqlRaw(query, parametroPalabra,parametroInicioPag,parametroCantidadReg)
                    .ToListAsync();

                var response = _mapper.Map<List<ConsultaEntity>>(resultados);

                return response;
            }
            catch (Exception e)
            {
                throw new ArgumentException($"Error ejecutando el procedimiento: {e.Message}");
            }
        }

        public async Task<List<ConsultaEntity>> VerResultadoConsultaAvanzada(ConsultaRequestEntity consulta)
        {
            try
            {
                if (consulta == null)
                    throw new ArgumentException("La consulta debe tener al menos 1 parámetro lleno.");

                bool tieneAlMenosUnCampo =
                    !string.IsNullOrWhiteSpace(consulta.Titulo) ||
                    !string.IsNullOrWhiteSpace(consulta.Autor) ||
                    !string.IsNullOrWhiteSpace(consulta.Serie) ||
                    !string.IsNullOrWhiteSpace(consulta.Descripcion) ||
                    !string.IsNullOrWhiteSpace(consulta.Tema) ||
                    !string.IsNullOrWhiteSpace(consulta.Fecha) ||
                    !string.IsNullOrWhiteSpace(consulta.Lugar) ||
                    !string.IsNullOrWhiteSpace(consulta.Documento) ||
                    !string.IsNullOrWhiteSpace(consulta.Codigo);

                if (!tieneAlMenosUnCampo)
                    throw new ArgumentException("Debe proporcionar al menos un criterio de búsqueda.");

                // 🔹 SQL Server usa SqlParameter
                var parametros = new[]
                {
                    new SqlParameter("@p_Titulo", string.IsNullOrWhiteSpace(consulta.Titulo) ? 0 : consulta.Titulo),
                    new SqlParameter("@p_Autor", string.IsNullOrWhiteSpace(consulta.Autor) ? 0 : consulta.Autor),
                    new SqlParameter("@p_Serie", string.IsNullOrWhiteSpace(consulta.Serie) ? 0 : consulta.Serie),
                    new SqlParameter("@p_Descripcion", string.IsNullOrWhiteSpace(consulta.Descripcion) ? 0 : consulta.Descripcion),
                    new SqlParameter("@p_Tema", string.IsNullOrWhiteSpace(consulta.Tema) ? 0 : consulta.Tema),
                    new SqlParameter("@p_Fecha", string.IsNullOrWhiteSpace(consulta.Fecha) ? 0 : consulta.Fecha),
                    new SqlParameter("@p_Lugar", string.IsNullOrWhiteSpace(consulta.Lugar) ? 0 : consulta.Lugar),
                    new SqlParameter("@p_Documento", string.IsNullOrWhiteSpace(consulta.Documento) ? 0 : consulta.Documento),
                    new SqlParameter("@p_Codigo", string.IsNullOrWhiteSpace(consulta.Codigo) ? 0 : consulta.Codigo),
                    new SqlParameter("@inicioPag", consulta.Inicio),
                    new SqlParameter("@cantidadReg", consulta.Cantidad)
                };

                // 🔹 Llamada al SP en SQL Server
                var query = "EXEC [dbo].[sp_saia_metabuscador_avanzada] " +
                            "@p_Titulo, @p_Autor, @p_Serie, @p_Descripcion, @p_Tema, @p_Fecha, @p_Lugar, @p_Documento, @p_Codigo, @inicioPag, @cantidadReg";

                var resultados = await _context.ConsultasModel
                    .FromSqlRaw(query, parametros)
                    .ToListAsync();

                return _mapper.Map<List<ConsultaEntity>>(resultados);
            }
            catch (Exception e)
            {
                throw new ArgumentException($"Error ejecutando el procedimiento: {e.Message}");
            }
        }

        public async Task<List<DocumentoSPEntity>> VerResultadoDetalle(string codigo)
        {
            try
            {
                if (string.IsNullOrEmpty(codigo))
                    throw new ArgumentException("el código no puede estar vacio.", nameof(codigo));

                var parametroCodigo = new SqlParameter("@p_Codigo ", codigo);

                var query = "EXEC [dbo].[sp_saia_metabuscador_detalle_registro] @p_Codigo";

                var resultados = await _context.DocumentosSPModel
                    .FromSqlRaw(query, parametroCodigo)
                    .ToListAsync();

                var response = _mapper.Map<List<DocumentoSPEntity>>(resultados);

                return response;
            }
            catch (Exception e)
            {
                throw new ArgumentException($"Error ejecutando el procedimiento: {e.Message}");
            }
        }
    }
}
