using AutoMapper;
using IQueueAPI.Models;
using IQueueBL.Models;

namespace IQueueAPI.AutoMapper;

public class AutoMapperProfileApi : Profile
{
    public AutoMapperProfileApi()
    {
        CreateMap<QueuePostViewModel, QueueModel>();
            

        CreateMap<QueueModel, QueuePostViewModel>();

        CreateMap<RecordModel, RecordPostViewModel>();

        CreateMap<RecordPostViewModel, RecordModel>();

        CreateMap<UserModel, UserPostViewModel>();

        CreateMap<UserPostViewModel, UserModel>();
    }
}