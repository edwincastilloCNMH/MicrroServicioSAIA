using AutoMapper;
using WebAPI.Application.DTO;
using WebAPI.Domain.Entities;

namespace WebAPI.Application.Profiles
{
	public class ApplicationProfile : Profile
	{
		public ApplicationProfile()
		{
            CreateMap<ConsultaDTO, ConsultaEntity>().ReverseMap();
            CreateMap<ConsultaRequestDTO, ConsultaRequestEntity>().ReverseMap();
            CreateMap<DocumentoSPDTO, DocumentoSPEntity>().ReverseMap();
        }
	}
}
