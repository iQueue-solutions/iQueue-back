using AutoMapper;
using IQueueAPI.AutoMapper;
using IQueueBL.AutoMapper;
using IQueueBL.Interfaces;
using IQueueBL.Services;
using IQueueData;
using IQueueData.Interfaces;
using IQueueData.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var connStr = builder.Configuration.GetConnectionString("QueueDb");
builder.Services.AddDbContext<QueueDbContext>(options => options.UseSqlServer(connStr));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//services cors
builder.Services.AddCors(p => p.AddPolicy("policyforall", b =>
{
    b.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AutoMapperProfileApi());
    mc.AddProfile(new AutoMapperProfile());
});

builder.Services.AddSingleton(mapperConfig.CreateMapper());

builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IQueueRepository, QueueRepository>();
builder.Services.AddScoped<IRecordRepository, RecordRepository>();
builder.Services.AddScoped<IUserGroupRepository, UserGroupRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IQueueService, QueueService>();
builder.Services.AddScoped<IRecordService, RecordService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("policyforall");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();