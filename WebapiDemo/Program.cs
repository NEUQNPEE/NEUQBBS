/*
 * @Author       : NieFire planet_class@foxmail.com
 * @Date         : 2024-04-15 16:23:07
 * @LastEditors  : NieFire planet_class@foxmail.com
 * @LastEditTime : 2024-08-01 17:23:37
 * @FilePath     : \CS_Computer-Science-and-Technologye:\CX\WebapiDemo\WebapiDemo\Program.cs
 * @Description  : 
 * ( ﾟ∀。)只要加满注释一切都会好起来的( ﾟ∀。)
 * Copyright (c) 2024 by NieFire, All Rights Reserved. 

 update 2024年8月1日 引入Serilog日志框架，在控制台与文件中存储日志，info级7日保留，warning/error级30日保留
 */
using WebApiDemo.BLL;
using WebApiDemo.BLL.Interfaces;
using WebApiDemo.DAL.Factorys;
using WebApiDemo.DAL.Interfaces;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// 添加日志
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7)
    .WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning || e.Level == LogEventLevel.Error)
        .WriteTo.File("Logs/warning_and_errors-.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30))
    .CreateLogger();

builder.Host.UseSerilog();

// 依赖注入
builder.Services.AddSingleton<IUserBll, UserBll>();
builder.Services.AddSingleton<IPostDalFactory, PostDalFactory>();
builder.Services.AddSingleton<IPostBll, PostBll>();
builder.Services.AddSingleton<ISectionBll, SectionBll>();

// 解决跨域问题，允许所有请求
builder.Services.AddCors(c=>
{
    c.AddPolicy("any", p=>
    {
        p.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{ 
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();
app.UseCors("any");
app.Run();