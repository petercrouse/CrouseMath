using AutoMapper;
using CrouseMath.Application.Common.Mappings;
using CrouseMath.Domain.Entities;

namespace CrouseMath.Application.Teachers.Queries.GetTeacherList
{
    public class TeacherLookup : IMapFrom<Teacher>
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Teacher, TeacherLookup>()
                .ForMember(d => d.Name, opt => opt.MapFrom(t => $"{t.FirstName} {t.LastName}"));
        }
    }
}