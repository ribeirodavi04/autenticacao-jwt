using AutenticacaoJWT.Application.DTO;
using AutenticacaoJWT.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacaoJWT.Application.AutoMapper
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile() 
        { 
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
