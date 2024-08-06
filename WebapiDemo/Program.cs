/*
 * @Author       : NieFire planet_class@foxmail.com
 * @Date         : 2024-04-15 16:23:07
 * @LastEditors  : NieFire planet_class@foxmail.com
 * @LastEditTime : 2024-08-05 16:56:53
 * @FilePath     : \CS_Computer-Science-and-Technologye:\CX\WebapiDemo\WebapiDemo\Program.cs
 * @Description  : 
 * ( ﾟ∀。)只要加满注释一切都会好起来的( ﾟ∀。)
 * Copyright (c) 2024 by NieFire, All Rights Reserved. 

 update 2024年8月1日 引入Serilog日志框架，在控制台与文件中存储日志，info级7日保留，warning/error级30日保留
 update 2024年8月4日 增添全局异常过滤器，暂采用同步方式
 */
using WebApiDemo.BLL;
using WebApiDemo.BLL.Interfaces;
using WebApiDemo.DAL.Factorys;
using WebApiDemo.DAL.Interfaces;
using Serilog;
using Serilog.Events;
using WebapiDemo.Filter;

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

// 全局异常过滤器
builder.Services.AddControllers(options => options.Filters.Add<GlobalExceptionFilter>());

// 服务注册。在此处注册的服务可以用来实现依赖注入。
// tip 注册服务是指将类或接口注册到DI容器中，以便在应用程序中使用。
// tip 依赖注入是指从DI容器中获取已注册的服务，并将其注入到应用程序的其他部分。
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