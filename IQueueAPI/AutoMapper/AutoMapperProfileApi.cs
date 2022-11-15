using AutoMapper;
using IQueueAPI.Requests;
using IQueueBL.Models;

namespace IQueueAPI.AutoMapper;

public class AutoMapperProfileApi : Profile
{
    public AutoMapperProfileApi()
    {
        CreateMap<QueuePostRequest, QueueModel>();
        
        CreateMap<QueueModel, QueuePostRequest>();

        CreateMap<RecordModel, RecordPostRequest>();

        CreateMap<RecordPostRequest, RecordModel>();

        CreateMap<UserModel, UserPostRequest>();

        CreateMap<UserPostRequest, UserModel>();
    }
}