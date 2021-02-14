using AutoMapper;
using MunicipalTax.Public.Interfaces.v1.Models;

namespace MunicipalTax.Logic.Mapping
{
    public class LogicProfile : Profile
    {
        public LogicProfile()
        {
            CreateMap<Models.AppliedTax, AppliedTax>();
            CreateMap<AppliedTax, Models.AppliedTax>();
        }
    }
}
