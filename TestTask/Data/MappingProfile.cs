using AutoMapper;
using TestTask.Data.DataTransferObject;
using TestTask.Data.Models;
using TestTask.Models;

namespace TestTask.Data
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistrationDto, User>().ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
            CreateMap<JobDto, Job>().ForMember(j => j.NextExecutionDate, opt => opt.MapFrom(x => x.StartDate));
        }
    }
}
