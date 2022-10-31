using AutoMapper;
using IQueueBL.Models;
using IQueueData.Entities;


namespace IQueueBL.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Queue, QueueModel>();

        CreateMap<QueueModel, Queue>();
        
        CreateMap<Record, RecordModel>();

        CreateMap<RecordModel, Record>();

        CreateMap<User, UserModel>()
            .ForMember(um => um.RecordsIds, u => u.MapFrom(x => x.UserInQueues.Select(r => r.Id)));

        CreateMap<UserModel, User>();
    }
}