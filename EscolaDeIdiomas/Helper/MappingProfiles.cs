using AutoMapper;
using EscolaDeIdiomas.Dto;
using EscolaDeIdiomas.Models;

namespace EscolaDeIdiomas.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Aluno, AlunoDto>().ReverseMap();
            CreateMap<Turma, TurmaDto>().ReverseMap();
            CreateMap<AlunosTurmas, AlunosTurmasDto>().ReverseMap();
        }
    }
}
