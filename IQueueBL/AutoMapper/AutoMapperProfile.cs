using AutoMapper;
using IQueueBL.Models;
using IQueueData.Entities;


namespace IQueueBL.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Queue, QueueModel>()
            .ForMember(qm => qm.RecordsIds, q => q.MapFrom(x => x.QueueRecords.Select(r => r.Id)));

        CreateMap<QueueModel, Queue>();

        CreateMap<QueueRecord, RecordModel>();

        CreateMap<RecordModel, QueueRecord>();

        CreateMap<User, UserModel>()
            .ForMember(um => um.RecordsIds, u => u.MapFrom(x => x.QueueRecords.Select(r => r.Id)));

        CreateMap<UserModel, User>();
    }
}