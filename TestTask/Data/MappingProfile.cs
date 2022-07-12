using AutoMapper;
using TestTask.Data.DataTransferObject;
using TestTask.Data.Models;


namespace TestTask.Data
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<JobDto, Job>().ForMember(j => j.NextExecutionDate, opt => opt.MapFrom(x => x.StartDate));
            CreateMap<JobForListDto, Job>();
        }
    }
}
