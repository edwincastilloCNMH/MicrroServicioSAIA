using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using WebAPI.Domain.Entities;
using WebAPI.Domain.IRepositories;
using WebAPI.Infrastructure.Contexts;

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

                var parametroPalabra = new MySqlParameter("@palabra", palabra);
                var parametroInicioPag = new MySqlParameter("@inicioPag", inicioPag);
                var parametroCantidadReg = new MySqlParameter("@cantidadReg", cantidadReg);

                var query = "CALL omeka_cnmh.sp_omeka_metabuscador(@palabra,@inicioPag,@cantidadReg);";

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
                    throw new ArgumentException("La consulta debe tener al menos 1 parametro lleno.");


                bool tieneAlMenosUnCampo =  !string.IsNullOrWhiteSpace(consulta.Titulo) ||
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


                var parametroTitulo = new MySqlParameter("@p_Titulo", string.IsNullOrEmpty(consulta.Titulo) ? 0 : consulta.Titulo);
                var parametroAutor = new MySqlParameter("@p_Autor", string.IsNullOrEmpty(consulta.Autor) ? 0 : consulta.Autor);
                var parametroSerie = new MySqlParameter("@p_Serie", string.IsNullOrEmpty(consulta.Serie) ? 0 : consulta.Serie);
                var parametroDescripcion = new MySqlParameter("@p_Descripcion", string.IsNullOrEmpty(consulta.Descripcion) ? 0 : consulta.Descripcion);
                var parametroTema = new MySqlParameter("@p_Tema", string.IsNullOrEmpty(consulta.Tema) ? 0 : consulta.Tema);
                var parametroFecha = new MySqlParameter("@p_Fecha", string.IsNullOrEmpty(consulta.Fecha) ? 0 : consulta.Fecha);
                var parametroLugar = new MySqlParameter("@p_Lugar", string.IsNullOrEmpty(consulta.Lugar) ? 0 : consulta.Lugar);
                var parametroDocumento = new MySqlParameter("@p_Documento", string.IsNullOrEmpty(consulta.Documento) ? 0 : consulta.Documento);
                var parametroCodigo = new MySqlParameter("@p_Codigo", string.IsNullOrEmpty(consulta.Codigo) ? 0 : consulta.Codigo);
                var parametroInicioPag = new MySqlParameter("@inicioPag", consulta.Inicio);
                var parametroCantidadReg = new MySqlParameter("@cantidadReg", consulta.Cantidad);

                var query = "CALL omeka_cnmh.sp_omeka_metabuscador_avanzada(@p_Titulo,@p_Autor,@p_Serie,@p_Descripcion,@p_Tema,@p_Fecha,@p_Lugar,@p_Documento,@p_Codigo,@inicioPag,@cantidadReg);";

                var resultados = await _context.ConsultasModel
                    .FromSqlRaw(query, parametroTitulo,parametroAutor,parametroSerie,parametroDescripcion,parametroTema,parametroFecha,parametroLugar,parametroDocumento,parametroCodigo,parametroInicioPag,parametroCantidadReg)
                    .ToListAsync();

                var response = _mapper.Map<List<ConsultaEntity>>(resultados);

                return response;
            }
            catch (Exception e)
            {
                throw new ArgumentException($"Error ejecutando el procedimiento: {e.Message}");
            }
        }
    }
}
