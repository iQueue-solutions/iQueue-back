using AutoMapper;
using IQueueBL.Models;
using IQueueData.Entities;


namespace IQueueBL.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Queue, QueueModel>()
            .ForMember(qm => qm.AdminId, q => q.MapFrom(x => x.Admin!.Id))
            .ForMember(qm => qm.RecordsIds, q => q.MapFrom(x => x.QueueRecords.Select(r => r.Id)));

        CreateMap<QueueModel, Queue>();
        
        CreateMap<QueueRecord, RecordModel>();

        CreateMap<RecordModel, QueueModel>();

        CreateMap<User, UserModel>()
            .ForMember(um => um.RecordsIds, u => u.MapFrom(x => x.QueueRecords.Select(r => r.Id)));

        CreateMap<UserModel, User>();
    }
}