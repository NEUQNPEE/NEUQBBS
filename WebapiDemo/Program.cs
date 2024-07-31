using WebApiDemo.BLL;
using WebApiDemo.BLL.Interfaces;
using WebApiDemo.DAL.Factorys;
using WebApiDemo.DAL.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

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