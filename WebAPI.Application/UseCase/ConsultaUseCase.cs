using AutoMapper;
using WebAPI.Application.DTO;
using WebAPI.Application.Models;
using WebAPI.Application.UseCase.Interfaces;
using WebAPI.Domain.Services.IService;

namespace WebAPI.Application.UseCase
{
    public class ConsultaUseCase : IConsultaUseCase
    {
        private readonly IMapper _mapper;
        private readonly IConsultaService _consultaService;


        public ConsultaUseCase(IMapper mapper, IConsultaService consultaService)
        {
            _mapper = mapper;
            _consultaService = consultaService;
        }

        public async Task<Response<ConsultaResponseDTO>> ConsultaBasica(ConsultaRequestDTO parametros)
        {
            var response = new Response<ConsultaResponseDTO>();
            try
            {
                var resultado = await _consultaService.VerResultadoConsultaBasica(parametros.PalabraClave, parametros.Inicio, parametros.Cantidad);

                var consultaResponseDTO = new ConsultaResponseDTO();

                consultaResponseDTO.Origen = "SAIA";
                consultaResponseDTO.Parametros = parametros;
                consultaResponseDTO.Respuesta = _mapper.Map<List<ConsultaDTO>>(resultado);
                response.Data = consultaResponseDTO;
                response.Succeeded = true;
            }
            catch (Exception e)
            {
                response.Succeeded = false;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<Response<ConsultaResponseDTO>> ConsultaAvanzada(ConsultaRequestDTO parametros)
        {
            var response = new Response<ConsultaResponseDTO>();
            try
            {

                var parametrosEntity = _mapper.Map<Domain.Entities.ConsultaRequestEntity>(parametros);

                var resultado = await _consultaService.VerResultadoConsultaAvanzada(parametrosEntity);

                var consultaResponseDTO = new ConsultaResponseDTO();

                consultaResponseDTO.Origen = "SAIA";
                consultaResponseDTO.Parametros = parametros;
                consultaResponseDTO.Respuesta = _mapper.Map<List<ConsultaDTO>>(resultado);
                response.Data = consultaResponseDTO;
                response.Succeeded = true;
            }
            catch (Exception e)
            {
                response.Succeeded = false;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<Response<DocumentoSPDTO>> ConsultaDetalle(string codigo)
        {
            var response = new Response<DocumentoSPDTO>();
            try
            {
                var resultado = await _consultaService.VerResultadoDetalle(codigo);

                var dataResponse = _mapper.Map<List<DocumentoSPDTO>>(resultado);

                response.Data = dataResponse.FirstOrDefault();
                response.Succeeded = true;
            }
            catch (Exception e)
            {
                response.Succeeded = false;
                response.Message = e.Message;
            }

            return response;
        }
    }
}
