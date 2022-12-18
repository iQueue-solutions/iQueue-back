using AutoMapper;
using IQueueBL.Models;
using IQueueData.Entities;


namespace IQueueBL.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Queue, QueueModel>()
            .ForMember(qm => qm.Participants, q => q.MapFrom(x => x.QueueUsers));

        CreateMap<QueueModel, Queue>();

        CreateMap<Record, RecordModel>()
            .ForMember(rm => rm.UserId, r => r.MapFrom(x => x.UserQueue.UserId))
            .ForMember(rm => rm.QueueId, r => r.MapFrom(x => x.UserQueue.QueueId))
            .ForMember(rm => rm.ParticipantId, r => r.MapFrom(x => x.UserQueue.Id));
        
        CreateMap<RecordModel, Record>()
            .ForMember(r => r.UserQueueId, rm => rm.MapFrom(x => x.ParticipantId));

        CreateMap<UserInQueue, ParticipantModel>();
        
        CreateMap<ParticipantModel, UserInQueue>();

        CreateMap<User, UserModel>();

        CreateMap<UserModel, User>();

        CreateMap<SwitchRequest, SwitchRequestModel>();
        
        CreateMap<SwitchRequestModel, SwitchRequest>();
    }
}