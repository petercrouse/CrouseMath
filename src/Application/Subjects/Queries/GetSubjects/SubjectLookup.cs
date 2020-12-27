using AutoMapper;
using CrouseMath.Application.Common.Mappings;
using CrouseMath.Domain.Entities;

namespace CrouseMath.Application.Subjects.Queries.GetSubjects
{
    public class SubjectLookup : IMapFrom<Subject>
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Subject, SubjectLookup>();
        }
    }
}