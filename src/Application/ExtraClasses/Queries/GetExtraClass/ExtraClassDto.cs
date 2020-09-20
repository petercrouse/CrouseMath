using AutoMapper;
using System;
using CrouseMath.Application.Common.Mappings;
using CrouseMath.Domain.Entities;

namespace CrouseMath.Application.ExtraClasses.Queries.GetExtraClass
{
    public class ExtraClassDto : IMapFrom<ExtraClass>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public long? TeacherId { get; set; }
        public string TeacherName { get; set; }
        public int Size { get; set; }
        public bool IsClassFull { get; set; }
        public TimeSpan Duration { get; set; }
        public long SubjectId { get; set; }
        public string SubjectName { get; set; }
        public double Price { get; set; }

        public void Mappings(Profile configuration)
        {
            configuration.CreateMap<ExtraClass, ExtraClassDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(d => d.SubjectName, opt => opt.MapFrom(c => c.Subject.Name))
                .ForMember(d => d.TeacherName,
                    opt => opt.MapFrom(c =>
                        c.Teacher != null ? $"{c.Teacher.FirstName} {c.Teacher.LastName}" : string.Empty));
        }
    }
}
