using System.Text;
using AutoMapper;
using IQueueAPI.AutoMapper;
using IQueueAPI.Extensions;
using IQueueBL.AutoMapper;
using IQueueBL.Helpers;
using IQueueBL.Interfaces;
using IQueueBL.Services;
using IQueueData;
using IQueueData.Interfaces;
using IQueueData.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(config =>
    {
        config.RespectBrowserAcceptHeader = true;
        config.ReturnHttpNotAcceptable = true;
    })
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

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
builder.Services.AddScoped<IUserInQueueRepository, UserInQueueRepository>();
builder.Services.AddScoped<ISwitchRequestRepository, SwitchRequestRepository>();

builder.Services.AddScoped<IQueueService, QueueService>();
builder.Services.AddScoped<IRecordService, RecordService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IParticipantService, ParticipantService>();
builder.Services.AddScoped<ISwitchRequestService, ISwitchRequestService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddSingleton(new TokenHelper(builder.Configuration));

builder.Services.ConfigureSwagger();

var jwtSettings = builder.Configuration.GetSection("AccessToken");
var secretKey = jwtSettings.GetSection("Secret").Value;
builder.Services.AddAuthentication(opt =>
    {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }) 
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, 
            ValidateAudience = false, 
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true, 
            ValidIssuer = jwtSettings.GetSection("Issuer").Value,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });

builder.Services.AddAuthentication();


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.All });

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();