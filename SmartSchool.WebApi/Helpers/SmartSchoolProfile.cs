using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SmartSchool.WebApi.Dtos;
using SmartSchool.WebApi.Models;

namespace SmartSchool.WebApi.Helpers
{
    public class SmartSchoolProfile : Profile
    {
        public SmartSchoolProfile()
        {
            CreateMap<Aluno,AlunoDto>()
            .ForMember
            (
                dest => dest.Nome,
                otp => otp.MapFrom(src => $"{src.Nome} {src.Sobrenome}")
            )
            .ForMember
            (
                dest => dest.Idade,
                otp => otp.MapFrom(src => src.DataNasc.GetCurrentAge())
            );

            CreateMap<AlunoDto,Aluno>();
            CreateMap<Aluno,AlunoRegistrarDto>().ReverseMap();

            CreateMap<Professor,ProfessorDto>()
                .ForMember(
                    dest => dest.Nome,
                    opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}")
                );

            CreateMap<Professor,ProfessorRegistrarDto>().ReverseMap();
        }
    }
}