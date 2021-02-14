using AutoMapper;
using MunicipalTax.Data.Entities;

namespace MunicipalTax.Data.MapperProfiles
{
    public class RepositoryProfile : Profile
    {
        public RepositoryProfile()
        {
            CreateMap<Municipality, Logic.Models.Municipality>();
        }
    }
}
