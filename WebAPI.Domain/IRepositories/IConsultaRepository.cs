using WebAPI.Domain.Entities;

namespace WebAPI.Domain.IRepositories
{
    public interface IConsultaRepository
    {
        Task<List<ConsultaEntity>> VerResultadoConsultaBasica(string palabra, int inicioPag, int cantidadReg);
        Task<List<ConsultaEntity>> VerResultadoConsultaAvanzada(ConsultaRequestEntity consulta);
        Task<List<DocumentoSPEntity>> VerResultadoDetalle(string codigo);
    }
}
