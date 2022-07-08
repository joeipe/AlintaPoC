using AlintaPoC.Contracts;
using AlintaPoC.Domain;
using AlintaPoC.Integration.TableStorage.Domain;
using AutoMapper;
using SharedKernel.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlintaPoC.Application.Automapper
{
    public class ContractToDomainMappingProfile : Profile
    {
        public ContractToDomainMappingProfile()
        {
            CreateMap<PersonDto, Person>()
                .ForMember(dest => dest.DoB, opt => opt.MapFrom(src => src.DoB.ParseDate()));
            CreateMap<TodoDto, TodoEntity>();
        }
    }
}
