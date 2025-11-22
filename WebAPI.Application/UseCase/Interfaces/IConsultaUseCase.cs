using WebAPI.Application.DTO;
using WebAPI.Application.Models;

namespace WebAPI.Application.UseCase.Interfaces
{
    public interface IConsultaUseCase
    {
        Task<Response<ConsultaResponseDTO>> ConsultaBasica(ConsultaRequestDTO parametros);
        Task<Response<ConsultaResponseDTO>> ConsultaAvanzada(ConsultaRequestDTO parametros);
    }
}
