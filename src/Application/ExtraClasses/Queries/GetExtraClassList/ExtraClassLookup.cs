using AutoMapper;
using CrouseMath.Application.Common.Mappings;
using CrouseMath.Domain.Entities;

namespace CrouseMath.Application.ExtraClasses.Queries.GetExtraClassList
{
    public class ExtraClassLookup : IMapFrom<ExtraClass>
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ExtraClass, ExtraClassLookup>();
        }
    }
}