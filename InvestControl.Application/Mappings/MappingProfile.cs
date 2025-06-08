using AutoMapper;
using InvestControl.Domain.Entities;
using InvestControl.Application.DTOs;

namespace InvestControl.Application.DTO
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Ativo, AtivoDto>().ReverseMap();
            CreateMap<Cotacao, CotacaoDto>().ReverseMap();
            CreateMap<Posicao, PosicaoDto>().ReverseMap();
            CreateMap<Usuario, UsuarioDetalhadoDto>();
        }
    }
}