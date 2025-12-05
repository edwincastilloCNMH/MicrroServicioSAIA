using WebAPI.Domain.Entities;
using WebAPI.Domain.IRepositories;
using WebAPI.Domain.Services.IService;

namespace WebAPI.Domain.Services
{
    public class ConsultaService : IConsultaService
    { 
        private readonly IConsultaRepository _consultaRepository;

        public ConsultaService(IConsultaRepository consultaRepository)
        {
            _consultaRepository = consultaRepository;
        }

        public async Task<List<ConsultaEntity>> VerResultadoConsultaBasica(string palabra, int inicioPag, int cantidadReg)
        {
            return await _consultaRepository.VerResultadoConsultaBasica(palabra, inicioPag, cantidadReg);
        }

        public async Task<List<ConsultaEntity>> VerResultadoConsultaAvanzada(ConsultaRequestEntity consulta)
        {
            return await _consultaRepository.VerResultadoConsultaAvanzada(consulta);
        }

        public async Task<List<DocumentoSPEntity>> VerResultadoDetalle(string codigo)
        {
            return await _consultaRepository.VerResultadoDetalle(codigo);
        }
    }
}
