using AutoMapper;
using WebAPI.Domain.Entities;
using WebAPI.Infrastructure.Models;

namespace WebAPI.Infrastructure.Profiles
{
    public class InfrastructureProfile : Profile
	{
		public InfrastructureProfile()
		{
            CreateMap<ConsultaModel, ConsultaEntity>().ReverseMap();
            CreateMap<ConsultaRequestModel, ConsultaRequestEntity>().ReverseMap();
        }
	}
}
